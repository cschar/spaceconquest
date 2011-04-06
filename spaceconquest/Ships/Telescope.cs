using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable]
    class Telescope : ShipType
    {
        public static Telescope creator = new Telescope(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters

        public override Ship CreateShip()
        {
            this.modelstring = "miningRobot";
            this.speed = 6;
            this.range = 4;
            this.damage = 1;
            this.cost = 300;
            this.shield = 0;
            this.capacity = 0;

            this.canjump = true;

            Ship newship = new Ship(this);
            return newship;
        }
        public override void PlaySelectSound()
        {
            //Game1.soundEffectBox.PlaySound("miningRobot");
        }
    }
}
