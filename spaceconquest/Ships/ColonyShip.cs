using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable]
    class ColonyShip : ShipType
    {
        public static ColonyShip creator = new ColonyShip(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters

        public Ship CreateShip()
        {
            Ship newship = new Ship("colonyship");
            return newship;
        }
    }
}
