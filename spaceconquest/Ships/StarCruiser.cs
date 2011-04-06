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
            this.cost = 100;
            this.movespeed = 0;
            this.capacity = 0;

            Warship newship = new Warship(this);
            return newship;
        }
    }
}
