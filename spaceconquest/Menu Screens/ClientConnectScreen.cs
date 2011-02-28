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
    class ClientConnectScreen : Screen
    {
        List<MenuComponent> components = new List<MenuComponent>();
        private MouseState mousestateold = Mouse.GetState();
        MenuList chatlist;
        IPAddress ip;
        EndPoint end;
        Socket listensocket;


        //public ClientConnectScreen(String ipstring)
        //{
        //    ip = IPAddress.Parse(ipstring);
        //    end = new IPEndPoint(ip, 6114);
        //    listensocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //    chatlist = new MenuList(new Rectangle(50, 50, 350, 450));
        //    components.Add(new TextInput(new Rectangle(50, 500, 350, 40), ChatSend));
        //    components.Add(chatlist);

        //   HostThread ht = new HostThread(listensocket, end, chatlist, "127.0.0.1");
        //   Thread t2 = new Thread(new ThreadStart(ht.SendRecieve));
        //   t2.Start();
        //}

        public void ChatSend(String input)
        {
            chatlist.AddNewTextLineDefault(20, 200, input);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            end = new IPEndPoint(ip, 6113);

            MessageThread mt = new MessageThread(socket, end, input);
            Thread t = new Thread(new ThreadStart(mt.SendRecieve));
            t.Start();
        }


        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            foreach (MenuComponent mb in components)
            {
                mb.Update(mousestate, mousestateold);
            }
            mousestateold = mousestate;
        }

        public void Draw()
        {
            foreach (MenuComponent mb in components)
            {
                mb.Draw();
            }
        }

        public class MessageThread
        {
            Socket socket;
            EndPoint end;
            String message;
            NetworkStream stream;
            BinaryFormatter formatter;

            public MessageThread(Socket s, EndPoint e, String input)
            {
                socket = s;
                end = e;
                message = input;
                formatter = new BinaryFormatter();
            }

            public void SendRecieve()
            {
                socket.Connect(end);
                stream = new NetworkStream(socket);
                Console.WriteLine("Connected to host");

                formatter.Serialize(stream, message);
            }
        }

        public class HostThread
        {
            Socket socket;
            Socket accept;
            EndPoint end;
            String message;
            NetworkStream stream;
            BinaryFormatter formatter;
            MenuList chatlist;
            String ipstring;

            public HostThread(Socket s, EndPoint e, MenuList cl, String ip)
            {
                socket = s;
                end = e;
                socket.Bind(end);
                formatter = new BinaryFormatter();
                chatlist = cl;
                ipstring = ip;
            }

            public void SendRecieve()
            {
                while (true)
                {
                    while (true)
                    {
                        socket.Listen(10);
                        Console.WriteLine("Host Listening");
                        accept = socket.Accept();
                        //accept.Listen(10);
                        Console.WriteLine("Host Accepted");
                        break;
                    }
                    stream = new NetworkStream(accept);
                    message = (String)formatter.Deserialize(stream);
                    chatlist.AddNewTextLineDefault(20, 200, message);
                    if (message.Equals("!start")) {MenuManager.ForceJoinGame( ipstring ,EventArgs.Empty);}
                }
            }
        }
    }
}
