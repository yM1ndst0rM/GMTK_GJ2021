using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Instruments
{
    public class Instrument : MonoBehaviour
    {
        public InstrumentType type;
        public AudioClip clip;
        public List<MatchDimension> matchesWell = new List<MatchDimension>();

        private void OnEnable()
        {
            var playback = gameObject.GetComponentInChildren<AudioSource>();
            playback.clip = clip;
			playback.Play();
        }

        private void Start()
        {
            InstrumentManager.Instance.RegisterInstrument(this);
        }

        private void OnDestroy()
        {
            InstrumentManager.Instance.UnregisterInstrument(this);
        }
    }
}