using Dashboard.WebHost.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Dashboard.WebHost
{
    public class LogWriter : TextWriter
    {
        public override void WriteLine(string value)
        {
            //do whatever with value
        }

        public override Encoding Encoding
        {
            get { return Encoding.Default; }
        }
    }
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //ConsoleManager.InitializeConsoleManager();
            var writer = new LogWriter();
            Console.SetOut(writer);
            EfConfig.Initialize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}