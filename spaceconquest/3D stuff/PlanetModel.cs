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
    static class PlanetModel
    {
        public static Model poplulatedPlanet;
        public static Model unpoplulatedPlanet;
        public static BasicEffect basicEffect;

        public static void InitializePrimitive(Model m_unpopulated, Model m_populated)
        {
            poplulatedPlanet = m_populated;
            unpoplulatedPlanet = m_unpopulated;
            GraphicsDevice graphicsDevice = Game1.device;

            Point center = Point.Zero;

            poplulatedPlanet.Root.Transform = Matrix.CreateScale(20f);
            unpoplulatedPlanet.Root.Transform = Matrix.CreateScale(20f);


            basicEffect = new BasicEffect(Game1.device);

            foreach (ModelMesh mesh in poplulatedPlanet.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = basicEffect;
                }
            }

            foreach (ModelMesh mesh in unpoplulatedPlanet.Meshes)
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



        public static void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor, float scale, bool isPopulated)
        {
            // Set BasicEffect parameters.
            basicEffect.World = Matrix.CreateScale(scale) * world;
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
                foreach (ModelMesh mesh in poplulatedPlanet.Meshes)
                {
                    mesh.Draw();
                }
            }
            else
            {
                foreach (ModelMesh mesh in unpoplulatedPlanet.Meshes)
                {
                    mesh.Draw();
                }
            }
        }



    }
}
