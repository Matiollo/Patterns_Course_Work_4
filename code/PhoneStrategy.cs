namespace code
{
    public class PhoneStrategy : ContactStrategy
    {
        public override bool SendMessage(User user, string message)
        {
            // Here should be the real implementation of sending an SMS with text from 'message' to 'user.phoneNumber'.
            // However being the hardest part of the project, it's irrelevant to the pattern idea. So I decided to skip 
            // the real implementation, printing "[Message has been sent to (phone number)]" in console to show that 
            // this part of code has been executed.

            System.Console.WriteLine($"[Message has been sent to {user.phoneNumber}]");
            return true;
        }
    }
}