﻿using System;
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
    class GameScreen : Screen
    {
        //in the future, the map would be here to but i dont have a map class yet
        SolarSystem3D solar; //current solar system
        //maybe make solar system and galaxy implement the same interface so we can draw either of them in here

        public static float scrollspeed = 2f;
        Vector3 offset;

        public GameScreen()
        {
            solar = new SolarSystem3D(5, new Rectangle(0,0,800, 600));
            offset = new Vector3(0,0,0);
        }

        public void Update()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.Left)) { offset.X = offset.X + scrollspeed; }
            if (keystate.IsKeyDown(Keys.Right)) { offset.X = offset.X - scrollspeed; }
            if (keystate.IsKeyDown(Keys.Down)) { offset.Y = offset.Y + scrollspeed; }
            if (keystate.IsKeyDown(Keys.Up)) { offset.Y = offset.Y - scrollspeed; }

            solar.Update();
        }

        public void Draw()
        {
            solar.Draw(offset);
        }
    }
}