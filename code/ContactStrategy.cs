namespace code
{
    public abstract class ContactStrategy
    {
        public abstract bool SendMessage(User user, string message);
    }
}