namespace code
{
    public class MainPage
    {
        private User user;
        private UserRepository userRep;
        private ConferenceRepository confRep;
        private UserInterface ui;

        public MainPage(UserInterface ui, User user, UserRepository userRep, ConferenceRepository confRep)
        {
            this.user = user;
            this.userRep = userRep;
            this.confRep = confRep;
            this.ui = ui;
        }
        
        public bool Run()
        {
            string action;
            while(true)
            {
                action = ui.GetMainMenuAction();
                switch(action)
                {
                    case "add new conference":
                        new AddNewConferencePage(this.ui, this.user, this.userRep, this.confRep).Run();
                        break;
                    case "user's conferences":
                        new UsersConferencesPage(this.ui, this.user, this.userRep, this.confRep).Run();
                        break;
                    case "conferences invitations":
                        new UsersConferencesInvitationsPage(this.ui, this.user, this.userRep, this.confRep).Run();
                        break;
                    case "sign out":
                        return false;
                    case "exit":
                        return true;
                    default:
                        break;
                }
            }
        }
    }
}