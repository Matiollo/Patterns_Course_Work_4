using System.Collections.Generic;

namespace code
{
    public class CompleteState : State
    {
        public override List<string> GetPossibleActionsOfState()
        {
            List<string> actions = new List<string>();
            actions.Add("Edit");
            return actions;
        }

        public override string ToString()
        {
            return "Complete";
        }
    }
}