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
using System.Runtime.Serialization.Formatters.Binary;


namespace spaceconquest
{
    class MapSelectScreen : Screen
    {
        public List<MenuComponent> components;
        private MouseState mousestateold = Mouse.GetState();
        String loadpath;
        bool host = false;


        public MapSelectScreen(bool h)
        {
            host = h;
            components = new List<MenuComponent>();
            components.Add(new MenuButton(new Rectangle(450, 200, 150, 40), "Load Map", ClickLoadMapStart));
            components.Add(new MenuButton(new Rectangle(450, 250, 150, 40), "New Map", ClickNewGameStart));

            MenuList maplist = new MenuList(new Rectangle(200, 200, 200, 300));
            //maplist.AddNewButtonDefault(20, 200, "i am a map", delegate(Object o, EventArgs e) { ((MenuButton)o).selected = !((MenuButton)o).selected; });
            //maplist.AddNewButtonDefault(20, 200, "i am also a map", delegate(Object o, EventArgs e) { ((MenuButton)o).selected = !((MenuButton)o).selected; });

            string[] filepaths = Directory.GetFiles(@"Content/", "*.map", SearchOption.AllDirectories);
            foreach (String s in filepaths)
            {
                maplist.AddNewButtonDefault(20, 200, s, delegate(Object o, EventArgs e) { ((MenuButton)o).selected = !((MenuButton)o).selected; loadpath = ((MenuButton)o).text;});
            }

            components.Add(maplist);
        }

        public void ClickNewGameStart(Object o, EventArgs e)
        {
            if (host) { MenuManager.screen = new GameScreen(true, "127.0.0.1", 1, null); }
            else { MenuManager.ClickNewGame(o, e); }
        }

        public void ClickLoadMapStart(Object o, EventArgs e)
        {
            if (loadpath == null)
            {
                if (host) { MenuManager.screen = new GameScreen(true, "127.0.0.1", 1, null); }
                else { MenuManager.ClickNewGame(o, e); }
            }
            else
            {
                Map map = null;
                FileStream fs = null;
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    fs = new FileStream(loadpath, FileMode.Open);
                    map = (Map)formatter.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    map = null;
                }
                finally
                {
                    if (fs != null) fs.Close();
                    if (host) { MenuManager.screen = new GameScreen(true, "127.0.0.1", map.players.Count-1, map); }
                    else { MenuManager.screen = new GameScreen(true, null, 0, map); }
                }
            }

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
