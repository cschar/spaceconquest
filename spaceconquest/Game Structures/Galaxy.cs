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
    class Galaxy : Space
    {
        public List<SolarSystem3D> systems;
        public List<Vector3> positions;
        public String gName;
        StarField stars = new StarField(2000);

        Matrix world;
        Matrix view;
        Matrix projection;

        public Galaxy(String g, int size)
        {
            gName = g;
            systems = new List<SolarSystem3D>(size);
            positions = new List<Vector3>(size);
            Random rand = new Random();
            //for (int i = 0; i < size; i++)
            //{
            //    systems.Add(new SolarSystem3D(8, rand.Next(4), new Color(rand.Next(30), rand.Next(30), rand.Next(30)))); //figure out the parameters. 
            //    positions.Add(new Vector3(rand.Next(100), rand.Next(100), rand.Next(100)));
            //}

            //pretty much hardcoding positions in
            if (size < 2)
            {
                systems.Add(new SolarSystem3D(8, rand.Next(4), new Color(rand.Next(150), rand.Next(150), rand.Next(150)), 0)); //figure out the parameters. 
                positions.Add(new Vector3(0, 0, 0));
            }

            if (size >= 2)
            {
                systems.Add(new SolarSystem3D(8, rand.Next(4), new Color(rand.Next(150), rand.Next(150), rand.Next(150)), 0)); //figure out the parameters. 
                positions.Add(new Vector3(-300, 300, 0));

                systems.Add(new SolarSystem3D(8, rand.Next(4), new Color(rand.Next(150), rand.Next(150), rand.Next(150)), 1)); //figure out the parameters. 
                positions.Add(new Vector3(300, 300, 0));
            }

            if (size >= 3)
            {

                systems.Add(new SolarSystem3D(8, rand.Next(4), new Color(rand.Next(150), rand.Next(150), rand.Next(150)), 2)); //figure out the parameters. 
                positions.Add(new Vector3(0, -300, 0));
            }



            foreach (SolarSystem3D solar in systems)
            {
                foreach (SolarSystem3D solar2 in systems)
                {
                    //add a random thing here or else all systems will be linked
                    if (solar != solar2) { solar.neighbors.Add(solar2); }
                }
            }


        }

        public Hex3D GetHex(Tuple<int, int ,int> c)
        {
            if (c.Item1 < 0 || c.Item1 >= systems.Count) return null;
            return systems[c.Item1].getHex(c.Item2, c.Item3);
        }

        public Hex3D GetHex(int s, int x, int y)
        {
            if (s < 0 || s >= systems.Count) return null;
            return systems[s].getHex(x, y);
        }

        public void Update()
        {
        }

        public SolarSystem3D GetMouseOverSystem()
        {
            MouseState mousestate = Mouse.GetState();
            Point mouseposition = new Point(mousestate.X, mousestate.Y);
            Viewport viewport = Game1.device.Viewport;

            Vector3 near = new Vector3(mouseposition.X, mouseposition.Y, 0);
            Vector3 far = new Vector3(mouseposition.X, mouseposition.Y, 1);
            near = viewport.Unproject(near, projection, view, world);
            far = viewport.Unproject(far, projection, view, world);


            Vector3 direction = far - near;
            direction.Normalize();
            Ray mouseray = new Ray(near, direction);

            for (int i = 0; i < systems.Count; i++)
            {
                if (systems[i].sun.IsMouseOver(mouseray, Matrix.CreateTranslation(positions[i]) * world)) return systems[i];
            }

            return null;
        }

        public void Draw(Vector3 offset, float xr, float yr, float zr, float height)
        {
            Vector3 cameraPosition = new Vector3(0, 0, height);
            float aspect = Game1.device.Viewport.AspectRatio;
            world = Matrix.Identity;
            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            view = Matrix.CreateFromYawPitchRoll(0, 0, zr) * Matrix.CreateTranslation(offset) * Matrix.CreateFromYawPitchRoll(xr, yr, 0) * view;
            projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10000);
            
            stars.Draw(world, view, projection);

            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].sun.Draw(Matrix.CreateTranslation(positions[i]) * world, view, projection);
            }
            //draw menu stuff here
        }
    }
}
