namespace code
{
    public interface UserInterface
    {
        public string LogInOrRegister();
        public string GetLogin();
        public string GetPassword();
        public string GetName();
        public string GetEmail();
        public string GetPhoneNumber();
        public string GetTgUsername();
        public string GetPreferedContact();
        public void PrintIncorrectLoginorPasswordMessage();
        public void PrintUserAddError();
        public void PrintUserWithLoginAlreadyExistsError();
        public void PrintUserWithLoginNotFound();
        public string GetMainMenuAction();
        public string GetAddNewConferencePageAction();
        public string GetTheme();
        public string GetDescription();
        public System.DateTime GetDateAndTime();
        public string GetPlace();
        public string GetParticipantsLogin();
        public void PrintConferenceAddError();
        public void PrintSuccessfulConferenceAddMessage();
        public void PrintUsersConferencesPageHeadLine();
        public void PrintUsersConferencesInvitationsPageHeadLine();
        public void PrintConferenceRow(Conference conference, int num);
        public int GetConferenceIndex(int num);
        public void PrintConferenceInfo(Conference conference);
        public string GetConferencePageAction(System.Collections.Generic.List<string> actions);
        public void PrintSuccessfulNotificationSendMessage();
        public string GetEditConferencePageAction();
        public void PrintConferenceUpdateError();
        public void PrintNotificationError();
        public void PrintSuccessfulConferenceUpdateMessage();
    }
}