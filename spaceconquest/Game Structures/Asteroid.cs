using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    class Asteroid : Unit
    {
        public override void kill(){
            this.affiliation = null;
            //this.hue = Color.Gray;
        }

        public Asteroid(Hex3D loc) {
            this.SetHex(loc);
            loc.defaultcolor = Color.Black;
            //loc.passable = false;
        }

        public void upkeep() {
            if (affiliation != null) { affiliation.giveMetal(100); }
        }



        private float angle = 0;
        private float angleIncr = 0.3f;

        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            //world = world + Matrix.CreateTranslation(getCenter());
           // if (affiliation == null) { SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, Color.Gray, 30); }
           // else SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, affiliation.color, 30);

            if (affiliation == null) { AsteroidModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, Color.Gray, 20, false, angle); }
            else AsteroidModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, affiliation.color, 20, true, angle);

            angle += angleIncr;
            if (angle > 360) angle = 0;

        
        }
    }
}
