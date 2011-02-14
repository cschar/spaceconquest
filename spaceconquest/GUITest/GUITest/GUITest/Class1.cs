using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace GUITest
{
    class Class1
    {
        public void DoForm()
        {
            Form f = new Form();
            f.Size = new Size(300, 300);
            Application.Run(f);
        }
    }
}
