using MinimalisticTelnet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    public class JamesHelper : HelperBase
    {
        public JamesHelper(ApplicationManager manager) : base(manager) { }

        public void Add(AccountData account)
        {
            // Метод для добавления учетной записи на сервер James
            if (Verify(account))
            {
                return; // Если учетная запись уже существует, выходим из метода
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("adduser " + account.Name + " " + account.Password); // Отправка команды для добавления учетной записи
            System.Console.Out.WriteLine(telnet.Read()); // Чтение ответа от сервера James
        }

        public void Delete(AccountData account)
        {
            // Метод для удаления учетной записи на сервер James
            if (!Verify(account))
            {
                return; // Если учетная запись не существует, выходим из метода
            }
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("deluser " + account.Name); // Отправка команды для удаления учетной записи
            System.Console.Out.WriteLine(telnet.Read()); // Чтение ответа от сервера James
        }

        public bool Verify(AccountData account)
        {
            // Метод для проверки существования учетной записи на сервере James
            TelnetConnection telnet = LoginToJames();
            telnet.WriteLine("verify " + account.Name); // Отправка команды для проверки существования учетной записи
            string s = telnet.Read(); // Чтение ответа от сервера James
            System.Console.Out.WriteLine(s); // Вывод ответа на консоль для отладки
            return !s.Contains("does not exist"); // Вернуть true, если учетная запись существует, иначе false
        }

        public void Update(AccountData account)
        {
        }

        // Метод для входа в систему на сервер James через telnet
        private TelnetConnection LoginToJames()
        {
            TelnetConnection telnet = new TelnetConnection("localhost", 4555); // Подключение к серверу James через telnet
            System.Console.Out.WriteLine(telnet.Read()); // Чтение приветственного сообщения от сервера James
            telnet.WriteLine("root"); // Отправка команды для входа в систему
            System.Console.Out.WriteLine(telnet.Read()); // Чтение ответа от сервера James
            telnet.WriteLine("root"); 
            System.Console.Out.WriteLine(telnet.Read());
            return telnet;
        }
    }
}