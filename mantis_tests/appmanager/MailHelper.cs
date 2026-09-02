using System;
using OpaqueMail;

namespace mantis_tests
{
    public class MailHelper : HelperBase
    {
        public MailHelper(ApplicationManager manager) : base(manager) { }

        // Метод для получения последнего письма для указанной учетной записи
        public String GetLastMail(AccountData account)
        {
            // Попытка получить письмо в течение 20 попыток с интервалом в 3 секунды
            for (int i = 0; i < 20; i++)
            {
                Pop3Client pop3 = new Pop3Client(
                    "localhost",
                    110,
                    account.Name,
                    account.Password,
                    false);

                pop3.Connect();
                pop3.Authenticate();

                // Проверяем, есть ли письма в почтовом ящике
                if (pop3.GetMessageCount() > 0)
                {
                    MailMessage message = pop3.GetMessage(1);
                    string body = message.Body;

                    pop3.DeleteMessage(1);
                    pop3.LogOut();

                    return body;
                }
                else
                {
                    System.Threading.Thread.Sleep(3000);
                }
            }

            return null;
        }
    }
}