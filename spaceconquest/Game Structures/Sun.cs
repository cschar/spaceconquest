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
    class Sun : GameObject
    {
        public Sun(Hex3D h)
        {
            SetHex(h);
        }

        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            //world = world + Matrix.CreateTranslation(getCenter());
            SphereModel.Draw( Matrix.CreateTranslation(0,0,0) * world, view, projection, Color.Goldenrod,80);
        }
    }
}
