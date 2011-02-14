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
    class GlobalLobbyScreen : Screen
    {
        public List<MenuComponent> buttons;
        private MouseState mousestateold = Mouse.GetState();

        public GlobalLobbyScreen(SpriteBatch sb, SpriteFont sf)
        {
            buttons = new List<MenuComponent>();
            //buttons.Add(new MenuButton(new Rectangle(325, 200, 150, 40), sb, sf, "Join Game", new MenuButton.ClickActionDelegate(MenuManager.ClickClientConnect) ));
            buttons.Add(new MenuButton(new Rectangle(325, 250, 150, 40), sb, sf, "Host Game", MenuManager.ClickMapSelect ));
            buttons.Add(new TextInput(new Rectangle(325,300,150,40)));
        }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            foreach (MenuComponent mb in buttons)
            {
                mb.Update(mousestate, mousestateold);
            }
            mousestateold = mousestate;
        }

        public void Draw()
        {
            foreach (MenuComponent mb in buttons)
            {
                mb.Draw();
            }
        }
    }
}
