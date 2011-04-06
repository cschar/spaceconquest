using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable]
    class MiningShip : ShipType
    {
        public static MiningShip creator = new MiningShip(); //i realize this is silly, but i couldnt figure out a compile time way to pass classes as parameters
        private MiningShip() { }

        public override Ship CreateShip()
        {
            this.modelstring = "miningship";
            this.speed = 4;
            this.range = 4;
            this.damage = 1;
            this.cost = 100;
            this.shield = 1;
            this.buildTime = 3;
            this.capacity = 0;

            this.cancolonize = true;

            Ship newship = new Ship(this);
            return newship;
        }

        public override void PlaySelectSound()
        {
            Game1.soundEffectBox.PlaySound("MiningShip");
        }
    }
}
