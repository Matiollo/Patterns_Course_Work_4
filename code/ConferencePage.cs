using System.Collections.Generic;

namespace code
{
    public class ConferencePage
    {
        private User user;
        private Conference conference;
        private UserRepository userRep;
        private ConferenceRepository confRep;
        private UserInterface ui;

        public ConferencePage(UserInterface ui, User user, Conference conference, UserRepository userRep, ConferenceRepository confRep)
        {
            this.user = user;
            this.conference = conference;
            this.userRep = userRep;
            this.confRep = confRep;
            this.ui = ui;
        }

        public void Run()
        {
            string action;

            while(true)
            {
                ui.PrintConferenceInfo(this.conference);
                List<string> actions = this.conference.GetPossibleActions();
                action = ui.GetConferencePageAction(actions);
                switch(action)
                {
                    case "Edit": 
                        this.conference = new ConferenceEditPage(this.ui, this.user, this.conference, this.userRep, this.confRep).Run();
                        break;
                    case "Complete":
                        this.conference.NextState();
                        this.confRep.Update(this.conference);                                
                        break;
                    case "Send invitations": 
                        ParticipantsNotifier notifier = new ParticipantsNotifier(this.conference.participants);
                        bool notified = notifier.Notify(
                            "Dear user!\r\n"
                            + "We want to remind you that you have been invited to a conference!\r\n"
                            + $"It's called {conference.name} and will be held in {conference.place}" 
                            + $"at {conference.dateAndTime.ToString("HH:mm dd-MM-yyyy")}\r\n"
                            + $"Host: {this.user.name} (login: {this.user.login})"
                            );
                        if(!notified)
                        {
                            ui.PrintNotificationError();
                        }
                        ui.PrintSuccessfulNotificationSendMessage();
                        break;
                    case "go back":
                        return;
                }
            }
        }
    }
}