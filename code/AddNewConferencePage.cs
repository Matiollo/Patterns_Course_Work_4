using System;

namespace code
{
    public class AddNewConferencePage
    {
        private User user;
        private UserRepository userRep;
        private ConferenceRepository confRep;
        private UserInterface ui;

        public AddNewConferencePage(UserInterface ui, User user, UserRepository userRep, ConferenceRepository confRep)
        {
            this.user = user;
            this.userRep = userRep;
            this.confRep = confRep;
            this.ui = ui;
        }
        
        public void Run()
        {
            string action;
            Builder builder = new ConferenceBuilder(this.user.id);
            builder.FillInDefaultValues(this.user); 

            ParticipantsNotifier notifier = new ParticipantsNotifier();

            while(true)
            {
                action = ui.GetAddNewConferencePageAction();
                switch(action)
                {
                    case "name": 
                        string name = ui.GetName();
                        builder.ChangeName(name);
                        break;
                    case "theme": 
                        string theme = ui.GetTheme();
                        builder.ChangeTheme(theme);
                        break;
                    case "description": 
                        string description = ui.GetDescription();
                        builder.ChangeDescription(description);
                        break;
                    case "date and time": 
                        DateTime dateAndTime = ui.GetDateAndTime();
                        builder.ChangeDateAndTime(dateAndTime);
                        break;
                    case "place": 
                        string place = ui.GetPlace();
                        builder.ChangePlace(place);
                        break;
                    case "participant": 
                        string login = ui.GetParticipantsLogin();
                        User participant = userRep.FindByLogin(login); 
                        if(participant != null)
                        {
                            builder.addParticipant(participant);
                            notifier.addParticipant(participant);
                        }
                        else
                        {
                            ui.PrintUserWithLoginNotFound();
                        }
                        break;
                    case "save": 
                        Conference conference = builder.getResult();
                        int confId = confRep.Add(conference);
                        if(confId < 1)
                        {
                            ui.PrintConferenceAddError();
                            return;
                        }
                        ui.PrintSuccessfulConferenceAddMessage();
                        notifier.Notify(
                            "Dear user!\r\n"
                            + "You have been invited to a new conference!\r\n"
                            + $"It's called {conference.name} and will be held in {conference.place}" 
                            + $"at {conference.dateAndTime.ToString("HH:mm dd-MM-yyyy")}\r\n"
                            + $"Host: {this.user.name} (login: {this.user.login})"
                            );
                        return;
                    case "cancel":
                        return;
                    default:
                        break;
                }
            }
        }
    }
}