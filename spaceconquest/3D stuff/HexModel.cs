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
    static class HexModel
    {
        public static int radius = 50;
        public static int spacing = 5;
        public static Color color = Color.White;

        // During the process of constructing a primitive model, vertex
        // and index data is stored on the CPU in these managed lists.
        static List<VertexPositionColor> vertices = new List<VertexPositionColor>();
        static List<ushort> indices = new List<ushort>();


        // Once all the geometry has been specified, the InitializePrimitive
        // method copies the vertex and index data into these buffers, which
        // store it on the GPU ready for efficient rendering.
        static VertexBuffer vertexBuffer;
        static IndexBuffer indexBuffer;
        public static BasicEffect basicEffect;


        public static void InitializePrimitive(GraphicsDevice graphicsDevice)
        {
            Point center = Point.Zero;

            float xshift = (float)Math.Cos(Math.PI / (double)6) * radius;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * radius;


            Vector3 up = new Vector3(0, 0, 1);
            vertices.Add(new VertexPositionColor(new Vector3(center.X, center.Y, 1), Color.Black));
            vertices.Add(new VertexPositionColor(new Vector3(center.X + xshift, center.Y + yshift, 1), color));
            vertices.Add(new VertexPositionColor(new Vector3(center.X + xshift, center.Y - yshift, 1), color));
            vertices.Add(new VertexPositionColor(new Vector3(center.X, center.Y - radius, 1), color));
            vertices.Add(new VertexPositionColor(new Vector3(center.X - xshift, center.Y - yshift, 1), color));
            vertices.Add(new VertexPositionColor(new Vector3(center.X - xshift, center.Y + yshift, 1), color));
            vertices.Add(new VertexPositionColor(new Vector3(center.X, center.Y + radius, 1), color));

            indices.Add((ushort)0);
            indices.Add((ushort)1);
            indices.Add((ushort)2);

            indices.Add((ushort)0);
            indices.Add((ushort)2);
            indices.Add((ushort)3);

            indices.Add((ushort)0);
            indices.Add((ushort)3);
            indices.Add((ushort)4);

            indices.Add((ushort)0);
            indices.Add((ushort)4);
            indices.Add((ushort)5);

            indices.Add((ushort)0);
            indices.Add((ushort)5);
            indices.Add((ushort)6);

            indices.Add((ushort)0);
            indices.Add((ushort)6);
            indices.Add((ushort)1);



            // Create a vertex buffer, and copy our vertex data into it.
            vertexBuffer = new VertexBuffer(graphicsDevice,
                                            typeof(VertexPositionColor),
                                            vertices.Count, BufferUsage.None);

            vertexBuffer.SetData(vertices.ToArray());

            // Create an index buffer, and copy our index data into it.
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort),
                                          indices.Count, BufferUsage.None);

            indexBuffer.SetData(indices.ToArray());

            // Create a BasicEffect, which will be used to render the primitive.
            basicEffect = new BasicEffect(graphicsDevice);

            //basicEffect.EnableDefaultLighting();
        }

        public static void Draw(Effect effect)
        {
            GraphicsDevice graphicsDevice = effect.GraphicsDevice;

            // Set our vertex declaration, vertex buffer, and index buffer.
            graphicsDevice.SetVertexBuffer(vertexBuffer);

            graphicsDevice.Indices = indexBuffer;


            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();

                int primitiveCount = indices.Count / 3;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, vertices.Count, 0, primitiveCount);

            }
        }

        public static void Draw(Matrix world, Matrix view, Matrix projection, Color newcolor)
        {
            // Set BasicEffect parameters.
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = newcolor.ToVector3();
            basicEffect.VertexColorEnabled = true;
            //basicEffect.Alpha = color.A / 255.0f;
            //basicEffect.EmissiveColor = newcolor.ToVector3();

            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;
            device.BlendState = BlendState.AlphaBlend;

            // Draw the model, using BasicEffect.
            Draw(basicEffect);
        }



        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        public static void Dispose()
        {
            if (vertexBuffer != null) vertexBuffer.Dispose();
            if (indexBuffer != null) indexBuffer.Dispose();
            if (basicEffect != null) basicEffect.Dispose();
            
        }
    }
}
