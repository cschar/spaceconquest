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

namespace spaceconquest
{
    class CommandMenu : MenuList
    {

        public CommandMenu(Rectangle r) : base(r)
        {
            //wierd syntax
            padding = 5;
        }

        public void AddNewCommand(int x, int y, String iconaddress, EventHandler c)
        {
            this.Add(new IconButton(new Rectangle(area.Left + padding + x*(60+padding), area.Top + padding + y*(60+padding), 60, 60), iconaddress, c));
        }

    }
}

