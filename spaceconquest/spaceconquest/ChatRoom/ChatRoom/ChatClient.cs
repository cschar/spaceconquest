using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;


namespace ChatRoom
{
    public partial class ChatClient : Form
    {
        private String username = "uknown";
        private StreamWriter swSender;
        private StreamReader srReceiver;
        private TcpClient tcpServer;

        //need to update the form with messages from another thread
        private delegate void UpdateLogCallback(string strMessage);
        //Needed to set the from to a "disconnected state from another thread
        private delegate void CloseConnectionCallback(string strReason);
        private Thread thrMessaging;
        private IPAddress ipAddr;
        private bool Connected;


        public ChatClient()
        {
            InitializeComponent();
            // On application exit, don't forget to disconnect first
            Application.ApplicationExit += new EventHandler(OnApplicationExit);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //if we are not currently connected but awaiting to connect
            if (Connected == false)
            {
                InitializeConnection();
            }
            else
            {
                CloseConnection("Disconnected at user's request.");
            }
        }


        private void InitializeConnection()
        {
            //Parse the IP address from the TextBox into an IPAddress object
            try
            {
                ipAddr = IPAddress.Parse(txtIP.Text);
            }
            catch (Exception e)
            {
                txtLog.AppendText(e.Message + " \n");
                return;
            }
            //Start a new TCP connection to the chat server
            tcpServer = new TcpClient();

            tcpServer.Connect(ipAddr, 1986);

            //help us track whether we are connected or not
            Connected = true;
            //Prepare the form
            username = txtUser.Text;
            this.Text = "chat Client =-=- " + username;
            //Disable the appropriate fields
            txtIP.Enabled = false;
            txtUser.Enabled = false;
            txtMessage.Enabled = true;
            btnSend.Enabled = true;
            btnConnect.Text = "Disconnect";

            //Send the desired username to the server
            swSender = new StreamWriter(tcpServer.GetStream());
            swSender.WriteLine(txtUser.Text);
            swSender.Flush();

            //start the read for receiving messages and further communication
            thrMessaging = new Thread(new ThreadStart(ReceiveMessages));
            thrMessaging.Start();

        }

        private void ReceiveMessages()
        {
            //Receive the response from the server
            srReceiver = new StreamReader(tcpServer.GetStream());
            //if the first character of the response is 1, connection was successful
            string ConReponse = srReceiver.ReadLine();
            if (ConReponse[0] == '1')
            {
                //Update the form to tell it we are now connected
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { "Connected Successfully!" });
            }
            else   //if the first character is not a 1 (probably a 0), the connection was unsuccesful
            {
                string Reason = "Not connected: ";
                //Extract the reason outof the reesponse message. The reason starts at the 3rd character
                Reason += ConReponse.Substring(2, ConReponse.Length - 2);
                //Update the form with the reason why we couldn't connect
                this.Invoke(new CloseConnectionCallback(this.CloseConnection), new object[] { Reason });
                //Exit the method
                return;
            }

            //While we are successfully connected, read incoming lines from the server
            while (Connected)
            {
                //Show hte messages in the log TextBox
                this.Invoke(new UpdateLogCallback(this.UpdateLog), new object[] { srReceiver.ReadLine() });
            }
        }

        private void UpdateLog(string Message)
        {
            txtLog.AppendText(Message + " \r\n");
        }

        // We want to send the message when the Send button is clicked
        private void btnSend_Click(object sender, EventArgs e)
        {
           SendMessage();
        }

        // But we also want to send the message once Enter is pressed

        private void txtMessage_KeyPress(object sender, KeyPressEventArgs e)
        {
            // If the key is Enter
            if (e.KeyChar == (char)13)
            {
              SendMessage();
            }
        }


        // Sends the message typed in to the server

        private void SendMessage()
        {
            if (txtMessage.Lines.Length >= 1)
            {
                swSender.WriteLine(txtMessage.Text);
                swSender.Flush();
                txtMessage.Lines = null;
            }
            txtMessage.Text = "";
        }

        // Closes a current connection

        private void CloseConnection(string Reason)
        {
            // Show the reason why the connection is ending
            txtLog.AppendText(Reason + "\r\n");

            // Enable and disable the appropriate controls on the form
            txtIP.Enabled = true;
            txtUser.Enabled = true;
            txtMessage.Enabled = false;
            btnSend.Enabled = false;
            btnConnect.Text = "Connect";

            // Close the objects
            Connected = false;
            swSender.Close();
            srReceiver.Close();
            tcpServer.Close();
        }

        // The event handler for application exit

        public void OnApplicationExit(object sender, EventArgs e)
        {
            if (Connected == true)
            {
                // Closes the connections, streams, etc.
                Connected = false;
                swSender.Close();
                srReceiver.Close();
            }
        }

        private void ChatClient_Load(object sender, EventArgs e)
        {

        }


    }//end ChatClient class

}//end namespace
