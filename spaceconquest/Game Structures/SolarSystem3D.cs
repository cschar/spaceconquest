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
    class SolarSystem3D : Space
    {
        public readonly int index;
        [NonSerialized] StarField stars = new StarField(2000);
        Hex3D[,] hexmap;
        List<Hex3D> hexlist;
        public List<SolarSystem3D> neighbors = new List<SolarSystem3D>();
        public List<Planet> planets = new List<Planet>(); 
        public readonly Sun sun;

        int radius;
        int hexcount;

        Plane plane;

        Matrix world;
        Matrix view;
        Matrix projection;

        public SolarSystem3D(int r, int p, Color hcolor, int ind, Int64 seed) {
            index = ind;
            radius = r;
            plane = new Plane(0, 0, 1, 1);

            hexmap = new Hex3D[(radius * 2) + 1, (radius * 2) + 1];

            hexcount = HowManyHexes(radius);
            hexlist = new List<Hex3D>(hexcount);

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    if (Math.Abs(i + j) > radius) { continue; }
                    Hex3D temp = new Hex3D(i, j, this, hcolor);
                    hexlist.Add(temp);
                    hexmap[i + radius, j + radius] = temp;
                }
            }
            sun = new Sun(getHex(0, 0));
            List<Hex3D> cands = new List<Hex3D>();
            foreach (Hex3D h in hexlist) {
                int j = Math.Abs(h.x) + Math.Abs(h.y);
                if(!(j < 1 || (j == 2 && Math.Max(h.x, h.y) == 1))) {
                    cands.Add(h);
                }
                
            }
            Int64 popper = seed;
 

            for (int i = 0; i < p; i++) {
                popper = CommonRNG.getRandom(popper);
                int popIndex = (int)(popper%cands.Count);
                Hex3D hTemp = cands.ElementAt(popIndex);
                cands.RemoveAt(popIndex);
                Planet pTemp = new Planet(index + "-" + i, hTemp);
                planets.Add(pTemp);
            }

            for (int i = 0; i < 3; i++)
            {
                popper = CommonRNG.getRandom(popper);
                int popIndex = (int)(popper % cands.Count);
                Hex3D hTemp = cands.ElementAt(popIndex);
                cands.RemoveAt(popIndex);
                Asteroid pTemp = new Asteroid(hTemp);
                //planets.Add(pTemp);
            }



        }

        public SolarSystem3D(int r, int p, Color hcolor, int idex)
        {
            index = idex;
            radius = r;
            plane = new Plane(0, 0, 1, 1);

            hexmap = new Hex3D[(radius * 2) + 1, (radius * 2) + 1];

            hexcount = HowManyHexes(radius);
            hexlist = new List<Hex3D>(hexcount);

            for (int i = -radius; i <= radius; i++)
            {
                for (int j = -radius; j <= radius; j++)
                {
                    if (Math.Abs(i + j) > radius) { continue; }
                    Hex3D temp = new Hex3D(i, j, this, hcolor);
                    hexlist.Add(temp);
                    hexmap[i + radius, j + radius] = temp;
                }
            }

            //BUIDIN' THA MOTHAF**KIN' SUN. 
            new Warship(getHex(-1, -1));
            sun = new Sun(getHex(0, 0));
            //getHex(0, 0).passable = false; i changed the sun constructor to handle the passibility stuff.

           // new Player(new Planet("Earth",Color.Blue,getHex(3, 3)), "Ted");
           // if (p > 1) new Planet("Garth", Color.Green, getHex(3, -4));
            //if (p > 2) new Planet("Mars", Color.Red, getHex(-4, -1));


        }

        private int HowManyHexes(int x)
        {
            if (x < 0) return 0;
            if (x == 0) return 1;
            else return (x * 6) + HowManyHexes(x - 1);
        }


        public Hex3D getHex(int x, int y)
        {
            if (x+radius >= ((radius * 2) + 1)) { return null; }
            if (y+radius >= ((radius * 2) + 1)) { return null; }
            if (x+radius < 0) { return null; }
            if (y+radius < 0) { return null; }

            if (hexmap[x + radius, y + radius] == null) { return null; }
            return hexmap[x + radius, y + radius];
        }

        public List<Hex3D> GetWarpable()
        {
            List<Hex3D> hexes = new List<Hex3D>();
            foreach (Hex3D h in hexlist)
            {
                if (Math.Abs(h.x) >= radius - 1 || Math.Abs(h.y) >= radius - 1 || Math.Abs(h.y + h.x) >= radius - 1) { hexes.Add(h); }
            }

            return hexes;
        }

        public void Update()
        {
            foreach (Hex3D h in hexlist)
            {
                h.color = h.defaultcolor;
            }
        }

        public Hex3D GetMouseOverHex()
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

            float? nintersection = mouseray.Intersects(plane);
            if (!nintersection.HasValue) { return null; }
            float intersection = nintersection.Value;


            mouseray = new Ray(((direction * intersection) + near), Vector3.Zero); //this "ray" is now just the point where the mouseray intersects the plane

            foreach (Hex3D h in hexlist)
            {
                if (h.IsMouseOver(mouseray)) return h;
            }

            return null;
        }

        public void Draw(Vector3 offset, float xr, float yr, float zr, float height)
        {
            Vector3 cameraPosition = new Vector3(0, 0, height);
            
            float aspect = Game1.device.Viewport.AspectRatio;

            world = Matrix.Identity;
            //world = Matrix.CreateTranslation(offset);

            view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            view = Matrix.CreateFromYawPitchRoll(0, 0, zr) * Matrix.CreateTranslation(offset) * Matrix.CreateFromYawPitchRoll(xr, yr, 0) * view;

            projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 10000);
            //projection = Matrix.CreateOrthographic(800, 600, 1, 20);

            if (stars == null) { stars = new StarField(2000); }
            stars.Draw(world, view, projection);

            foreach (Hex3D h in hexlist)
            {

                h.DrawObject(world, view, projection);
            }


            foreach (Hex3D h in hexlist)
            {
                //if (screenarea.Contains((int)h.getCenter().X, (int)h.getCenter().Y)) { h.Draw(world, view, orthog, color); }
                h.Draw(world, view, projection);
            }
        }
    }
}