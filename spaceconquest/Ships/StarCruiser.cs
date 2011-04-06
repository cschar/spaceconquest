using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable]
    class StarCruiser : ShipType
    {
        public static StarCruiser creator = new StarCruiser(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters
        private StarCruiser() { }

        public override Ship CreateShip()
        {
            this.modelstring = "starcruiser";
            this.speed = 3;
            this.range = 4;
            this.damage = 1;
            this.shield = 4;
            this.cost = 500;
            this.capacity = 0;
            this.buildTime = 4;

            this.canenter = false;
            this.canjump = true;

            Warship newship = new Warship(this);
            return newship;
        }

        public override void PlaySelectSound()
        {
            Game1.soundEffectBox.PlaySound("StarCruiser");
        }
    }
}
