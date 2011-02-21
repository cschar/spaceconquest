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
        int range = 2;

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
            List<Hex3D> hexes = new List<Hex3D>();
            //magic
            return hexes;
        }

        public override void Draw(Microsoft.Xna.Framework.Matrix world, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection)
        {
            SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, Color.Red, 20);
        }
    }
}
