using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace ChatRoom
{
    public partial class Form1 : Form
    {

        private delegate void UpdateStatusCallback(string strMessage);


        public Form1()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            //Parse the server's IP address out of the TextBox
            IPAddress ipAddr = IPAddress.Parse(txtIp.Text);
            //Create a new instance of the chatserver object
            ChatServer mainServer = new ChatServer(ipAddr);
            //Hook the StatusChanged event handler to mainServer_statusChanged
            ChatServer.StatusChanged += new StatusChangedEventHandler(mainServer_StatusChanged);
            //Start listening for connections
            mainServer.StartListening();
            //show that we started to listen for connections
            txtLog.AppendText("Monitoring for connections... \r\n");
        }



        public void mainServer_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            // Call the method that updates the form
            this.Invoke(new UpdateStatusCallback(this.UpdateStatus), new object[] { e.EventMessage });
        }



        private void UpdateStatus(string strMessage)
        {
            // Updates the log with the message
            txtLog.AppendText(strMessage + "\r\n");
        }

    }
}
