using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace code
{
    public class ConsoleUserInterface : UserInterface
    {
        public string LogInOrRegister()
        {
            Console.WriteLine("1. Log in");
            Console.WriteLine("2. Register");
            Console.WriteLine("3. Exit");

            ConsoleKeyInfo pressedKey = Console.ReadKey();
            Console.WriteLine();
            if(pressedKey.KeyChar == '1')
            {
                return "log in";
            }
            else if(pressedKey.KeyChar == '2')
            {
                return "register";
            }
            else if(pressedKey.KeyChar == '3')
            {
                return "exit";
            }
            Console.WriteLine("Please enter 1, 2 or 3");
            Console.WriteLine();
            return LogInOrRegister();
        }

        public string GetLogin()
        {
            Console.WriteLine("Enter login: ");
            string login = Console.ReadLine();
            if(login.Length < 6)
            {
                Console.WriteLine("Login must contain at least 6 symbols.");
                Console.WriteLine();
                return GetLogin();
            }
            return login;
        }

        public string GetPassword()
        {
            Console.WriteLine("Enter password: ");
            string password = Console.ReadLine();
            if(password.Length < 6)
            {
                Console.WriteLine("Password must contain at least 6 symbols.");
                Console.WriteLine();
                return GetPassword();
            }
            return password;
        }

        public string GetName()
        {
            Console.WriteLine("Enter name: ");
            string name = Console.ReadLine();
            if(name.Length < 1)
            {
                Console.WriteLine("Name must contain at least 1 symbol.");
                Console.WriteLine();
                return GetName();
            }
            return name;
        }

        public string GetEmail()
        {
            Console.WriteLine("Enter email: ");
            string email = Console.ReadLine();
            if(!IsValidEmail(email)) 
            {
                Console.WriteLine("Entered text doesn't look like email.");
                Console.WriteLine();
                return GetEmail();
            }
            return email;
        }

        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

                string DomainMapper(Match match)
                {
                    var idn = new IdnMapping();
                    string domainName = idn.GetAscii(match.Groups[2].Value);
                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                string mes = e.Message;
                return false;
            }
            catch (ArgumentException e)
            {
                string mes = e.Message;
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public string GetPhoneNumber()
        {
            Console.WriteLine("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
            if(phoneNumber.Length < 7 || phoneNumber.Length > 13 || !OnlyNumeric(phoneNumber))
            {
                Console.WriteLine("Phone number must contain 7-13 digits only. Please do not use other symbols.");
                Console.WriteLine();
                return GetPhoneNumber();
            }
            return phoneNumber;
        }

        private bool OnlyNumeric(string text)
        {
            int num;
            foreach(char c in text)
            {
                if(!int.TryParse(c.ToString(), out num))
                {
                    return false;
                }
            }
            return true;
        }

        public string GetTgUsername()
        {
            Console.WriteLine("Enter telegram username: ");
            string username = Console.ReadLine();
            if(!IsTgUsername(username))
            {
                Console.WriteLine("Entered username is incorrect.");
                Console.WriteLine();
                return GetTgUsername();
            }
            return username;
        }

        private bool IsTgUsername(string username)
        {
            if(username.Length < 5 || username.Length > 32)
            {
                return false;
            }
            foreach(char c in username)
            {
                if(!Char.IsLetterOrDigit(c) && c != '_')
                {
                    return false;
                }
            }
            return true;
        }

        public string GetPreferedContact()
        {
            Console.WriteLine("Choose prefered contact method: ");
            Console.WriteLine("1. Email");
            Console.WriteLine("2. Telegram");
            Console.WriteLine("3. Phone");

            ConsoleKeyInfo pressedKey = Console.ReadKey();
            Console.WriteLine();
            if(pressedKey.KeyChar == '1')
            {
                return "email";
            }
            else if(pressedKey.KeyChar == '2')
            {
                return "telegram";
            }
            else if(pressedKey.KeyChar == '3')
            {
                return "phone";
            }
            Console.WriteLine("Please enter 1, 2 or 3");
            Console.WriteLine();
            return GetPreferedContact();
        }

        public void PrintIncorrectLoginorPasswordMessage()
        {
            Console.WriteLine("Incorrect login or password.");
            Console.WriteLine();
        }

        public void PrintUserAddError()
        {
            Console.WriteLine("There has been a problem with creating new acoount.");
            Console.WriteLine();
        }

        public void PrintUserWithLoginAlreadyExistsError()
        {
            Console.WriteLine("User with this login already exists.");
            Console.WriteLine();
        }

        public void PrintUserWithLoginNotFound()
        {
            Console.WriteLine("User with this login was not found in the system.");
            Console.WriteLine();
        }

        public string GetMainMenuAction()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Add new conference");
            Console.WriteLine("2. See your conferences");
            Console.WriteLine("3. See conferences you have been invited to");
            Console.WriteLine("4. Sign out");
            Console.WriteLine("5. Exit the program");

            ConsoleKeyInfo pressedKey = Console.ReadKey();
            Console.WriteLine();
            if(pressedKey.KeyChar == '1')
            {
                return "add new conference";
            }
            else if(pressedKey.KeyChar == '2')
            {
                return "user's conferences";
            }
            else if(pressedKey.KeyChar == '3')
            {
                return "conferences invitations";
            }
            else if(pressedKey.KeyChar == '4')
            {
                return "sign out";
            }
            else if(pressedKey.KeyChar == '5')
            {
                return "exit";
            }
            Console.WriteLine("Please enter a number from 1 to 5");
            Console.WriteLine();
            return GetMainMenuAction();
        }

        public string GetAddNewConferencePageAction()
        {
            Console.WriteLine("Create new conference");
            Console.WriteLine("Choose features you want to add/change:");
            Console.WriteLine("1. Add/change name");
            Console.WriteLine("2. Add/change theme");
            Console.WriteLine("3. Add/change description");
            Console.WriteLine("4. Add/change date and time");
            Console.WriteLine("5. Add/change place");
            Console.WriteLine("6. Add participant");
            Console.WriteLine("7. Save");
            Console.WriteLine("8. Cancel");
            Console.WriteLine("Help box: In case you decide not to fill some fields, they will be filled with default values.");

            ConsoleKeyInfo pressedKey = Console.ReadKey();
            Console.WriteLine();
            if(pressedKey.KeyChar == '1')
            {
                return "name";
            }
            else if(pressedKey.KeyChar == '2')
            {
                return "theme";
            }
            else if(pressedKey.KeyChar == '3')
            {
                return "description";
            }
            else if(pressedKey.KeyChar == '4')
            {
                return "date and time";
            }
            else if(pressedKey.KeyChar == '5')
            {
                return "place";
            }
            else if(pressedKey.KeyChar == '6')
            {
                return "participant";
            }
            else if(pressedKey.KeyChar == '7')
            {
                return "save";
            }
            else if(pressedKey.KeyChar == '8')
            {
                return "cancel";
            }
            Console.WriteLine("Please enter a number from 1 to 8");
            Console.WriteLine();
            return GetAddNewConferencePageAction();
        }

        public string GetTheme()
        {
            Console.WriteLine("Enter theme: ");
            string theme = Console.ReadLine();
            return theme;
        }

        public string GetDescription()
        {
            Console.WriteLine("Enter description: ");
            string description = Console.ReadLine();
            return description;
        }

        public DateTime GetDateAndTime()
        {
            DateTime dateAndTime = GetDate();
            dateAndTime = AddTime(dateAndTime);
            return dateAndTime;
        }
        
        private DateTime GetDate()
        {
            Console.WriteLine("Enter date using format \"yyyy-mm-dd\": ");
            string dateStr = Console.ReadLine();
            DateTime date;
            if(!DateTime.TryParse(dateStr, out date))
            {
                Console.WriteLine("Enter date according to the given format.");
                Console.WriteLine();
                return GetDate();
            }
            return date;
        }

        private DateTime AddTime(DateTime date)
        {
            Console.WriteLine("Enter time using format \"hh:mm\": ");
            string timeStr = Console.ReadLine();
            Regex rgx = new Regex("^([01]?\\d|2[0-3]):[0-5]\\d$");
            if(!rgx.IsMatch(timeStr))
            {
                Console.WriteLine("Enter time according to the given format.");
                Console.WriteLine();
                return AddTime(date);
            }

            int hour, minute;
            int.TryParse(timeStr.Split(":")[0], out hour);
            int.TryParse(timeStr.Split(":")[1], out minute);

            return new DateTime(date.Year, date.Month, date.Day, hour, minute, 0);
        }

        public string GetPlace()
        {
            Console.WriteLine("Enter place: ");
            string place = Console.ReadLine();
            return place;
        }

        public string GetParticipantsLogin()
        {
            Console.WriteLine("Enter login of the person you want to invite to the conference: ");
            string login = Console.ReadLine();
            if(login.Length < 6)    
            {
                Console.WriteLine("Login must contain at least 6 symbols.");
                Console.WriteLine();
                return GetParticipantsLogin();
            }
            return login;
        }

        public void PrintConferenceAddError()
        {
            Console.WriteLine("There has been a problem with creating new conference.");
            Console.WriteLine();
        }

        public void PrintSuccessfulConferenceAddMessage()
        {
            Console.WriteLine("Conference has been added successfully.");
            Console.WriteLine();
        }

        public void PrintUsersConferencesPageHeadLine()
        {
            Console.WriteLine("To go back press \"b\"");
            Console.WriteLine();
            Console.WriteLine("Your conferences:");
            Console.WriteLine("#    Name                Date         Status");
        }

        public void PrintUsersConferencesInvitationsPageHeadLine()
        {
            Console.WriteLine("To go back press \"b\"");
            Console.WriteLine();
            Console.WriteLine("Conferences you have been invited to:");
            Console.WriteLine("#    Name                Date         Status");
        }

        public void PrintConferenceRow(Conference conference, int num)
        {
            int spaceNum = 4 - num.ToString().Length;
            string space = " ";
            for(int i = spaceNum; i > 0; i--)
            {
                space += " ";
            }
            Console.Write(num + space + conference.name);

            space = "";
            for(int i = conference.name.Length; i < 20; i++)
            {
                space += " ";
            }
            Console.WriteLine(space + conference.dateAndTime.ToString("yyyy-MM-dd") + "   " + conference.state.ToString());
        }
        
        public int GetConferenceIndex(int num)
        {
            string input = Console.ReadLine();
            if(input == "b" || input == "B")
            {
                return -1;
            }
            for(int i = 0; i < num; i++)
            {
                if(input == (i+1).ToString())
                {
                    return i;
                }
            }

            Console.WriteLine($"Please enter \"b\" to go back or a number of the conference to see detailed information");
            Console.WriteLine();
            return GetConferenceIndex(num);
        }

        public void PrintConferenceInfo(Conference conference)
        {
            Console.WriteLine("Conference information");
            Console.WriteLine("Name: " + conference.name);
            Console.WriteLine("Theme: " + conference.theme);
            Console.WriteLine("Description: " + conference.description);
            Console.WriteLine("Time and date: " + conference.dateAndTime.ToString("HH:mm yyyy-MM-dd"));
            Console.WriteLine("Place: " + conference.place);
            Console.WriteLine("Number of participants: " + conference.participants.Count);
            Console.WriteLine("Status: " + conference.state.ToString());
            Console.WriteLine();
        }
        
        public string GetConferencePageAction(List<string> actions)
        {
            int counter = 1;
            foreach(string action in actions)
            {
                Console.WriteLine(counter + ". " + action);
                counter++;
            }
            Console.WriteLine(counter + ". Go back");
            Console.WriteLine();

            string pressedKey = Console.ReadLine();
            Console.WriteLine();
            int key;

            if(!int.TryParse(pressedKey, out key) || key < 1 || key > actions.Count + 1)
            {
                Console.WriteLine($"Please enter a number from 1 to {actions.Count + 1}");
                Console.WriteLine();
                return GetConferencePageAction(actions);
            }

            if(key > actions.Count)
            {
                return "go back";
            }
            return actions[key-1];
        }

        public void PrintSuccessfulNotificationSendMessage()
        {
            Console.WriteLine("Invitations were sent successfully.");
            Console.WriteLine();
        }

        public string GetEditConferencePageAction()
        {
            Console.WriteLine("Choose features you want to edit:");
            Console.WriteLine("1. Change name");
            Console.WriteLine("2. Change theme");
            Console.WriteLine("3. Change description");
            Console.WriteLine("4. Change date and time");
            Console.WriteLine("5. Change place");
            Console.WriteLine("6. Add participant");
            Console.WriteLine("7. Save");
            Console.WriteLine("8. Cancel");

            ConsoleKeyInfo pressedKey = Console.ReadKey();
            Console.WriteLine();
            if(pressedKey.KeyChar == '1')
            {
                return "name";
            }
            else if(pressedKey.KeyChar == '2')
            {
                return "theme";
            }
            else if(pressedKey.KeyChar == '3')
            {
                return "description";
            }
            else if(pressedKey.KeyChar == '4')
            {
                return "date and time";
            }
            else if(pressedKey.KeyChar == '5')
            {
                return "place";
            }
            else if(pressedKey.KeyChar == '6')
            {
                return "participant";
            }
            else if(pressedKey.KeyChar == '7')
            {
                return "save";
            }
            else if(pressedKey.KeyChar == '8')
            {
                return "cancel";
            }
            Console.WriteLine("Please enter a number from 1 to 8");
            Console.WriteLine();
            return GetEditConferencePageAction();
        }

        public void PrintConferenceUpdateError()
        {
            Console.WriteLine("There has been a problem with editing the conference.");
            Console.WriteLine();
        }

        public void PrintNotificationError()
        {
            Console.WriteLine("There has been a problem with notifying participants.");
            Console.WriteLine();
        }

        public void PrintSuccessfulConferenceUpdateMessage()
        {
            Console.WriteLine("Conference has been updated successfully.");
            Console.WriteLine();
        }
    }
}