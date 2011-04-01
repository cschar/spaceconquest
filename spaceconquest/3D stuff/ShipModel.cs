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
    class ShipModel
    {
        public static readonly Dictionary<String, ShipModel> shipmodels = new Dictionary<String, ShipModel>();
        public Model ship;
        public BasicEffect basicEffect;

        public static ShipModel AddShipModel(Model m, String name)
        {
            return (new ShipModel(m, name));
        }

        private ShipModel(Model m, String name)
        {
            shipmodels.Add(name, this); //add to static dictionary of models
            ship = m;

            GraphicsDevice graphicsDevice = Game1.device;

            Point center = Point.Zero;

            m.Root.Transform = Matrix.CreateScale(10f);

            basicEffect = new BasicEffect(Game1.device);

            foreach (ModelMesh mesh in ship.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = basicEffect;
                }
            }

            // Create a BasicEffect, which will be used to render the primitive.
            //basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.DiffuseColor = new Vector3(50, 50, 50);
            basicEffect.EnableDefaultLighting();
        }


        public void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor, float scale, float hoveringHeight)
        {
            // Set BasicEffect parameters.
            Matrix worldMatrix = Matrix.CreateScale(scale) *
                Matrix.CreateRotationX((float)(Math.PI)) *
                Matrix.CreateTranslation(new Vector3(0, 0, hoveringHeight)) *
                world;

            basicEffect.World = worldMatrix;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = newcolor.ToVector3();
            // basicEffect.VertexColorEnabled = true;

            //basicEffect.Alpha = color.A / 255.0f;
            basicEffect.EmissiveColor = newcolor.ToVector3();

            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.

            foreach (ModelMesh mesh in ship.Meshes)
            {
                mesh.Draw();
            }
        }
    }
}
