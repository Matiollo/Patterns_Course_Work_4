using System.Collections.Generic;

namespace code
{
    public class ArrivingState : State
    {
        public override List<string> GetPossibleActionsOfState()
        {
            List<string> actions = new List<string>();
            actions.Add("Edit");
            actions.Add("Complete");
            actions.Add("Send invitations");
            return actions;
        }

        public override string ToString()
        {
            return "Arriving";
        }
    }
}