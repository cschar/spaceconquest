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
    [Serializable]
    class Galaxy : Space
    {
        public List<SolarSystem3D> systems;
        public List<Vector3> positions;
        protected List<LineModel> lines = new List<LineModel>();
        public String gName;
        [NonSerialized] StarField stars = new StarField(2000);

        Matrix world;
        Matrix view;
        Matrix projection;

        public Galaxy(String g, int size, Int64 seed) {
            gName = g;
            systems = new List<SolarSystem3D>(size);
            positions = new List<Vector3>(size);
            float radialIncrement = (float)(Math.PI * 2 / size);
            Int64 P2 = seed;
            for (int i = 0; i < size; i++) {
                Int64 Red = CommonRNG.getRandom(P2);
                Int64 Blue = CommonRNG.getRandom(Red);
                Int64 Green = CommonRNG.getRandom(Blue);
                Int64 Size1 = CommonRNG.getRandom(Green);
                Int64 Size2 = CommonRNG.getRandom(Size1);
                Int64 P1 = CommonRNG.getRandom(Size2);
                P2 = CommonRNG.getRandom(P1);
                Red = Red % 150;
                Green = Green % 150;
                Blue = Blue % 150;
                //Console.WriteLine("RGB = " + (float)Red + ", " + (float)Green + ", " + (byte)Blue);
                int iRed = ((int)Red);
                int iBlue = ((int)Blue);
                int iGreen = ((int)Green);
                int iP1 = ((int)P1);
                int iP2 = ((int)P2);
                int iS1 = ((int)Size1);
                int iS2 = ((int)Size2);
                iP2 %= 3;
                iP1 %= 3;
                iS1 %= 3;
                iS2 %= 3;

                //Console.WriteLine("P1, P2 = " + iP1 + ", " + iP2);
                //Console.WriteLine("R1, R2 = " + iS1 + ", " + iS2);

                systems.Add(new SolarSystem3D((int)(5+iS1+iS2), (int)(1+iP1+iP2), new Color(iRed, iBlue, iGreen), i, P2));
                positions.Add(new Vector3((float)(300*Math.Cos(i * radialIncrement)), (float)(300*Math.Sin(i* radialIncrement)), 0));
            }

            CommonRNG.resetSeed();
            int count = 0;
            for (int i = 0; i < size-1; i++) {
                SolarSystem3D sTemp = systems.ElementAt(i);
                List<SolarSystem3D> nTemp = sTemp.neighbors;
                int sCount = systems.Count;
                SolarSystem3D s1 = systems.ElementAt((i + 1 + sCount) % sCount);
                SolarSystem3D s2 = systems.ElementAt((i - 1 + sCount) % sCount);
                nTemp.Add(s1);
                nTemp.Add(s2);
                s2.neighbors.Add(sTemp);
                lines.Add(new LineModel(positions[s1.index], positions[sTemp.index]));
                s1.neighbors.Add(sTemp);
                lines.Add(new LineModel(positions[s2.index], positions[sTemp.index]));

                Random rng = CommonRNG.rand;
                int link = rng.Next(int.MaxValue);
                for (int j = 0; j < size-1; j++) {
                    if (i != j) {
                        //link = link = rng.Next(int.MaxValue);
                        link = rng.Next(size);
                        count++;
                        //Console.WriteLine(size + ", " + link + ", " +link%(size));
                        s1 = systems.ElementAt(j);
                        if (link == size-1 && !sTemp.neighbors.Contains(s1)) {
                            //Console.WriteLine("CONNECTION");
                            s1.neighbors.Add(sTemp);
                            lines.Add(new LineModel(positions[s1.index],positions[sTemp.index])); //for drawing lines between connected galaxies
                            nTemp.Add(s1);
                        }
 
                    }
                }
                //Console.WriteLine("Tried " + count);
            }


        }

        public Galaxy(String g, int size)
        {
            gName = g;
            systems = new List<SolarSystem3D>(size);
            positions = new List<Vector3>(size);
            Random rand = new Random();

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
            Vector3 cameraPosition = new Vector3(0, 0, height*1.5f);
            float aspect = Game1.device.Viewport.AspectRatio;
            world = Matrix.Identity;
            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);

            // Matrix.CreateTranslation(offset) * Matrix.CreateFromYawPitchRoll(xr, yr, 0) removing yr
            view = Matrix.CreateFromYawPitchRoll(0, 0, zr) * Matrix.CreateFromYawPitchRoll(xr, 0, 0) * view;
            projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10000);

            if (stars == null) { stars = new StarField(2000); }
            stars.Draw(world, view, projection);

            for (int i = 0; i < systems.Count; i++)
            {
                systems[i].sun.Draw(Matrix.CreateTranslation(positions[i]) * world, view, projection);
            }

            foreach (LineModel lm in lines)
            {
                lm.Draw(world, view, projection,Color.White);
            }
        }
    }
}
