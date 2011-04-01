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
     class StatusMenu
    {
        Unit selectedunit;
        protected Rectangle area;
        protected List<MenuComponent> menucomponents = new List<MenuComponent>();
        TextLine nameline;
        TextLine healthline;
        //TextLine
        Texture2D texture;
        Color currentcolor = new Color(0,0,0,150);
        //Color barcolor = Color.FromNonPremultiplied(130, 245, 100, 100);
        public bool visible = true;
        public bool showbackround = true;

        public StatusMenu(Rectangle r)
        {
            area = r;
            texture = new Texture2D(MenuManager.batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            nameline = new TextLine(new Rectangle(area.Left + 5, area.Top + 10, 200, 20), "dummy");
            healthline = new TextLine(new Rectangle(area.Left + 5, area.Top + 30, 200, 20), "dummy2");
            menucomponents.Add(nameline);
            menucomponents.Add(healthline);
        }

        public void Update(Unit u) 
        {
            selectedunit = u;
            nameline.text = u.GetType().Name;
            healthline.text = "Health: " + u.getHealth() + "/" + u.getMaxHealth();
        }

        public void Draw()
        {
            if (visible)
            {
                if (showbackround) MenuManager.batch.Draw(texture, area, currentcolor);
                foreach (MenuComponent mc in menucomponents)
                {
                    mc.Draw();
                }
            }
        }

        public void Hide()
        {
            visible = false;
        }

        public void Show()
        {
            visible = true;
        }
    }
}
