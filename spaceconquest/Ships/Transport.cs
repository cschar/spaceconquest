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

        public Ship CreateShip()
        {
            Carrier newship = new Carrier("transport");
            return newship;
        }
    }
}
