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
    class LineModel
    {
        Vector3 position;
        Vector3 destination;
        List<VertexPositionColor> vertices = new List<VertexPositionColor>();
        List<int> indices = new List<int>();
        static int size = 5;
        static Vector3 x = new Vector3(size, 0, 0);
        static Vector3 y = new Vector3(0, size, 0);
        static Vector3 z = new Vector3(0, 0, size);
        BasicEffect effect;

        public LineModel(Vector3 pos, Vector3 dest)
        {
            position = pos;
            destination = dest;
            effect = new BasicEffect(Game1.device);

            vertices.Add(new VertexPositionColor(position + x, Color.White));
            vertices.Add(new VertexPositionColor(position + y, Color.White));
            vertices.Add(new VertexPositionColor(position + z, Color.White));

            vertices.Add(new VertexPositionColor(destination, Color.White));
            //vertices.Add(new VertexPositionColor(destination + y, Color.White));
            //vertices.Add(new VertexPositionColor(destination + z, Color.White));

            indices.Add(0);
            indices.Add(1);
            indices.Add(2);

            indices.Add(0);
            indices.Add(1);
            indices.Add(3);

            indices.Add(1);
            indices.Add(2);
            indices.Add(3);

            indices.Add(0);
            indices.Add(2);
            indices.Add(3);


        }

        public void Draw(Effect effect)
        {
            
            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();

                int primitiveCount = indices.Count / 3;

                Game1.device.DrawUserIndexedPrimitives<VertexPositionColor>(PrimitiveType.TriangleList, vertices.ToArray(), 0, 4, indices.ToArray(), 0, 4);

            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor)
        {
            // Set BasicEffect parameters.
            effect.World = world;
            effect.View = view;
            effect.Projection = projection;
            effect.DiffuseColor = newcolor.ToVector3();
            effect.VertexColorEnabled = true;

            //basicEffect.Alpha = color.A / 255.0f;
            //basicEffect.EmissiveColor = newcolor.ToVector3();

            Game1.device.DepthStencilState = DepthStencilState.Default;
            Game1.device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.
            Draw(effect);
        }

    }
}
