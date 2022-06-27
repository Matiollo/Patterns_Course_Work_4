using System;

namespace code
{
    class Program
    {        
        static void Main(string[] args)
        {
            UserRepository userRep = new UserRepository("./../data/DB.db");
            ConferenceRepository confRep = new ConferenceRepository("./../data/DB.db");
            UserInterface ui = new ConsoleUserInterface();

            while(true)
            {
                User user = new AuthorizationPage(ui, userRep).Run();
                if(user == null)
                {
                    break;
                }

                if(new MainPage(ui, user, userRep, confRep).Run())
                {
                    break;
                }
            }
        }

        
    }
}
