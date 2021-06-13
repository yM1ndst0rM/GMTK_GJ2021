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
            SnapToNearestTile();
        }

        private void OnDestroy()
        {
            InstrumentManager.Instance.UnregisterInstrument(this);
        }

        public void SnapToNearestTile()
        {
            var tiles = GameObject.FindGameObjectsWithTag("tiles");
            var shortestDistance = float.MaxValue;
            GameObject closestTile = null;

            foreach (var tile in tiles)
            {
                var distance = Vector3.Distance(tile.transform.position, transform.position);
                if (distance < shortestDistance)
                {
                    closestTile = tile;
                    shortestDistance = distance;
                }
            }

            if (closestTile != null)
            {
                transform.position = closestTile.transform.position;
            }
        }
    }
}