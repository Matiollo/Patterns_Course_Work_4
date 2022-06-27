using System.Collections.Generic;

namespace code
{
    public abstract class State
    {
        public abstract List<string> GetPossibleActionsOfState();
    }
}