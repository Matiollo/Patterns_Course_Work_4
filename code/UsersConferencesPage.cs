using System.Collections.Generic;

namespace code
{
    public class UsersConferencesPage
    {
        private User user;
        private UserRepository userRep;
        private ConferenceRepository confRep;
        private UserInterface ui;

        public UsersConferencesPage(UserInterface ui, User user, UserRepository userRep, ConferenceRepository confRep)
        {
            this.user = user;
            this.userRep = userRep;
            this.confRep = confRep;
            this.ui = ui;
        }

        public void Run()
        {
            int index;

            while(true)
            {
                ui.PrintUsersConferencesPageHeadLine();
                List<Conference> conferences = this.confRep.GetAllConferencesOfTheUser(this.user.id);
                for(int i = 0; i < conferences.Count; i++)
                {
                    ui.PrintConferenceRow(conferences[i], i+1);
                }

                index = ui.GetConferenceIndex(conferences.Count);
                if(index == -1)
                { 
                    return;
                }
                Conference chosenConf = conferences[index];
                new ConferencePage(this.ui, this.user, chosenConf, this.userRep, this.confRep).Run();
            }
        }
    }
}