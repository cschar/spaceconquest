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
    class Client : MiddleMan
    {
        List<Command> commands = new List<Command>();
        IPAddress ip;
        EndPoint end, end2;
        Socket socket;
        Socket aSock;
        SlaveDriver slavedriver;
        bool done = false;
        bool busy = false;
        GameScreen gs;
        AttendanceThread at;

        public Client(String ipstring, SlaveDriver sd, GameScreen GS)
        {
            gs = GS;
            ip = IPAddress.Parse(ipstring);
            end = new IPEndPoint(ip, 6112);
            end2 = new IPEndPoint(ip, 6113);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            aSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
            slavedriver = sd;
            //socket.EnableBroadcast = false;
            ReceiveMap();
            //Start attendance thread;
            TakeAttendance();
            
            
        }

        public void cb(Socket s) { s.Dispose(); gs.Save(); gs.Quit(); Console.WriteLine("foo bar baz 2"); return; }

        public void TakeAttendance() {
            at = new AttendanceThread(aSock, end2, cb);
            Thread t = new Thread(new ThreadStart(at.Run));
            t.Start();
        }

        public void AttendClose() {
            at.exit();
        }

        public void Close()
        {
            socket.Dispose();
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

        public void ReceiveMap()
        {
            ClientThread ct = new ClientThread(socket, end, slavedriver, ReturnCommands);
            commands = new List<Command>();
            //Thread t = new Thread(new ThreadStart(ct.SendRecieve));
            //t.Start();

            //blocking!
            ct.SendRecieve();
        }

        public void EndTurn()
        {
            if (busy) { return; }
            busy = true;
            done = false;
            ClientThread ct = new ClientThread(socket, end, commands, ReturnCommands);
            commands = new List<Command>();
            Thread t = new Thread(new ThreadStart(ct.SendRecieve));
            t.Start();
        }

        private void ReturnCommands(List<Command> c)
        {
            slavedriver.Receive(c);
            done = true;
        }

        private void SaveAndQuit() { 
            //
        }

        public class AttendanceThread {
            Socket socko;
            EndPoint ep;
            public delegate void DisconnectCallback(Socket s);
            Byte[] bPing = System.Text.Encoding.ASCII.GetBytes("ping");
            DisconnectCallback concreteDCB;
            Byte[] recBuff = new Byte[10];
            Boolean cont = true;

            public void exit() {
                cont = false;
            }

            public AttendanceThread(Socket s, EndPoint e, DisconnectCallback dcb) {
                socko = s;
                socko.ReceiveTimeout = 0;
                socko.SendTimeout = 0;
                ep = e;
                concreteDCB = dcb;
            }

            public void Run() {
                connectToHost();
                Attendance();
            }

            public void connectToHost() {
                socko.Connect(ep);
                
            }

            public void Attendance() {
                //socko.Receive(recBuff);

                while (cont) {
                    Thread.Sleep(10000);
                    try
                    {
                        socko.Send(bPing);
                        socko.SendTimeout = 10000;
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine(se.Message);
                        concreteDCB(socko);
                        break;
                    }
                }  
            }


        }

        public class ClientThread
        {
            Socket socket;
            NetworkStream stream;
            EndPoint end;
            BinaryFormatter formatter;
            List<Command> commands;
            Result action;
            SlaveDriver slave;
            bool mapexchange = false;

            public ClientThread(Socket s, EndPoint e, SlaveDriver sd, Result r)
            {
                socket = s;
                end = e;
                //socket.Bind(end);
                formatter = new BinaryFormatter();
                slave = sd;
                action = r;
                mapexchange = true;
            }

            public ClientThread(Socket s, EndPoint e, List<Command> c, Result r)
            {
                socket = s;
                end = e;
                //socket.Bind(end);
                formatter = new BinaryFormatter();
                commands = c;
                action = r;
            }

            public void SendRecieve()
            {

                if (mapexchange)
                {
                    //try
                    //{
                        if (!socket.Connected) { socket.Connect(end); }
                        Console.WriteLine("Connected to host");
                        stream = new NetworkStream(socket);
                        slave.SetMap((Map)formatter.Deserialize(stream));
                        return;
                    //}
                    //catch (SocketException e) { Console.WriteLine(e); SendRecieve(); }
                }

                try
                {
                    if (!socket.Connected) { socket.Connect(end); }
                    Console.WriteLine("Connected to host");
                    stream = new NetworkStream(socket);

               
                    formatter.Serialize(stream, commands);
                    Console.WriteLine("Sent Commands");

                    commands = (List<Command>)formatter.Deserialize(stream);
                    Console.WriteLine("Recieved Commands");

                    //Console.WriteLine("Disconnecting Socket");
                    //socket.Disconnect(true);
                    // Console.WriteLine("Disconnected");
                    action(commands);
                }
                catch (Exception e) { Console.WriteLine(e.Message); socket.Dispose(); MenuManager.ClickTitle(this, EventArgs.Empty); return; }
            }
            public delegate void Result(List<Command> c);
        }
    }
}