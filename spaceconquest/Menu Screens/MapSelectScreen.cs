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
    class MapSelectScreen : Screen
    {
        public List<MenuComponent> components;
        private MouseState mousestateold = Mouse.GetState();

        public MapSelectScreen(SpriteBatch sb, SpriteFont sf)
        {
            components = new List<MenuComponent>();
            components.Add(new MenuButton(new Rectangle(450, 200, 150, 40), sb, sf, "Load Map",  null ));
            components.Add(new MenuButton(new Rectangle(450, 250, 150, 40), sb, sf, "New Map", MenuManager.ClickNewGame));

            MenuList maplist = new MenuList(new Rectangle(200, 200, 200, 300), sb, sf);
            maplist.AddNewButtonDefault(20, 200, "i am a map", delegate(Object o, EventArgs e) { ((MenuButton)o).selected = !((MenuButton)o).selected; });
            maplist.AddNewButtonDefault(20, 200, "i am also a map", delegate(Object o, EventArgs e) { ((MenuButton)o).selected = !((MenuButton)o).selected; });
            components.Add(maplist);
        }


        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            foreach (MenuComponent mb in components)
            {
                mb.Update(mousestate, mousestateold);
            }
            mousestateold = mousestate;
        }

        public void Draw()
        {
            foreach (MenuComponent mb in components)
            {
                mb.Draw();
            }
        }
    }
}
