using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class MiningShip : ShipType
    {
        public static MiningShip creator = new MiningShip(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters

        private MiningShip() { }

        public Ship CreateShip()
        {
            Warship newship = new Warship("miningship");
            return newship;
        }
    }
}
