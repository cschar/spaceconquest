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
        Hex3D[,] hexmap;
        int radius;

        public SolarSystem(int r, Rectangle sa)
        {
            screenarea = sa;
            radius = r;
            hexmap = new Hex3D[(radius * 2)+1, (radius * 2)+1];


            for (int i = 0; i < (radius * 2)+1; i++)
            {
                for (int j = 0; j < (radius * 2)+1; j++)
                {
                    hexmap[i, j] = new Hex3D(i - radius, j - radius, this);
                }
            }
        }

        public Hex3D getHex(int x, int y)//was gonna use lazy initialization
        {
            if (hexmap[x + radius, y + radius] == null) { hexmap[x + radius, y + radius] = new Hex3D(x, y, this); }
            return hexmap[x + radius, y + radius];
        }



        public void Draw()
        {
            //3d stuff
            //float time = (float)gameTime.TotalGameTime.TotalSeconds;

            //float yaw =  time * 0.4f;
            //float pitch = time * 0.7f;
           // float roll = time * 1.1f;

            Vector3 cameraPosition = new Vector3(0, 0, 10f);

            float aspect = Game1.device.Viewport.AspectRatio;

            Matrix world = Matrix.Identity;
            //Matrix world = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
            Matrix view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 20);
            Matrix orthog = Matrix.CreateOrthographic(800, 600, 1, 20);

            Color color = Color.Green;



            foreach (Hex3D h in hexmap)
            {
                //if (screenarea.Contains((int)h.getCenter().X, (int)h.getCenter().Y)) { h.Draw(world, view, orthog, color); }
                h.Draw(world, view, orthog, color);
            }
        }
    }
}
