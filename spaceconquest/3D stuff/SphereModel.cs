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
    static class SphereModel
    {
        public static Model sphere;
        public static int radius = 50;
        public static int spacing = 5;
        //public static Color color = Color.White;
        public static BasicEffect basicEffect;

        public static void InitializePrimitive(Model m)
        {
            sphere = m;
            GraphicsDevice graphicsDevice = Game1.device;

            Point center = Point.Zero;

            m.Root.Transform = Matrix.CreateScale(50f);

            basicEffect = new BasicEffect(Game1.device);

            foreach (ModelMesh mesh in sphere.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = basicEffect;
                }
            }

            // Create a BasicEffect, which will be used to render the primitive.
            //basicEffect = new BasicEffect(graphicsDevice);

            basicEffect.EnableDefaultLighting();
        }

        

        public static void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor)
        {
            // Set BasicEffect parameters.
            basicEffect.World = Matrix.CreateScale(50f)*world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = newcolor.ToVector3();
            //basicEffect.VertexColorEnabled = true;

            //basicEffect.Alpha = color.A / 255.0f;
            basicEffect.EmissiveColor = Color.Orange.ToVector3();

            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.

            foreach (ModelMesh mesh in sphere.Meshes)
            {
                mesh.Draw();
            }
        }



    }
}
