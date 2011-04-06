using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable]
    class Transport : ShipType
    {
        public static Transport creator = new Transport(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters

        public override Ship CreateShip()
        {
            this.modelstring = "transport";
            this.speed = 3;
            this.range = 4;
            this.damage = 1;
            this.cost = 100;
            this.movespeed = 0;
            this.capacity = 0;

            Carrier newship = new Carrier(this);
            return newship;
        }
    }
}
