using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class Carrier : Warship
    {

        List<Ship> payload = new List<Ship>();
        int capacity = 8;
        int load = 0;

        public Carrier(String s)
            : base(s)
        {
            range = 1;
            speed = 2;
            buildTime = 5;
        }

        public Boolean LoadShip(Ship s)
        {
            if (!s.hex.getNeighbors().Contains(this.hex) || s is Carrier || load >= capacity)
            {
                return false;
            }
            s.hex.RemoveObject();
            payload.Add(s);
            load++;
            return true;
        }

        public Boolean UnloadShip(Ship s)
        {
            if (!payload.Contains(s))
                return false;
            Hex3D target = null;

            foreach (Hex3D h1 in this.hex.getNeighbors())
            {
                if (h1.passable && h1.GetGameObject() == null)
                {
                    target = h1;
                    break;
                }
            }
            if (target != null)
            {
                s.hex = target;
                target.AddObject(s);
                return true;
            }
            return false;
        }
        public Boolean UnloadAll() {
            Boolean ret = true;
            foreach (Ship s in payload) {
                ret = ret && UnloadShip(s);
            }
            return ret;
        }


    }

}
