using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Collections;

namespace ChatRoom
{
    //this class handles connections; there will be as many isntances of it as there will be connected users
    class Connection
    {
        TcpClient tcpClient;
        //The thread that will send information to the client
        private Thread thrSender;
        private StreamReader srReceiver;
        private StreamWriter swSender;
        private string currUser;
        private string strResponse;

        //The constructor of the class takes in a TCP connection
        public Connection(TcpClient tcpCon)
        {
            tcpClient = tcpCon;
            //The thread that accepts the client and awaits messages
            thrSender = new Thread(AcceptClient);
            //The thread calls the Acceptclient() method
            thrSender.Start();

        }

        private void CloseConnection()
        {
            //Close the currently open objects
            tcpClient.Close();
            srReceiver.Close();
            swSender.Close();
        }

        //Occurs when a new client is accepted
        private void AcceptClient()
        {
            srReceiver = new System.IO.StreamReader(tcpClient.GetStream());
            swSender = new System.IO.StreamWriter(tcpClient.GetStream());

            //Read the account information from the client
            currUser = srReceiver.ReadLine();

            //We got a response from the client
            if (currUser != "")
            {
                //Store the user name in the hash table
                if (ChatServer.htUsers.Contains(currUser) == true)
                {
                    //0 means not connected
                    swSender.WriteLine("0|This username already exists.");
                    swSender.Flush();
                    CloseConnection();
                    return;

                }

                else if (currUser == "Administrator")
                {
                    //0 means not connected
                    swSender.WriteLine("0|This username is reserved");
                    swSender.Flush();
                    CloseConnection();
                    return;
                }
                else
                {
                    //1 means connected successfylly
                    swSender.WriteLine("1");
                    swSender.Flush();

                    //Add the user to the hash tables and start listening for messages from them
                    ChatServer.AddUser(tcpClient, currUser);

                }
            }
            else
            {
                CloseConnection();
                return;
            }

            try
            {
                //Keep waiting for a message from the user
                while ((strResponse = srReceiver.ReadLine()) != "")
                {
                    //If its invalid remove the user
                    if (strResponse == null)
                    {
                        ChatServer.RemoveUser(tcpClient);
                    }
                    else
                    {
                        //Otherwise send the message to all the other users
                        ChatServer.SendMessage(currUser, strResponse);
                    }

                }
            }//end try
            catch (Exception e)
            {

                //if anything went wrong with this user, disconnect
                ChatServer.RemoveUser(tcpClient);
            }

        }//end acceptClient() method
    }//end Connectino class
}//end namespace

