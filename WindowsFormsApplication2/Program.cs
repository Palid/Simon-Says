using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimonSays
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        //[STAThread]
        static void Main()
        {
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
        }
        static void Application_ApplicationExit(object sender, EventArgs e)
        {
            try
            {
                Corale.Colore.Core.Keyboard.Instance.Clear();
            }
            catch
            {
                // Again catch all SDK errors just for kicks.
            }
        }
    }
}
