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
        public static int pixelradius = 50;
        Texture2D texture;


        public Hex(int xx, int yy, SolarSystem ss)
        {
            x = xx;
            y = yy;
            hexgrid = ss;

            texture = new Texture2D(Game1.device, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            
           
        }

        public Point getScreenCenter(Point offset)
        {
            int diagonal = (int)((Hex.pixelradius * 2) / (Math.Sqrt(2)));
            return new Point(offset.X + this.x * Hex.pixelradius + this.y * diagonal,offset.Y + this.y * diagonal);
        }



        public void Draw()
        {

        }


        public void DrawReallyCrooked()
        {
            Point offset = Point.Zero;
            offset = new Point(100, 100);
            Point center = getScreenCenter(offset);
            int xshift = (int) Math.Round((Math.Cos(Math.PI / (double)3) * pixelradius));
            int yshift = (int) Math.Round((Math.Sin(Math.PI / (double)3) * pixelradius)); 
            MenuManager.batch.Draw (texture, new Rectangle(center.X + pixelradius, center.Y, pixelradius, 3), null, Color.Blue, (float)((double)2*Math.PI/(double)3), new Vector2 (0f, 0f), SpriteEffects.None, 1f);
            MenuManager.batch.Draw (texture, new Rectangle(center.X - xshift, center.Y + yshift, pixelradius, 3), Color.Blue);
            MenuManager.batch.Draw(texture, new Rectangle(center.X - pixelradius, center.Y, pixelradius, 3), null, Color.Blue, (float)((double)Math.PI / (double)3), new Vector2(0f, 0f), SpriteEffects.None, 1f);

            MenuManager.batch.Draw(texture, new Rectangle(center.X - pixelradius, center.Y, pixelradius, 3), null, Color.Blue, -(float)((double)Math.PI / (double)3), new Vector2(0f, 0f), SpriteEffects.None, 1f);
            MenuManager.batch.Draw (texture, new Rectangle(center.X - xshift, center.Y - yshift, pixelradius, 3), Color.Blue);
            MenuManager.batch.Draw (texture, new Rectangle(center.X + pixelradius, center.Y, pixelradius, 3), null, Color.Blue, -(float)((double)2 * Math.PI / (double)3), new Vector2(0f, 0f), SpriteEffects.None, 1f);
        }

        
    }
}
