using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Instruments;
using UnityEngine;
using UnityEngine.Events;

public class DancerNpc : MonoBehaviour
{
    public enum State
    {
        Spawning,
        Idle,
        Dancing,
        CantStandThis,
        DespawningSad,
        DespawningHappy
    }

    public int maxPatienceInSeconds = 30;
    public int dancingDurationInSeconds = 5;
    public List<ITuneConstraint> Constraints;
    public State currentState = State.Spawning;
    public UnityEvent<State> OnDancerStateChanged;
    public UnityEvent<int> OnPlayerRewardEarned;

    private float _patienceDeadline;
    private float _dancingDeadline;
    private int _currentPlayerReward = 0;

    private InstrumentManager _iM;

    // Start is called before the first frame update
    void Start()
    {
        _patienceDeadline = Time.time + maxPatienceInSeconds;
        _iM = FindObjectOfType<InstrumentManager>();
    }

    // Update is called once per frame
    void Update()
    {
        List<Instrument> audibleInstruments = _iM.GetAllAudibleInstruments(gameObject).ToList();
        switch (currentState)
        {
            case State.Idle:
                if (_iM.IsATune(audibleInstruments))
                {
                    if (_iM.AreListenerConstraintsSatisfied(Constraints, audibleInstruments))
                    {
                        _dancingDeadline = Time.time + dancingDurationInSeconds;
                        _currentPlayerReward = Mathf.CeilToInt(_patienceDeadline - Time.time);
                        GoToState(State.Dancing);
                    }
                    else
                    {
                        GoToState(State.CantStandThis);
                    }
                }
                else if (Time.time > _patienceDeadline)
                {
                    GoToState(State.DespawningSad);
                }


                break;
            case State.Dancing:
                if (!_iM.IsATune(audibleInstruments))
                {
                    _currentPlayerReward = 0;
                    GoToState(State.Idle);
                }
                else
                {
                    if (!_iM.AreListenerConstraintsSatisfied(Constraints, audibleInstruments))
                    {
                        _currentPlayerReward = 0;
                        GoToState(State.CantStandThis);
                    }
                    else
                    {
                        if (Time.time > _dancingDeadline)
                        {
                            OnPlayerRewardEarned.Invoke(_currentPlayerReward);
                            GoToState(State.DespawningHappy);
                        }
                    }
                }

                break;
            case State.CantStandThis:
                if (!_iM.IsATune(audibleInstruments))
                {
                    GoToState(State.Idle);
                }
                else
                {
                    if (_iM.AreListenerConstraintsSatisfied(Constraints, audibleInstruments))
                    {
                        _dancingDeadline = Time.time + dancingDurationInSeconds;
                        _currentPlayerReward = Mathf.CeilToInt(_patienceDeadline - Time.time);
                        GoToState(State.Dancing);
                    }
                    else if (Time.time > _patienceDeadline)
                    {
                        GoToState(State.DespawningSad);
                    }
                }

                break;
            case State.DespawningSad:
            case State.DespawningHappy:
            case State.Spawning:
            default:
                break;
        }
    }

    public void OnSpawningCompleted()
    {
        GoToState(State.Idle);
    }

    public void OnDespawningCompleted()
    {
        Destroy(gameObject);
    }

    private void GoToState(State newState)
    {
		Debug.Log("newState is currently: " + newState);
        //currentState = newState;
        //OnDancerStateChanged.Invoke(currentState);

		//if(newState == State.Idle)
    }
}