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

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new StartForm());

            new Game().start();
            int var1 = Console.Read();
            Console.Read();
            Console.Read();
            Console.WriteLine((char)var1);
            Console.WriteLine("Before first readline");
            Console.ReadLine();
            Console.WriteLine("After first readline");
            Console.ReadLine();
            Console.WriteLine("After second readline");

        }
    }
}
