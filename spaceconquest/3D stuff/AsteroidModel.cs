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
    static class AsteroidModel
    {
        public static Model poplulatedAsteroid;
        public static Model unpoplulatedAsteroid;
        public static BasicEffect basicEffect;

        public static void InitializePrimitive(Model m_unpopulated, Model m_populated)
        {
            poplulatedAsteroid = m_populated;
            unpoplulatedAsteroid = m_unpopulated;
            GraphicsDevice graphicsDevice = Game1.device;

            Point center = Point.Zero;

            poplulatedAsteroid.Root.Transform = Matrix.CreateScale(20f);
            unpoplulatedAsteroid.Root.Transform = Matrix.CreateScale(20f);


            basicEffect = new BasicEffect(Game1.device);

            foreach (ModelMesh mesh in poplulatedAsteroid.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = basicEffect;
                }
            }

            foreach (ModelMesh mesh in unpoplulatedAsteroid.Meshes)
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



        public static void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor, float scale, bool isPopulated, float curAngle)
        {
            // Set BasicEffect parameters.
            Matrix worldMatrix = Matrix.CreateScale(scale) *
                 Matrix.CreateRotationZ((float)((curAngle / 360.0) * 2 * Math.PI)) *
                 world;
            basicEffect.World = worldMatrix;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = newcolor.ToVector3();
            //basicEffect.VertexColorEnabled = true;

            //basicEffect.Alpha = color.A / 255.0f;
            basicEffect.EmissiveColor = newcolor.ToVector3();

            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.

            if (isPopulated)
            {
                foreach (ModelMesh mesh in poplulatedAsteroid.Meshes)
                {
                    mesh.Draw();
                }

                //To DO: Draw Mining Robots

            }
            else
            {
                foreach (ModelMesh mesh in unpoplulatedAsteroid.Meshes)
                {
                    mesh.Draw();
                }
            }
        }



    }
}
