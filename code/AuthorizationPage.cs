namespace code
{
    public class AuthorizationPage
    {
        private UserRepository userRep;
        private UserInterface ui;

        public AuthorizationPage(UserInterface ui, UserRepository userRep)
        {
            this.userRep = userRep;
            this.ui = ui;
        }

        public User Run()
        {
            User user = null;
            while(user == null)
            {
                string action = ui.LogInOrRegister();
                if(action == "exit") 
                {
                    return null;
                } 
                else if(action == "log in")
                {
                    user = LogInUser();
                }
                else 
                {
                    user = RegisterUser();
                }
            }
            return user;
        }

        public User LogInUser()
        {
            string login = ui.GetLogin();
            string password = ui.GetPassword();
            User user = this.userRep.FindByLoginAndPassword(login, password);
            if(user == null) 
            {
                ui.PrintIncorrectLoginorPasswordMessage();
            }
            return user;
        }

        public User RegisterUser()
        {
            string login = ui.GetLogin();
            while(this.userRep.FindByLogin(login) != null)
            {
                ui.PrintUserWithLoginAlreadyExistsError();
                login = ui.GetLogin();
            }
            string password = ui.GetPassword();
            string name = ui.GetName();
            string email = ui.GetEmail();
            string phoneNumber = ui.GetPhoneNumber();
            string tgUsername = ui.GetTgUsername();
            string preferedContact = ui.GetPreferedContact();
            User user = new User(-1, login, password, name, email, phoneNumber, tgUsername, preferedContact);
            user.id = this.userRep.Add(user);
            if(user.id < 1)
            {
                ui.PrintUserAddError();
                return null;
            }
            return user;
        }
    }
}