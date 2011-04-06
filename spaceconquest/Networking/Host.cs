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
        EndPoint end, end2;
        Socket listensocket;
        Socket aSocket;
        SlaveDriver slavedriver;
        bool done = false;
        bool busy = false;
        int numclients;
        Map map;
        GameScreen gs;

        public Host(Map m, SlaveDriver sd, int n, GameScreen GS)
        {
            AttendanceThread.socklist = new List<Socket>();
            HostThread.streamlist = new List<NetworkStream>();
            gs = GS;
            ip = IPAddress.Any; //IPAddress.Parse("70.55.141.164");
            end = new IPEndPoint(ip, 6112);
            end2 = new IPEndPoint(ip, 6113);
            listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            aSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listensocket.Bind(end);
            aSocket.Bind(end2);
            //listensocket.EnableBroadcast = false;
            slavedriver = sd;
            slavedriver.SetMap(m);
            numclients = n;
            map = m;
            SendMap();
            TakeAttendance();


        }

        public void cb(Socket s, Socket a) { a.Dispose(); s.Dispose(); gs.Save(); gs.Quit(); Console.WriteLine("foo bar baz 2"); return; }
        public void TakeAttendance()
        {
            AttendanceThread at = new AttendanceThread(aSocket, end2, numclients, cb);
            Thread t = new Thread(new ThreadStart(at.Run));
            t.Start();
        }

        public void Close()
        {
            listensocket.Dispose();
            aSocket.Dispose();
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

        public void SendMap()
        {
            if (busy) { return; }
            //busy = true;
            //done = false;
            HostThread ht = new HostThread(listensocket, end, map, ReturnCommands, numclients);
            commands = new List<Command>();
            //Thread t = new Thread(new ThreadStart(ht.SendRecieve));
            //t.Start();

            //blocking!
            ht.SendRecieve();
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

        public class AttendanceThread
        {
            int numclients;
            Socket acco;
            public static List<Socket> socklist = new List<Socket>();
            Socket socko;
            EndPoint ep;
            public delegate void DisconnectCallback(Socket s, Socket a);
            Byte[] bPing = System.Text.Encoding.ASCII.GetBytes("ping");
            DisconnectCallback concreteDCB;
            Byte[] recBuff = new Byte[10];

            public AttendanceThread(Socket s, EndPoint e, int num, DisconnectCallback dcb)
            {
                numclients = num;
                socko = s;
                socko.ReceiveTimeout = 10000;   
                ep = e;
                concreteDCB = dcb;
            }

            public void Run() {
                connectToClients();
                Attendance();
            }

            public void connectToClients()
            {

                while (socklist.Count < numclients)
                {
                    while (true)
                    {
                        socko.Listen(1);
                        Console.WriteLine("Host Attendance Listening");
                        acco = socko.Accept();
                        //accept.Listen(10);
                        Console.WriteLine("Host Attendance Accepted");
                        break;
                    }

                    socklist.Add(acco);
                }
            }

            public void Attendance()
            {
                //socko.Receive(recBuff);
                Boolean foo = false;
                while (true)
                {
                    Thread.Sleep(10000);
                    try
                    {
                        foreach (Socket sock in socklist) {
                            sock.Receive(recBuff);
                        }
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine(se.Message);
                        concreteDCB(socko, acco);
                        foo = true;
                    }
                    if (foo) {
                        break;
                    }
                }
            }


        }

        public class HostThread
        {
            int numclients;
            Socket socket;
            Socket accept;
            public static List<NetworkStream> streamlist = new List<NetworkStream>();
            EndPoint end;
            BinaryFormatter formatter;
            List<Command> commands;
            Result action;
            Map map;
            bool sendmap = false;

            public HostThread(Socket s, EndPoint e, List<Command> c, Result r, int n)
            {
                socket = s;
                end = e;
                formatter = new BinaryFormatter();
                commands = c;
                action = r;
                numclients = n;
            }

            public HostThread(Socket s, EndPoint e, Map m, Result r, int n)
            {
                socket = s;
                end = e;
                formatter = new BinaryFormatter();
                map = m;
                action = r;
                numclients = n;
                sendmap = true;
                
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

                if (sendmap)
                {
                    //sending map
                    foreach (NetworkStream ns in streamlist)
                    {
                        formatter.Serialize(ns, map);
                    }
                    return;
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