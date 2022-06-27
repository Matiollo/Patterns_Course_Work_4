using System;
using System.Collections.Generic;

namespace code
{
    public class Conference
    {
        public int id;
        public string name;
        public string theme;
        public string description;
        public DateTime dateAndTime;
        public string place;
        public List<User> participants = new List<User>();
        public int creatorId;
        public State state = new ArrivingState();

        public Conference(int id, string name, string theme, string description, DateTime dateAndTime, string place, int creatorId, string state)
        {
            this.id = id;
            this.name = name;
            this.theme = theme;
            this.description = description;
            this.dateAndTime = dateAndTime;
            this.place = place;
            this.creatorId = creatorId;
            if(state == "Arriving")
            {
                this.state = new ArrivingState();
            }
            else if(state == "Complete")
            {
                this.state = new CompleteState();
            }
        }

        public Conference(int creatorId)
        {
            this.creatorId = creatorId;
        }

        public List<string> GetPossibleActions()
        {
            return this.state.GetPossibleActionsOfState();
        }

        public void NextState()
        {
            this.state = new CompleteState();
        }
    }
}