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
using System.IO;

namespace spaceconquest
{
    static class ProtonBeamModel
    {
        public static Model beam;
        public static BasicEffect basicEffect;

        public static void InitializePrimitive(Model m)
        {
            beam = m;
            GraphicsDevice graphicsDevice = Game1.device;

            Point center = Point.Zero;

            m.Root.Transform = Matrix.CreateScale(50f);

            basicEffect = new BasicEffect(Game1.device);

            foreach (ModelMesh mesh in beam.Meshes)
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



        public static void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor, float scale)
        {
            // Set BasicEffect parameters.
            basicEffect.World =  Matrix.CreateRotationZ((float) (Math.PI))  * Matrix.CreateScale(2.0f) * world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = newcolor.ToVector3();
            //basicEffect.VertexColorEnabled = true;

            basicEffect.Alpha = newcolor.A / 255.0f;
            basicEffect.EmissiveColor = newcolor.ToVector3();
            //basicEffect.EmissiveColor = new Color(100,100,0).ToVector3();
            basicEffect.SpecularColor = new Color(255, 255, 0).ToVector3();
            basicEffect.SpecularPower = 10f;
            //basicEffect.Texture = Texture2D.FromStream(Game1.device, new FileStream(@"Content\suntexture.png", FileMode.Open));
            // basicEffect.TextureEnabled = true;

            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.
            Matrix translationMatrix = Matrix.CreateRotationY((float)(Math.PI / 2.0f));
            foreach (ModelMesh mesh in beam.Meshes)
            {
                mesh.Draw();
            }
        }



    }
}