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
    class TitleScreen : Screen
    {   
        public List<MenuButton> buttons;
        private MouseState mousestateold;

        public TitleScreen(SpriteBatch sb,SpriteFont sf)
        {
            buttons = new List<MenuButton>();
            buttons.Add(new MenuButton(new Rectangle(325, 200, 150, 40), "Singleplayer", MenuManager.ClickMapSelect ));
            buttons.Add(new MenuButton(new Rectangle(325, 250, 150, 40), "Multiplayer", MenuManager.ClickGlobalLobby ));
            buttons.Add(new MenuButton(new Rectangle(325, 300, 150, 40), "Sound Options", MenuManager.ClickMusicOptions));
        }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            foreach (MenuButton mb in buttons)
            {
                mb.Update(mousestate, mousestateold);
            }
            mousestateold = mousestate;
        }

        public void Draw()
        {
            foreach (MenuButton mb in buttons)
            {
                mb.Draw();
            }
        }
    }
}
