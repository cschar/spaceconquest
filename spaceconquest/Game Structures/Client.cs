using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;



namespace spaceconquest
{
    class Client : MiddleMan
    {
        Player player;
        SlaveDriver slavedriver;
        

        public Client(Player p, SlaveDriver sd)
        {
            player = p;
            slavedriver = sd;
        }

        void EndTurnHelper(List<Command> q)
        {
            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            IPAddress ip;
            IPAddress.TryParse("192.168.1.64",out ip);
            EndPoint e = new IPEndPoint(ip,6112);
            s.Connect(e);
            NetworkStream stream = new NetworkStream(s);
            SendThread sendthread = new SendThread(q, stream, ReturnCommands);
            Thread t = new Thread(new ThreadStart(sendthread.SendRecieve));
            t.Start();

        }

        //callback method passed to the thread
        public void ReturnCommands(List<Command> q)
        {
            slavedriver.Recieve(q);
        }

        
        /// <summary>
        /// Thread Class
        /// </summary>
        class SendThread
        {
            List<Command> queue;
            NetworkStream stream;
            Result result;
            BinaryFormatter formatter = new BinaryFormatter();

            public SendThread(List<Command> q, NetworkStream s, Result r)
            {
                queue = q;
                stream = s;
            }

            public void SendRecieve()
            {
                if (stream.CanWrite)
                {
                    foreach (Command c in queue)
                    {
                        formatter.Serialize(stream, c);
                    }
                }

                //recieve stuff from host here
                List<Command> completelist = new List<Command>();
                result(completelist);

            }

            public delegate void Result(List<Command> q);
        }
    }
}
