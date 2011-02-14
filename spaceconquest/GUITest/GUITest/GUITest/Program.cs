using System;
using System.Threading;
using spaceconquest;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace GUITest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Class1 c = new Class1();
            Thread k = new Thread(new ThreadStart(c.DoForm));
            
            using (Game1 game = new Game1())
            {
                k.Start();
                game.Run();
                k.Abort();
            }
        }
       
    }
#endif
}

