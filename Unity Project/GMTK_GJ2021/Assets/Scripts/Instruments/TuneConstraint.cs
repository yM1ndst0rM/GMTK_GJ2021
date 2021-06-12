using System.Collections.Generic;
using System.Linq;

namespace Instruments
{
    
    public interface ITuneConstraint
    {
        bool Matches(IEnumerable<Instrument> instrument);
    }

    public class TuneContainsInstrumentConstraint : ITuneConstraint
    {
        private readonly InstrumentType _requiredType;

        public TuneContainsInstrumentConstraint(InstrumentType requiredType)
        {
            _requiredType = requiredType;
        }

        public bool Matches(IEnumerable<Instrument> instrument)
        {
            return instrument.Any(i => i.type == _requiredType);
        }
    }
    
    public class TuneDoesNotContainInstrumentConstraint : ITuneConstraint
    {
        private readonly InstrumentType _prohibitedType;

        public TuneDoesNotContainInstrumentConstraint(InstrumentType prohibitedType)
        {
            _prohibitedType = prohibitedType;
        }

        public bool Matches(IEnumerable<Instrument> instrument)
        {
            return instrument.All(i => i.type != _prohibitedType);
        }
    }
}