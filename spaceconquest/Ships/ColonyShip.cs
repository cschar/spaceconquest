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

        public override Ship CreateShip()
        {
            this.modelstring = "colonyship";
            this.speed = 2;
            this.range = 4;
            this.damage = 1;
            this.cost = 100;
            this.shield = 0;
            this.capacity = 0;

            this.cancolonize = true;

            Ship newship = new Ship(this);
            return newship;
        }
        public override void PlaySelectSound()
        {
            Game1.soundEffectBox.PlaySound("ColonyShip");
        }
    }
}
