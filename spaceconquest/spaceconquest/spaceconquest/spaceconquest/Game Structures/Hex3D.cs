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

    class Hex3D
    {
        public int x;
        public int y;
        SolarSystem hexgrid;
        public static int radius = 50;
        public static int spacing = 5;

        // During the process of constructing a primitive model, vertex
        // and index data is stored on the CPU in these managed lists.
        List<VertexPositionNormal> vertices = new List<VertexPositionNormal>();
        List<ushort> indices = new List<ushort>();


        // Once all the geometry has been specified, the InitializePrimitive
        // method copies the vertex and index data into these buffers, which
        // store it on the GPU ready for efficient rendering.
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;
        BasicEffect basicEffect;


        public Hex3D(int xx, int yy, SolarSystem ss)
        {
            x = xx;
            y = yy;
            hexgrid = ss;



            Vector2 center = getCenter();
            float xshift = (float)Math.Cos(Math.PI / (double)6) * radius;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * radius;


            Vector3 up = new Vector3(0, 0, 1);
            vertices.Add(new VertexPositionNormal(new Vector3(center.X, center.Y, 1), up));
            vertices.Add(new VertexPositionNormal(new Vector3(center.X + xshift, center.Y + yshift, 1), up));
            vertices.Add(new VertexPositionNormal(new Vector3(center.X + xshift, center.Y - yshift, 1), up));
            vertices.Add(new VertexPositionNormal(new Vector3(center.X, center.Y - radius, 1), up));
            vertices.Add(new VertexPositionNormal(new Vector3(center.X - xshift, center.Y - yshift, 1), up));
            vertices.Add(new VertexPositionNormal(new Vector3(center.X - xshift, center.Y + yshift, 1), up));
            vertices.Add(new VertexPositionNormal(new Vector3(center.X, center.Y + radius, 1), up));

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

            InitializePrimitive(Game1.device);
        }

        public Vector2 getCenter()
        {
            
            float xshift = (float)Math.Cos(Math.PI / (double)6) * radius+spacing;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * radius+spacing;

            return new Vector2(       (this.x * xshift * 2) + (xshift*this.y) , (yshift + radius) * this.y);

        }

        protected void InitializePrimitive(GraphicsDevice graphicsDevice)
        {
            // Create a vertex declaration, describing the format of our vertex data.

            // Create a vertex buffer, and copy our vertex data into it.
            vertexBuffer = new VertexBuffer(graphicsDevice,
                                            typeof(VertexPositionNormal),
                                            vertices.Count, BufferUsage.None);

            vertexBuffer.SetData(vertices.ToArray());

            // Create an index buffer, and copy our index data into it.
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(ushort),
                                          indices.Count, BufferUsage.None);

            indexBuffer.SetData(indices.ToArray());

            // Create a BasicEffect, which will be used to render the primitive.
            basicEffect = new BasicEffect(graphicsDevice);

            basicEffect.EnableDefaultLighting();
        }


        public void Draw(Effect effect)
        {
            GraphicsDevice graphicsDevice = effect.GraphicsDevice;

            // Set our vertex declaration, vertex buffer, and index buffer.
            graphicsDevice.SetVertexBuffer(vertexBuffer);

            graphicsDevice.Indices = indexBuffer;


            foreach (EffectPass effectPass in effect.CurrentTechnique.Passes)
            {
                effectPass.Apply();

                int primitiveCount = indices.Count / 3;

                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0,
                                                     vertices.Count, 0, primitiveCount);

            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, Color color)
        {
            // Set BasicEffect parameters.
            basicEffect.World = world;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.DiffuseColor = color.ToVector3();
            basicEffect.Alpha = color.A / 255.0f;

            GraphicsDevice device = basicEffect.GraphicsDevice;
            device.DepthStencilState = DepthStencilState.Default;

            if (color.A < 255)
            {
                // Set renderstates for alpha blended rendering.
                device.BlendState = BlendState.AlphaBlend;
            }
            else
            {
                // Set renderstates for opaque rendering.
                device.BlendState = BlendState.Opaque;
            }

            // Draw the model, using BasicEffect.
            Draw(basicEffect);
        }

        /// <summary>
        /// Finalizer.
        /// </summary>
        ~Hex3D()
        {
            Dispose(false);
        }


        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        /// <summary>
        /// Frees resources used by this object.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (vertexBuffer != null)
                    vertexBuffer.Dispose();

                if (indexBuffer != null)
                    indexBuffer.Dispose();

                if (basicEffect != null)
                    basicEffect.Dispose();
            }
        }
    }
}



//GraphicsDevice.Clear(Color.Black);
//GraphicsDevice.RasterizerState = wireFrameState;

//// Create camera matrices, making the object spin.
//float time = (float)gameTime.TotalGameTime.TotalSeconds;

//float yaw = time * 0.4f;
//float pitch = time * 0.7f;
//float roll = time * 1.1f;

//Vector3 cameraPosition = new Vector3(0, 0, 15f);

//float aspect = GraphicsDevice.Viewport.AspectRatio;

//Matrix world = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
//Matrix view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
//Matrix projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10);

//// Draw the current primitive.
//Color color = Color.Green;