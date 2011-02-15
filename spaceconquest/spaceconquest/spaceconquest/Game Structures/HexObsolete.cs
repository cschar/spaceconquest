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
    class Hex
    {
        public int x;
        public int y;
        SolarSystem hexgrid;
        public static int radius = 196/2;
        public static int spacing = 5;
        public Vector2 position;


        public Hex(int xx, int yy, SolarSystem ss)
        {
            x = xx;
            y = yy;
            hexgrid = ss;


            position = getScreenCenter(Point.Zero);
           
        }

        public Vector2 getScreenCenter(Point offset)
        {
            
            float xshift = (float)Math.Cos(Math.PI / (double)6) * radius + spacing;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * radius + spacing;

            return new Vector2(  offset.X + (this.x * xshift * 2) + (xshift * this.y), offset.Y + (yshift + radius) * this.y);
        }



        public void Draw()
        {
            MenuManager.batch.Draw(Game1.hextexture, position, Color.Green);
        }


        //public void DrawReallyCrooked()
        //{
        //    Point offset = Point.Zero;
        //    offset = new Point(100, 100);
        //    Point center = getScreenCenter(offset);
        //    int xshift = (int) Math.Round((Math.Cos(Math.PI / (double)3) * pixelradius));
        //    int yshift = (int) Math.Round((Math.Sin(Math.PI / (double)3) * pixelradius)); 
        //    MenuManager.batch.Draw (texture, new Rectangle(center.X + pixelradius, center.Y, pixelradius, 3), null, Color.Blue, (float)((double)2*Math.PI/(double)3), new Vector2 (0f, 0f), SpriteEffects.None, 1f);
        //    MenuManager.batch.Draw (texture, new Rectangle(center.X - xshift, center.Y + yshift, pixelradius, 3), Color.Blue);
        //    MenuManager.batch.Draw(texture, new Rectangle(center.X - pixelradius, center.Y, pixelradius, 3), null, Color.Blue, (float)((double)Math.PI / (double)3), new Vector2(0f, 0f), SpriteEffects.None, 1f);

        //    MenuManager.batch.Draw(texture, new Rectangle(center.X - pixelradius, center.Y, pixelradius, 3), null, Color.Blue, -(float)((double)Math.PI / (double)3), new Vector2(0f, 0f), SpriteEffects.None, 1f);
        //    MenuManager.batch.Draw (texture, new Rectangle(center.X - xshift, center.Y - yshift, pixelradius, 3), Color.Blue);
        //    MenuManager.batch.Draw (texture, new Rectangle(center.X + pixelradius, center.Y, pixelradius, 3), null, Color.Blue, -(float)((double)2 * Math.PI / (double)3), new Vector2(0f, 0f), SpriteEffects.None, 1f);
        //}

        
    }
}
