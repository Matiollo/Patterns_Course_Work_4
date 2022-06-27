namespace code
{
    public class TelegramStrategy : ContactStrategy
    {
        public override bool SendMessage(User user, string message)
        {
            // Here should be the real implementation of sending a message with text from 'message' to telegram 
            // account with username 'user.tgUsername'. However being the hardest part of the project, it's 
            // irrelevant to the pattern idea. So I decided to skip the real implementation, printing "[Message 
            // has been sent to (username)]" in console to show that this part of code has been executed.

            System.Console.WriteLine($"[Message has been sent to {user.tgUsername}]");
            return true;
        }
    }
}