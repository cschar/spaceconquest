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
    class SolarSystem
    {
        Rectangle screenarea;
        Hex[,] hexmap;
        int radius;

        public SolarSystem(int r, Rectangle sa)
        {
            screenarea = sa;
            radius = r;
            hexmap = new Hex[(radius * 2)+1, (radius * 2)+1];


            for (int i = 0; i < (radius * 2)+1; i++)
            {
                for (int j = 0; j < (radius * 2)+1; j++)
                {
                    hexmap[i, j] = new Hex(i - radius, j - radius, this);
                }
            }
        }

        public Hex getHex(int x, int y)//was gonna use lazy initialization
        {
            if (hexmap[x + radius, y + radius] == null) { hexmap[x + radius, y + radius] = new Hex(x, y, this); }
            return hexmap[x + radius, y + radius];
        }



        public void Draw()
        {
            foreach (Hex h in hexmap)
            {
                //if (screenarea.Contains((int)h.getCenter().X, (int)h.getCenter().Y)) { h.Draw(world, view, orthog, color); }
                h.Draw();
            }
        }

    }
}
