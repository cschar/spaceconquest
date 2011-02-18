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
        private GlobalChatClient chatClient;
        MenuList maplist;

        public GlobalLobbyScreen(SpriteBatch sb, SpriteFont sf)
        {
            chatClient = new GlobalChatClient("http://openportone.appspot.com/");
            buttons = new List<MenuComponent>();
            //buttons.Add(new MenuButton(new Rectangle(325, 200, 150, 40), sb, sf, "Join Game", new MenuButton.ClickActionDelegate(MenuManager.ClickClientConnect) ));
            buttons.Add(new MenuButton(new Rectangle(450, 250, 150, 40), sb, sf, "Host Game", MenuManager.ClickMapSelect ));
            buttons.Add(new TextInput(new Rectangle(50,500,350,40), chatClient));
            maplist = new MenuList(new Rectangle(50, 50, 350, 450), sb, sf);
            buttons.Add(maplist);
        }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            foreach (MenuComponent mb in buttons)
            {
                mb.Update(mousestate, mousestateold);
            }
            mousestateold = mousestate;

            maplist.Clear();
            List<ChatLog> newChats = chatClient.GetLogs();

            foreach (ChatLog c in newChats)
            {
                string finalMessage = "";
                string init = c.message;
                bool nameSaid = false;
                while (init.Length > 31)
                {
                    
                    //finalMessage += init.Substring(0, 25) + "\n";
                    //init = init.Substring(26);
                    finalMessage = init.Substring(0, 31) + "\n";
                    if (nameSaid == false)
                    {
                        maplist.AddNewTextLineDefault(20, 200, c.playerName + finalMessage);
                    }
                    else
                    {
                        maplist.AddNewTextLineDefault(20, 200, "          " + finalMessage);
                    
                    }
                    nameSaid = true;
                    
                    init = init.Substring(31);
                }
                if (nameSaid == false)
                {
                    maplist.AddNewTextLineDefault(20, 200, c.playerName + init);
                }
                else
                {
                    maplist.AddNewTextLineDefault(20, 200, "          " + init);

                }
            }
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
