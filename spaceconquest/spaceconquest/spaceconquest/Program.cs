using System;
using System.Threading;

namespace spaceconquest
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                while (true)
                {
                    String serverHttp = "http://localhost:8080/";
                    GlobalChatClient s = new GlobalChatClient(serverHttp);
                    //s.UpdateLog("http://localhost:8085/?GetInfo=1");
                    s.UpdateLog();
                    Console.WriteLine(" ==== Writing +++=");
                    //s.SendMessage("cody3", "hope you are doing fine s");
                    Thread.Sleep(1000);
                    Console.Clear();
                    s.UpdateLog();
                    
                }
                //game.Run();
            }
        }
    }
#endif
}

