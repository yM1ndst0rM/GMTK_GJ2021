using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Instruments;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Networking.Match;

/** class: InstrumentInteraction
	 * 
	 * Purpose: This class handles detecting and pickingup/placing instruments
	 * 
	 */
public class InstrumentInteraction : MonoBehaviour
{
    public AudioClip pickUpSound, putDownSound;
    public float MaxPickUpRangeInMeters = 1;
    public GameObject CarryingAnchor;
    private ControlPlayer _player;
    private AudioSource _playerAudioSource;

    [CanBeNull] private Instrument _pickedUpInstrument;
    [CanBeNull] private Instrument _closestInstrument;
    private float _closestInstrumentDistance = float.MaxValue;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInParent<ControlPlayer>();
        _playerAudioSource = GetComponentInParent<AudioSource>();
        _pickedUpInstrument = null;
        _player.IsCarryingInstrument = false;
    }

    // Update is called once per frame
    void Update()
    {
        _closestInstrument = null;
        _closestInstrumentDistance = float.MaxValue;
        if (_pickedUpInstrument == null)
        {
            foreach (var i in InstrumentManager.Instance.GetAllInstruments())
            {
                var distance = Vector3.Distance(i.transform.position, _player.transform.position);
                if (distance < _closestInstrumentDistance)
                {
                    _closestInstrumentDistance = distance;
                    _closestInstrument = i;
                }
            }
        }
        else
        {
            _pickedUpInstrument.gameObject.transform.position = CarryingAnchor.transform.position;
        }
    }

    public bool CanPickUpInstrument()
    {
        return _pickedUpInstrument == null && _closestInstrumentDistance < MaxPickUpRangeInMeters;
    }

    public void PickUpInstrument()
    {
        if (!CanPickUpInstrument()) return;

        _pickedUpInstrument = _closestInstrument;
        _player.IsCarryingInstrument = true;
        _playerAudioSource.PlayOneShot(pickUpSound);
    }

    public void PutDownInstrument()
    {
        if (_pickedUpInstrument == null) return;

        _pickedUpInstrument = null;
        _player.IsCarryingInstrument = false;
        _playerAudioSource.PlayOneShot(putDownSound);
    }
}