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

    class Hex3D
    {
        Color color = Color.Green;
        public int x;
        public int y;
        SolarSystem3D hexgrid;
        public static int radius = HexModel.radius;
        public static int spacing = HexModel.spacing;

        //used to test the bounds for mouse projection
        BoundingSphere boundsphere;


       

        public Hex3D(int xx, int yy, SolarSystem3D ss)
        {
            x = xx;
            y = yy;
            hexgrid = ss;

            boundsphere = new BoundingSphere(getCenter(), radius);

            
        }

        public Vector3 getCenter()
        {
            
            float xshift = (float)Math.Cos(Math.PI / (double)6) * radius+spacing;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * radius+spacing;

            return new Vector3(   (this.x * xshift * 2) + (xshift*this.y) , (yshift + radius) * this.y, 1);

        }

        public void Update(Ray mouseray)
        {
            if (mouseray.Intersects(boundsphere) != null) { color = Color.Red; }
            else color = Color.Green;
        }

        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            world.Translation = world.Translation + getCenter();
           


            HexModel.Draw(world, view, projection, color);
        }

        
    }
}

