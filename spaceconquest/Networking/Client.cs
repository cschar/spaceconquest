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
        EndPoint end;
        Socket socket;
        SlaveDriver slavedriver;
        bool done = false;
        bool busy = false;

        public Client(String ipstring, SlaveDriver sd)
        {
            ip = IPAddress.Parse(ipstring);
            end = new IPEndPoint(ip, 6112);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
            slavedriver = sd;
            //socket.EnableBroadcast = false;
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

        public void EndTurn()
        {
            if (busy) { return; }
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

        public class ClientThread
        {
            Socket socket;
            NetworkStream stream;
            EndPoint end;
            BinaryFormatter formatter;
            List<Command> commands;
            Result action;

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
               // try
               // {
                
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
                //}
                //catch (SocketException e) { Console.WriteLine(e); SendRecieve(); }
            }
            public delegate void Result(List<Command> c);
        }
    }
}