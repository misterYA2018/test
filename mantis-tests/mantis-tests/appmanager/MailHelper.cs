using OpaqueMail;

namespace mantis_tests
{
    public class MailHelper : HelperBase
    {
        public MailHelper(ApplicationManager manager) : base(manager) { }

        public string GetLastMail(AccountData account)
        {
            for (int i = 0; i < 20; i++)
            {
                var pop3 = new Pop3Client("localhost", 110, account.Name, account.Password, false);

                pop3.Connect();
                pop3.Authenticate();
                var count = pop3.GetMessageCount();

                if (count > 0)
                {
                    var message = pop3.GetMessage(1);
                    var body = message.Body;

                    pop3.DeleteMessage(1);

                    return body;
                }

                System.Threading.Thread.Sleep(3000);
            }

            return null;
        }
    }
}
