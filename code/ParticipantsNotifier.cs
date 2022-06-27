using System.Collections.Generic;

namespace code
{
    public class ParticipantsNotifier
    {
        private List<Participant> participants = new List<Participant>();

        public ParticipantsNotifier()
        {
            this.participants = new List<Participant>();
        }

        public ParticipantsNotifier(List<User> users)
        {
            foreach(User user in users)
            {
                this.participants.Add(user);
            };
        }

        public void addParticipant(Participant participant)
        {
            this.participants.Add(participant);
        }

        public bool removeParticipant(Participant participant)
        {
            return this.participants.Remove(participant);
        }

        public bool Notify(string message)
        {
            bool allNotified = true;
            foreach(Participant participant in this.participants)
            {
                bool notified = participant.GetContacted(message);
                allNotified = allNotified && notified;
            }
            return allNotified;
        }
    }
}