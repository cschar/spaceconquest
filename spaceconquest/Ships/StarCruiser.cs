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

        public Ship CreateShip()
        {
            Warship newship = new Warship("starcruiser");
            return newship;
        }
    }
}
