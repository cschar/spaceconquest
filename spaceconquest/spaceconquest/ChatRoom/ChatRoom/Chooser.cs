using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ChatRoom
{
    public partial class Chooser : Form
    {
        public Chooser()
        {
            InitializeComponent();
            this.Text = "Chat Chooser";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            //Application.Run(new Form1());
            Form1 f = new Form1();
            f.Show();
            f.Disposed += new EventHandler(f_Disposed);
        }

        void f_Disposed(object sender, EventArgs e)
        {
            this.Show();    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ChatClient c = new ChatClient();
            c.Show();
            c.Disposed += new EventHandler(f_Disposed);
        }
    }
}
