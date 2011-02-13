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
    class GameScreen : Screen
    {
        //in the future, the map would be here to but i dont have a map class yet
        SolarSystem solar; //current solar system
        //maybe make solar system and galaxy implement the same interface so we can draw either of them in here

        public GameScreen()
        {
            solar = new SolarSystem(1, new Rectangle(0,0,800, 600));
        }

        public void Update()
        {
            //throw new NotImplementedException();
        }

        public void Draw()
        {
            solar.Draw();
        }
    }
}
