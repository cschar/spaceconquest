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
using System.IO;

namespace spaceconquest
{
    class IconButton : MenuComponent
    {

        Rectangle area;
        //SpriteBatch batch = MenuManager.batch;
        public readonly static SpriteBatch batch = new SpriteBatch(Game1.device);
        SpriteFont font = MenuManager.font;
        String text;
        Texture2D texture;
        Texture2D texture2;
        Vector2 stringvector;
        Color normalcolor;
        Color clickedcolor;
        Color selectedcolor;
        Color currentcolor;
        public bool selected = false;
        public bool toggle = false;

        public IconButton(Rectangle r, String t, EventHandler c)
        {
            area = r;
            text = t;
            //Console.WriteLine(Game1.contentManager.RootDirectory);
            FileStream fs = new FileStream(@"Content/Buttons/" + t, FileMode.Open);
            texture = Texture2D.FromStream(Game1.device, fs);
            stringvector = new Vector2(area.Center.X, area.Center.Y) - (font.MeasureString(text) / 2);
            

            currentcolor = normalcolor = Color.White; 
            clickedcolor = Color.Blue;
            selectedcolor = Color.Red;
            addClickEvent(c);
        }

        public IconButton(Rectangle r, String t, String t2, EventHandler c)
        {
            area = r;
            text = t;
            //Console.WriteLine(Game1.contentManager.RootDirectory);
            FileStream fs = new FileStream(@"Content/Buttons/" + t, FileMode.Open);
            texture = Texture2D.FromStream(Game1.device, fs);

            FileStream fs2 = new FileStream(@"Content/Buttons/" + t2, FileMode.Open);
            texture2 = Texture2D.FromStream(Game1.device, fs2);

            stringvector = new Vector2(area.Center.X, area.Center.Y) - (font.MeasureString(text) / 2);


            currentcolor = normalcolor = Color.White;
            clickedcolor = Color.Blue;
            selectedcolor = Color.Red;
            addClickEvent(c);
        }

        public bool Contains(Point p)
        {
            return area.Contains(p);
        }

        public override bool Contains(int x, int y)
        {
            return area.Contains(x, y);
        }


        public override void Update(MouseState mscurrent, MouseState msold)
        {
            if (this.Contains(mscurrent.X, mscurrent.Y))
            {
                if ((mscurrent.LeftButton == ButtonState.Released) && (msold.LeftButton == ButtonState.Pressed)) { Console.WriteLine("derp"); OnClick(EventArgs.Empty); }
                else if (mscurrent.LeftButton == ButtonState.Pressed) { currentcolor = clickedcolor; }
                else { currentcolor = selectedcolor; }
            }
            else { if (!selected) currentcolor = normalcolor; }

        }

        public override void Draw()
        {
            //batch.Draw(
            //MenuManager.batch.Draw(texture, area, currentcolor);
            if (toggle) {IconButton.batch.Draw(texture2, area, currentcolor);}
            else IconButton.batch.Draw(texture, area, currentcolor);
            //batch.DrawString(font, text, stringvector, Color.Yellow);
        }


    }
}
