using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace spaceconquest
{
    abstract class ShipType
    {
        public String modelstring = "starcruiser";
        public int speed = 3;
        public int range = 4;
        public int damage = 1;
        public int cost = 100;
        public int movespeed = 0;
        public int capacity = 0;

        //private static ShipType c;
        //public static ShipType Creator() { return c; }
        abstract public Ship CreateShip();

    }
}
