using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Instruments;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Instruments
{
    public class InstrumentManager : MonoBehaviour
    {
        public static InstrumentManager Instance { get; private set; }
        public float LoopDuration = 10.323f;
        private float _nextSpawnTime;

        public List<GameObject> InstrumentTemplates = new List<GameObject>();
        public GameObject MusicInstrumentContainer;
        public int InstrumentSpawnRate;
        
        public readonly List<ITuneConstraint> PossibleConstraints = new List<ITuneConstraint>()
        {
            new TuneContainsInstrumentConstraint(InstrumentType.Guitar),
            new TuneContainsInstrumentConstraint(InstrumentType.Banjo),
            new TuneContainsInstrumentConstraint(InstrumentType.Saxophone),
            new TuneContainsInstrumentConstraint(InstrumentType.Flute),
            new TuneContainsInstrumentConstraint(InstrumentType.Harp),
            new TuneDoesNotContainInstrumentConstraint(InstrumentType.Harp),
            new TuneDoesNotContainInstrumentConstraint(InstrumentType.Banjo),
        };
                

        public float MaxInstrumentInfluenceDistance = 10;
        private readonly List<Instrument> Instruments = new List<Instrument>();

        private static readonly MatchDimension[] _availableMatchDimensions =
            (MatchDimension[]) Enum.GetValues(typeof(MatchDimension));

        public IEnumerable<Instrument> GetAllInstruments()
        {
            return Instruments.AsEnumerable();
        }
        
        public IEnumerable<Instrument> GetAllAudibleInstruments(GameObject target)
        {
            return Instruments
                .FindAll(i =>
                    Vector3.Distance(i.transform.position, target.transform.position) < MaxInstrumentInfluenceDistance);
        }

        public bool IsATune(IReadOnlyCollection<Instrument> instruments)
        {
            //make sure minimum amount of instruments is present
            if (instruments.Count < 3)
            {
                return false;
            }

            //makes sure the instruments all have at least one dimension in common
            return instruments.Aggregate(new List<MatchDimension>(_availableMatchDimensions),
                (matches, i) => matches.Intersect(i.matchesWell).ToList()).Count > 0;
        }

        public bool AreListenerConstraintsSatisfied(IEnumerable<ITuneConstraint> l, IEnumerable<Instrument> instruments)
        {
            return l.All(c => c.Matches(instruments));
        }

        public ITuneConstraint GetRandomizedConstraint()
        {
            return PossibleConstraints[UnityEngine.Random.Range(0, PossibleConstraints.Count - 1)];
        }

        private void OnEnable()
        {
            Instance = this;
        }

        public void RegisterInstrument(Instrument i)
        {
            Instruments.Remove(i);
            Instruments.Add(i);
        }
        
        public void UnregisterInstrument(Instrument i)
        {
            Instruments.Remove(i);
        }


        private void Start()
        {
            _nextSpawnTime = Time.time;
        }

        private void Update()
        {
            if (_nextSpawnTime <= Time.time)
            {
                _nextSpawnTime += LoopDuration;
                var tiles = GameObject.FindGameObjectsWithTag("tiles");
                for (int i = 0; i < InstrumentSpawnRate; i++)
                {
                    var nextInstrumentIndex = Mathf.FloorToInt(Random.Range(0, InstrumentTemplates.Count - 1));
                    var nextTile = tiles[Mathf.FloorToInt(Random.Range(0, tiles.Length - 1))];

                    var newInstrument = Instantiate(InstrumentTemplates[nextInstrumentIndex], nextTile.transform.position,
                        Quaternion.identity);

                    newInstrument.transform.parent = MusicInstrumentContainer.transform;
                }
            }
        }
    }

    public enum InstrumentType
    {
        Guitar,
        Banjo,
        Saxophone,
        Flute,
        Harp
    }
    
    public enum MatchDimension
    {
        SectionA,
        SectionB,
        SectionC,
        SectionD,
        SectionE
    }
}