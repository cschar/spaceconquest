using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace spaceconquest
{
    [Serializable]
    abstract class ShipType
    {
        public String modelstring = "starcruiser";
        public int speed = 3;
        public int range = 4;
        public int damage = 1;
        public int cost = 100;
        public int buildTime = 1; //will be changed with subclasses 
        public int capacity = 0;
        public int shield = 5; // ditto

        public bool canjump = false;
       // public bool canfire = false;
        public bool canenter = true;
       // public bool cancarry = false;
        public bool cancolonize = false;

        //private static ShipType c;
        //public static ShipType Creator() { return c; }
        abstract public Ship CreateShip();

        abstract public void PlaySelectSound();
    }
}
