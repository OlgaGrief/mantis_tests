using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Internal;
using System;
using System.Text;
using System.Threading;

namespace mantis_tests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        public RegistrationHelper Registration { get; }       
        private FtpHelper ftp;
        public FtpHelper Ftp
        {
            get
            {
                if (ftp == null)
                {
                    ftp = new FtpHelper(this);
                }
                return ftp;
            }
        }
        public JamesHelper James { get; }
        public MailHelper Mail { get; }
        public LoginHelper Auth { get; }
        public ManagementMenuHelper Menu { get; }
        public ProjectManagementHelper Project { get; }
        public AdminHelper Admin { get; }
        public APIHelper API { get; set;  }

        private StringBuilder verificationErrors;
        private static ThreadLocal<ApplicationManager> app = new ThreadLocal<ApplicationManager>();

        private ApplicationManager()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost/mantisbt-2.28.4";

            Registration = new RegistrationHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);

            Auth = new LoginHelper(this);
            Menu = new ManagementMenuHelper(this);
            Project = new ProjectManagementHelper(this);

            Admin = new AdminHelper(this, baseURL);
            API = new APIHelper(this);
        }

        ~ApplicationManager()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        // синглтон, чтобы не создавать каждый раз новый экземпляр класса ApplicationManager
        public static ApplicationManager GetInstance()
        {
            if (!app.IsValueCreated)
            {
                ApplicationManager newInstance = new ApplicationManager();
                newInstance.driver.Url = newInstance.baseURL + "/login_page.php";
                app.Value = newInstance;
            }
            return app.Value;
        }

        public IWebDriver Driver
        { get { return driver; } }

    }
}
