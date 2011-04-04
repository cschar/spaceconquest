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
        public static Model miningBot;
        public static Model unpoplulatedAsteroid;
        public static BasicEffect basicEffect;


        public static void InitializePrimitive(Model m_unpopulated, Model mingingBotModel)
        {
            miningBot = mingingBotModel;
            unpoplulatedAsteroid = m_unpopulated;
            GraphicsDevice graphicsDevice = Game1.device;

            Point center = Point.Zero;


            unpoplulatedAsteroid.Root.Transform = Matrix.CreateScale(20f);


            basicEffect = new BasicEffect(Game1.device);

            foreach (ModelMesh mesh in miningBot.Meshes)
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
            float radianFromAngle = (float)((curAngle / 360.0) * 2 * Math.PI);
            Matrix worldMatrix = Matrix.CreateScale(scale) *
                 Matrix.CreateRotationZ(radianFromAngle) *
                 world;
            basicEffect.World = worldMatrix;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = newcolor.ToVector3();
            basicEffect.EmissiveColor = newcolor.ToVector3();
            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.

            if (isPopulated)
            {
                foreach (ModelMesh mesh in unpoplulatedAsteroid.Meshes)
                {
                    mesh.Draw();
                }

                //To DO: Draw Mining Robots

                Matrix RobotTransform = Matrix.Identity;
                //Draw 3 Robots flying around asteroid
                for (int i = 0; i < 3; i++)
                {

                    if (i == 0)
                    {
                        float asteroid1 = (float)((curAngle / 360.0f) * 2 * Math.PI);

                        RobotTransform =
                            Matrix.CreateTranslation(new Vector3(30, 0, 0)) *
                            Matrix.CreateRotationZ(asteroid1);
                    }
                    if (i == 1)
                    {
                        RobotTransform =
                            Matrix.CreateRotationZ((float)Math.PI / -2.0f) *
                            Matrix.CreateTranslation(new Vector3(0, 0, 40)) *

                            Matrix.CreateRotationY(radianFromAngle);


                    }
                    if (i == 2)
                    {
                        RobotTransform =
                            Matrix.CreateRotationX((float)Math.PI / 2.0f) *
                            Matrix.CreateRotationZ((float)Math.PI / 2.0f) *
                            Matrix.CreateTranslation(new Vector3(0, 100, 0)) *
                            Matrix.CreateRotationX((float)radianFromAngle);
                    }
                    miningBot.Root.Transform = RobotTransform;
                    miningBot.Draw(world, view, projection);
                }
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