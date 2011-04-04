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
using System.Threading;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace spaceconquest
{
    class Host : MiddleMan
    {
        List<Command> commands = new List<Command>();
        IPAddress ip;
        EndPoint end;
        Socket listensocket;
        SlaveDriver slavedriver;
        bool done = false;
        bool busy = false;
        int numclients;

        public Host(SlaveDriver sd, int n)
        {
            ip = IPAddress.Any; //IPAddress.Parse("70.55.141.164");
            end = new IPEndPoint(ip, 6112);
            listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listensocket.Bind(end);
            //listensocket.EnableBroadcast = false;
            slavedriver = sd;
            numclients = n;
        }

        public void Close()
        {
            listensocket.Dispose();
        }

        public bool DriverReady()
        {
            return done;
        }

        public void DriverReset()
        {
            done = false;
            busy = false;
        }

        public void AddCommand(Command c)
        {
            commands.Add(c);
        }

        public void EndTurn()
        {
            if (busy) { return; }
            busy = true;
            done = false;
            HostThread ht = new HostThread(listensocket, end, commands, ReturnCommands, numclients);
            commands = new List<Command>();
            Thread t = new Thread(new ThreadStart(ht.SendRecieve));
            t.Start();
            
        }

        private void ReturnCommands(List<Command> c)
        {
            slavedriver.Receive(c);
            done = true;
        }

        public class HostThread
        {
            int numclients;
            Socket socket;
            Socket accept;
            static List<NetworkStream> streamlist = new List<NetworkStream>();
            EndPoint end;
            BinaryFormatter formatter;
            List<Command> commands;
            Result action;

            public HostThread(Socket s, EndPoint e, List<Command> c, Result r, int n)
            {
                socket = s;
                end = e;
                formatter = new BinaryFormatter();
                commands = c;
                action = r;
                numclients = n;
            }

            public void SendRecieve()
            {
                while (streamlist.Count < numclients)
                {
                        while (true)
                        {
                            socket.Listen(1);
                            Console.WriteLine("Host Listening");
                            accept = socket.Accept();
                            //accept.Listen(10);
                            Console.WriteLine("Host Accepted");
                            break;
                        }

                        streamlist.Add(new NetworkStream(accept));
                }

                foreach (NetworkStream ns in streamlist)
                {
                    commands.AddRange((List<Command>)formatter.Deserialize(ns));
                }


                foreach (NetworkStream ns in streamlist)
                {
                    formatter.Serialize(ns, commands);
                }

                //i = 0;
                //streamlist.Clear();
                action(commands);
               
            }
           public delegate void Result(List<Command> c);
        }
    }
}