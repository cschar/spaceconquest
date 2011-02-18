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

        public enum ScreenState
        {
            //class out each of these states so they have a draw call
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

        public static void ClickNewGame(Object o, EventArgs e) { screen = new GameScreen(); } //at some point this will actually make a new game, now it just shows a hex
       // public static void ClickMapSelect() { screen = new MapSelectScreen(batch, font); } 

    }
}
