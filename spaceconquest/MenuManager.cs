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
    public static class MenuManager
    {
        public static SpriteBatch batch;
        public static SpriteFont font;

        public enum ScreenState //does this even do anything?
        {
            Main,
            MapSelect,
            Game,
            GlobalLobby,
            GameLobby,
            ClientConnect,
        }

        //public static ScreenState currentstate = ScreenState.Main;

        public static Screen screen;

        public static void Init(SpriteBatch b, SpriteFont f)
        {
            batch = b;
            font = f;
            screen = new TitleScreen(batch, font);
        }

        //some menu clicking event functions,
        public static void ClickTitle(Object o, EventArgs e) { screen = new TitleScreen(batch, font); }
        public static void ClickMapSelect(Object o, EventArgs e) { screen = new MapSelectScreen(batch, font); }
        public static void ClickGlobalLobby(Object o, EventArgs e) { screen = new GlobalLobbyScreen(batch, font); }
        public static void ClickGameLobby(Object o, EventArgs e) { screen = new GameLobbyScreen(batch, font); }
        //public static void ClickClientConnect(Object o, EventArgs e) { screen = new ClientConnectScreen((String)o); }
        //public static void ClickHost(Object o, EventArgs e) { screen = new HostScreen((String)o); }

        public static void ClickClientConnect(Object o, EventArgs e) { screen = new GameScreen(false, (String)o, 1, null); }

        public static void ClickHost(Object o, EventArgs e) { screen = new GameScreen(true, null,1, null); }
        public static void ForceJoinGame(Object o, EventArgs e) { screen = new GameScreen(false,(String)o,1, null); }

        public static void ClickNewGame(Object o, EventArgs e) { screen = new GameScreen(true, null,0, null); } //this should be the number of client players, right now we only support 2 players so there.
       // public static void ClickMapSelect() { screen = new MapSelectScreen(batch, font); } 

        //screen for changing music
        public static void ClickMusicOptions(Object o, EventArgs e){ screen = new OptionsScreen(); }
        }

    
}
