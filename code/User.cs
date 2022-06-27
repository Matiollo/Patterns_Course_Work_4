namespace code
{
    public class User: Participant
    {
        public int id;
        public string login;
        public string password;
        public string name;
        public string email;
        public string phoneNumber;
        public string tgUsername;
        public string preferedContact;

        public User(int id, string login, string password, string name, string email, string phoneNumber, string tgUsername, string preferedContact) 
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.name = name;
            this.email = email;
            this.phoneNumber = phoneNumber;
            this.tgUsername = tgUsername;
            this.preferedContact = preferedContact;
        }

        public override bool GetContacted(string info)
        {
            ContactStrategy contactStrategy;
            switch(this.preferedContact)
            {
                case "email":
                    contactStrategy = new EmailStrategy();
                    break;
                case "telegram":
                    contactStrategy = new TelegramStrategy();
                    break;
                case "phone":
                    contactStrategy = new PhoneStrategy();
                    break;
                default:
                    contactStrategy = new EmailStrategy();
                    break;
            }

            return contactStrategy.SendMessage(this, info);
        }
    }
}