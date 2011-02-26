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
    class MenuButton : MenuComponent
    {
        Rectangle area;
        SpriteBatch batch = MenuManager.batch;
        SpriteFont font = MenuManager.font;
        String text;
        Texture2D texture;
        Vector2 stringvector;
        Color normalcolor;
        Color clickedcolor;
        Color selectedcolor;
        Color currentcolor;
        public bool selected = false;

        public MenuButton(Rectangle r, String t, EventHandler c)
        {
            area = r;
            text = t;
            texture = new Texture2D(batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            stringvector = new Vector2(area.Center.X, area.Center.Y) - (font.MeasureString(text) / 2);

            currentcolor = normalcolor = Color.Teal;
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
            return area.Contains(x,y);
        }


        public override void Update(MouseState mscurrent, MouseState msold)
        {
           if (this.Contains(mscurrent.X, mscurrent.Y))
           {
               if ((mscurrent.LeftButton == ButtonState.Released) && (msold.LeftButton == ButtonState.Pressed)) { Console.WriteLine("derp"); OnClick(EventArgs.Empty); }
               else if (mscurrent.LeftButton == ButtonState.Pressed) { currentcolor = clickedcolor; }
               else { currentcolor = selectedcolor; }
           }
           else { if(!selected) currentcolor = normalcolor; }
           
        }

        public override void Draw()
        {
            batch.Draw(texture, area, currentcolor);
            batch.DrawString(font, text, stringvector, Color.Yellow);
        }


    }
}
