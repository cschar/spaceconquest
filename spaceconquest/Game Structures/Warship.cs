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
    [Serializable]
    class Warship : Ship
    {
        protected int damage = 1;
        protected int range = 3;

        private bool firinganimation = false;
        private Vector3 firingtarget = new Vector3();
        private Vector3 firingsource = new Vector3();
        private Unit targetship = null;
        protected int firingpercent = 0;

        public Warship(ShipType st) : base(st)
        {
            damage = st.damage;
            range = st.range;
        }

        public void Attack(Unit u)
        {
            u.hit(damage);
            targetship = null;
            if (u.hex.GetGameObject() != u) { targetship = u; }

            firingsource = this.getCenter();
            firingpercent = 0;
            firingtarget = u.getCenter();
            firinganimation = true;
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

        //public int GetDamage() { return damage; }
        public int GetRange() { return range; }

        public override void Draw(Microsoft.Xna.Framework.Matrix world, Microsoft.Xna.Framework.Matrix view, Microsoft.Xna.Framework.Matrix projection)
        {
            if (!firinganimation) { base.Draw(world, view, projection); }
            else
            {
                Vector3 currentfiring = (firingtarget - firingsource) * (firingpercent / 100.0f) + firingsource;
                firingpercent = firingpercent + 2;
                if (firingpercent == 100) { firingpercent = 0; targetship = null; firinganimation = false; }

                SphereModel.Draw(Matrix.CreateTranslation(currentfiring) * world, view, projection, Color.Red, 10);
                if (targetship != null) targetship.Draw(world, view, projection);


                if (shipmodel == null) { shipmodel = ShipModel.shipmodels[modelstring]; }

                shipmodel.Draw(Matrix.CreateRotationZ((float)currentAngle) * Matrix.CreateTranslation(firingsource) * world, view, projection, affiliation.color, 1.6f, hoveringHeight);

                //create illusion that ship is hovering in space
                hoveringHeight += hoveringAcc;
                if (hoveringHeight > 13 || hoveringHeight < 6) { hoveringAcc *= -1; }
            }
        }

        
    }
}
