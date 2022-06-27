using System;

namespace code
{
    public class ConferenceEditPage
    {
        private User user;
        private Conference conference;
        private UserRepository userRep;
        private ConferenceRepository confRep;
        private UserInterface ui;

        public ConferenceEditPage(UserInterface ui, User user, Conference conference, UserRepository userRep, ConferenceRepository confRep)
        {
            this.user = user;
            this.conference = conference;
            this.userRep = userRep;
            this.confRep = confRep;
            this.ui = ui;
        }

        public Conference Run()
        {
            string action;
            Builder builder = new ConferenceBuilder(this.conference);

            while(true)
            {
                action = ui.GetEditConferencePageAction();
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
                        }
                        else
                        {
                            ui.PrintUserWithLoginNotFound();
                        }
                        break;
                    case "save": 
                        this.conference = builder.getResult();
                        if(!this.confRep.Update(conference))
                        {
                            ui.PrintConferenceUpdateError();
                            return this.conference;
                        }
                        ui.PrintSuccessfulConferenceUpdateMessage();
                        return this.conference;
                    case "cancel":
                        return this.conference;
                }
            }
        }
    }
}