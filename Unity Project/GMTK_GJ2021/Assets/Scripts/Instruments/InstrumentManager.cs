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
        public readonly List<GameObject> InstrumentTemplates = new List<GameObject>();
        public readonly List<ITuneConstraint> PossibleConstraints = new List<ITuneConstraint>();
        public float MaxInstrumentInfluenceDistance = 10;
        private readonly List<GameObject> Instruments = new List<GameObject>();

        private static readonly MatchDimension[] _availableMatchDimensions =
            (MatchDimension[]) Enum.GetValues(typeof(MatchDimension));
        private IEnumerable<Instrument> GetAllAudibleInstruments(GameObject target)
        {
            return Instruments
                .FindAll( i => Vector3.Distance(i.transform.position, target.transform.position) < MaxInstrumentInfluenceDistance )
                .Select(i => i.GetComponent<Instrument>());
        }

        private bool IsATune(IReadOnlyCollection<Instrument> instruments)
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

        private bool AreListenerConstraintsSatisfied(TuneListener l, IReadOnlyCollection<Instrument> instruments)
        {
            return l.Constraints.All(c => c.Matches(instruments));
        }

        public ITuneConstraint GetRandomizedConstraint()
        {
            return PossibleConstraints[UnityEngine.Random.Range(0, PossibleConstraints.Count - 1)];
        }
    }

    public enum InstrumentType
    {
        Drums,
        Guitar,
        Banjo
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