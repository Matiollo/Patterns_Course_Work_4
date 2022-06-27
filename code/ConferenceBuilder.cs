using System;

namespace code
{
    public class ConferenceBuilder : Builder
    {
        private Conference conference;

        public ConferenceBuilder(int creatorId)
        {
            this.conference = new Conference(creatorId);
        }

        public ConferenceBuilder(Conference conference)
        {
            this.conference = conference;
        }
        
        public override void ChangeName(string name)
        {
            this.conference.name = name;
        }

        public override void ChangeTheme(string theme)
        {
            this.conference.theme = theme;
        }

        public override void ChangeDescription(string description)
        {
            this.conference.description = description;
        }
        
        public override void ChangeDateAndTime(DateTime dateAndTime)
        {
            this.conference.dateAndTime = dateAndTime;
        }

        public override void ChangePlace(string place)
        {
            this.conference.place = place;
        }

        public override void addParticipant(User participant)
        {
            this.conference.participants.Add(participant);
        }

        public override Conference getResult()
        {
            return conference;
        }

        public override void FillInDefaultValues(User creator)
        {
            this.conference.name = "untitled";
            this.conference.theme = "none";
            this.conference.description = "none";
            this.conference.dateAndTime = new DateTime(2022, 12, 31, 8, 0, 0);
            this.conference.place = "none";
            this.conference.participants = new System.Collections.Generic.List<User>();
            this.conference.participants.Add(creator);
        }
    }
}