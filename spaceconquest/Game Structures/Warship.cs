using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spaceconquest
{
    class Warship : Ship
    {
        int damage = 1;
        int range = 3;

        public Warship(Hex3D h)
        {
            SetHex(h);
        }

        public override void HopOn(Ship c)
        {
            throw new NotImplementedException();
        }

        public void Attack(Unit u)
        {
            u.hit(damage);
        }

        public List<Hex3D> GetShootable()
        {
            HashSet<Hex3D> hexes = new HashSet<Hex3D>();
            Shootable(hexes, hex, range);
            hexes.Remove(hex);
            return hexes.ToList();
        }

        private void Shootable(HashSet<Hex3D> hexes, Hex3D h, int r)
        {
            if (r <= 0) return;
            foreach (Hex3D n in h.getNeighbors())
            {
                hexes.Add(n);
                Shootable(hexes, n, r - 1);
            }
        }

        public override void Draw(Microsoft.Xna.Framework.Matrix world, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection)
        {
            Color color = Color.Red; //in the future, this will link to the player's color

            SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, color, 20);

             if (ghosthex != null) SphereModel.Draw(Matrix.CreateTranslation(ghosthex.getCenter()) * world, view, projection, Color.Multiply(color,.2f) , 20);
        }
    }
}
