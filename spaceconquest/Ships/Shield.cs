using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable]
    class Shield : ShipType
    {
        public static Shield creator = new Shield(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters

        public override Ship CreateShip()
        {
            this.modelstring = "Shield";
            this.buildTime = 3;
            this.cost = 100;

            //stats dont matter since we never create this
            Ship newship = new Ship(this);
            return newship;
        }
        public override void PlaySelectSound()
        {
            //Game1.soundEffectBox.PlaySound("ColonyShip");
        }
    }
}
