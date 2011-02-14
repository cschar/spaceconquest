using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;


namespace ChatRoom
{
    public class ChatServer
    {
        //this hash table stores users and connections(browsable by user)
        public static Hashtable htUsers = new Hashtable(30); ///30 users at one time
        //this hash table stores connections and users (browsable by connection)
        public static Hashtable htConnections = new Hashtable(30); //30 users at one time limit
        //Will store the IP address passed to it                                            
        private IPAddress ipAddress;
        private TcpClient tcpClient;
        //The event and its argument will notify the form when a user has connected, disconnected, send message
        public static event StatusChangedEventHandler StatusChanged;
        private static StatusChangedEventArgs e;


        //The constructor sets the IP address to the one retreived by the instantiating object.
        public ChatServer(IPAddress address)
        {
            ipAddress = address; 
        }
        
        //The thread that will hold the connection listener
        private Thread thrListener;

        //The TCP object that listens for the connections
        private TcpListener tlsClient;

        //Will tell the while loop to keep monitoring for connections
        bool ServRunning = false;

        //Add the user to the hash tables
        public static void AddUser(TcpClient tcpUser, string strUsername)
        {
            //First add the username and associated connection to both hash tablse
            ChatServer.htUsers.Add(strUsername, tcpUser);
            ChatServer.htConnections.Add(tcpUser, strUsername);

            //Let all other users and the server form know about new connection
            SendAdminMessage(htConnections[tcpUser] + "has joined us");
        }

        //Remove the user from the hashTables
        public static void RemoveUser(TcpClient tcpUser)
        {

            //If the user is there
            if (htConnections[tcpUser] != null)
            {
                //First show the information and tell the others users about the disconnection
                SendAdminMessage(htConnections[tcpUser] + " has left us");

                //Remove the user from the hash table
                ChatServer.htUsers.Remove(ChatServer.htConnections[tcpUser]);
                ChatServer.htConnections.Remove(tcpUser);

            }
        }//end RemoveUser

        public static void OnStatusChanged(StatusChangedEventArgs e)
        {
            StatusChangedEventHandler statusHandler = StatusChanged;
            if (statusHandler != null)
            {
                //Invoke the delegate
                statusHandler(null, e);
            }
        }


        //Send administrative messages
        public static void SendAdminMessage(string Message)
        {
            StreamWriter swSenderSender;

            //First of all, show in our application who says what
            e = new StatusChangedEventArgs("Administrator: " + Message);
            OnStatusChanged(e);

            //Create an array of TCP clients, the size of then umber of users we have
            TcpClient[] tcpClients = new TcpClient[ChatServer.htConnections.Count];
            //Copy the TcpClient objects into the array
            ChatServer.htUsers.Values.CopyTo(tcpClients, 0);
            //Loop through the list of TCP clients
            for (int i = 0; i < tcpClients.Length; i++)
            {
                //Try sending a message to each
                try
                {
                    //If the message is blank or the connection is null, break out
                    if (Message.Trim() == "" || tcpClients[i] == null)
                    {
                        continue;
                    }
                    //send the message to the current user in the loop
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine("Administrator: " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch (Exception e1)
                {
                    //If there was a problem, the user is not there anymore, remove them
                    RemoveUser(tcpClients[i]);
                }
            }
        }//end SendAdminMessage


        //send message to user
        public static void SendMessage(string From, string Message)
        {
            StreamWriter swSenderSender;

            //First of all, show in our application who says what
            e = new StatusChangedEventArgs(From + " says: " + Message);
            OnStatusChanged(e);

            //Create an array of TCP clients, the size of the number of users we have
            TcpClient[] tcpClients = new TcpClient[ChatServer.htUsers.Count];
            //Copy the TcpClient objects into the array
            ChatServer.htUsers.Values.CopyTo(tcpClients, 0);
            //Loop through the list of TCP clients
            for (int i = 0; i < tcpClients.Length; i++)
            {
                //try sending a message to each
                try
                {
                    //if the message is blank or the connection is null, break out
                    if (Message.Trim() == "" || tcpClients[i] == null)
                        continue;

                    //Send the message to the current user in the loop
                    swSenderSender = new StreamWriter(tcpClients[i].GetStream());
                    swSenderSender.WriteLine(From + " says: " + Message);
                    swSenderSender.Flush();
                    swSenderSender = null;
                }
                catch // If there was a problem, the user is not there anymore, remove them
                {
                    RemoveUser(tcpClients[i]);
                }
            }
        }

        private int Port = 1986;
        public void StartListening()
        {
            //Get the IP of the first network device, however this can prove 
            //unreliable on certain configurations
            IPAddress ipaLocal = ipAddress;
           // ipaLocal = IPAddress.Parse("127.0.0.1");
            try
            {
                //Create the TCP listener object using the IP of the server and the specified port
                tlsClient = new TcpListener(ipaLocal, Port);


                //Start the TCP Listener and listen for connections
                tlsClient.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("yeah ");
                return;
            }

            //The while loop will check for true in this before 
            //checking for connections

            ServRunning = true;

            //Start the new thread that hosts the listener
            thrListener = new Thread(new ThreadStart(KeepListening));
            thrListener.Start();


        }

        private void KeepListening()
        {
            //While the server is running
            while (ServRunning == true)
            {
                //Accept a pending connection
                tcpClient = tlsClient.AcceptTcpClient();
                //Create a new isntance of Connection
                Connection newConnection = new Connection(tcpClient);
            }
        }//end KeepListening

    }




    public class StatusChangedEventArgs : EventArgs
    {
        // The argument we're interested in is a message describing the event
        private string EventMsg;
        // Property for retrieving and setting the event message

        public string EventMessage
        {
            get
            {
                return EventMsg;
            }
            set
            {
                EventMsg = value;
            }
        }

        public StatusChangedEventArgs(string strEventMsg)
        {
            EventMsg = strEventMsg;
        }
    }

    // This delegate is needed to specify the parameters we're passing with our event
    public delegate void StatusChangedEventHandler(object sender, StatusChangedEventArgs e);

}
