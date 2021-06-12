using System.Collections.Generic;

namespace Instruments
{
    public interface ITuneConstraint
    {
        bool Matches(List<Instrument> instrument);
    }
}