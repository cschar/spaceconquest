using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class Telescope : ShipType
    {
        public static Telescope telescope = new Telescope();

        public Ship CreateShip()
        {
            Warship newship = new Warship("starcruiser");
            return newship;
        }
    }
}
