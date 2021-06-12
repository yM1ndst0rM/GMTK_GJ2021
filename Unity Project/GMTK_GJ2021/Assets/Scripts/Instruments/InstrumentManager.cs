using System;
using System.Collections;
using System.Collections.Generic;
using Instruments;
using UnityEngine;

namespace Instruments
{
    public class InstrumentManager: MonoBehaviour
    {
        private void Start()
        {
            Console.Out.WriteLine("Test");
        }

        public List<GameObject> InstrumentTemplates = new List<GameObject>();
        public int MaxInstrumentInfluenceDistance = 10;
        public List<GameObject> Instuments { get; private set; }

        public enum InstrumentType
        {
            Trumpet,
            Drums,
        }

        private List<Instrument> GetAllHearableInstruments(GameObject target)
        {
            return null;
        }
        
        
    }
}