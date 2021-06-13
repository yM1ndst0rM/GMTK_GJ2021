using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InstrStateMachine : MonoBehaviour
{
    public int LoopDurationInSeconds;
    public int LifetimeInLoops;
    public State CurrentState { get; private set; }
    public UnityEvent<State> OnInstrumentStateChanged;
    
    public enum State
    {
        Spawning,
        Playing,
        Degraded,
        Despawning
    }
    
    private float _lastLoopStartTime;
    private float _lastLoopEndTime;
    
    
    // Start is called before the first frame update
    void Start()
    {
        GoToState(State.Spawning);
        _lastLoopEndTime = LifetimeInLoops * LoopDurationInSeconds + Time.time;
        _lastLoopStartTime = _lastLoopEndTime - LoopDurationInSeconds;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentState == State.Playing && Time.time > _lastLoopStartTime)
        {
            GoToState(State.Degraded);
        }

        if (CurrentState != State.Despawning || Time.time > _lastLoopEndTime)
        {
            GoToState(State.Despawning);
        }
    }

    public void OnSpawningCompleted()
    {
        GoToState(State.Playing);
    }

    public void OnDespawningCompleted()
    {
        Destroy(gameObject);
    }
    
    private void GoToState(State newState)
    {
        CurrentState = newState;
        OnInstrumentStateChanged.Invoke(CurrentState);
    }
}
