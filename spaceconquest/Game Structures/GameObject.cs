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
    abstract class GameObject
    {
        [NonSerialized] public Hex3D hex;

        public void SetHex(Hex3D h) {
            hex = h;
            h.AddObject(this);
        }

        public Vector3 getCenter()
        {

            float xshift = (float)Math.Cos(Math.PI / (double)6) * Hex3D.radius + Hex3D.spacing;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * Hex3D.radius + Hex3D.spacing;

            return new Vector3((hex.x * xshift * 2) + (xshift * hex.y), (yshift + Hex3D.radius) * hex.y, 1);

        }

        public abstract void Draw(Matrix world, Matrix view, Matrix projection);
    }
}
