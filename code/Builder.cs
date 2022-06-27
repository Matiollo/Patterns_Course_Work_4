using System;

namespace code
{
    public abstract class Builder
    {
        public abstract void ChangeName(string name);
        public abstract void ChangeTheme(string theme);
        public abstract void ChangeDescription(string description);
        public abstract void ChangeDateAndTime(DateTime dateAndTime);
        public abstract void ChangePlace(string place);
        public abstract void addParticipant(User participant);
        public abstract Conference getResult();
        public abstract void FillInDefaultValues(User creator);
    }
}