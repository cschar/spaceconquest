using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    abstract class Ship:Unit
    {
        int speed = 6;
        public void move(Hex3D target) {
            hex.RemoveObject();
            SetHex(target);
        }

        public Ship() { 
        }

        public Ship(int moveSpeed) {
            speed = moveSpeed;
        }

        public abstract void HopOn(Ship c);



        public override void kill()
        {
            hex.RemoveObject();
            affiliation.army.Remove(this);
        }

        public List<Hex3D> GetReachable()
        {
            List<Hex3D> hexes = reachable(hex, speed);
            foreach (Hex3D h in hexes) {
                h.distance = -1;
                if (h.GetGameObject() != null) {
                    //hexes.Remove(h);
                }
            }
            hex.distance = -1; 
            return hexes;
        }



        List<Hex3D> reachable(Hex3D startHex, int r)
        {   
            startHex.distance = r;
            List<Hex3D> hexes = new List<Hex3D>();
            if (r <= 0) {
                return hexes;
            }
            foreach (Hex3D h in startHex.getNeighbors())
            {
                int dist = h.distance;
                if (dist == -1 || dist < r-1) {
                    hexes.Add(h);
                    hexes.AddRange(reachable(h, r-1));
                }
            }

            return hexes;
        }


        public override void Draw(Microsoft.Xna.Framework.Matrix world, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection)
        {
            throw new NotImplementedException();
        }
    }
}
