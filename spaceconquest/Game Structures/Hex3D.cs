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
        public int distance = -1; //for the pathfinding algorithm
        public Color defaultcolor;
        public Color color;
        public readonly int x;
        public readonly int y;
        SolarSystem3D hexgrid;
        public readonly static int radius = HexModel.radius;
        public static int spacing = HexModel.spacing;
        GameObject gameobject;
        public Boolean passable = true; 
        private List<Hex3D> neighbors;

        //used to test the bounds for mouse projection
        BoundingSphere boundsphere;


       

        public Hex3D(int xx, int yy, SolarSystem3D ss, Color c)
        {
            defaultcolor = c;
            color = c;
            x = xx;
            y = yy;
            hexgrid = ss;
            boundsphere = new BoundingSphere(getCenter(), radius);

            
        }

        public List<Hex3D> getNeighbors()
        {// -1, 1; 0, 1; 0, -1; 1, 0;, -1,0; 1,-1;

            if (neighbors != null) { return neighbors; }
            
            neighbors = new List<Hex3D>(6);
            Hex3D n;
            n = hexgrid.getHex(x - 1, y + 1);
            if (n != null && n.passable) { neighbors.Add(n); }
            n = hexgrid.getHex(x , y + 1);
            if (n != null && n.passable) { neighbors.Add(n); }
            n = hexgrid.getHex(x , y - 1);
            if (n != null && n.passable) { neighbors.Add(n); }
            n = hexgrid.getHex(x + 1, y);
            if (n != null && n.passable) { neighbors.Add(n); }
            n = hexgrid.getHex(x - 1, y);
            if (n != null && n.passable) { neighbors.Add(n); }
            n = hexgrid.getHex(x + 1, y - 1);
            if (n != null && n.passable) { neighbors.Add(n); }

            return neighbors;
        }

        public Vector3 getCenter()
        {
            
            float xshift = (float)Math.Cos(Math.PI / (double)6) * radius+spacing;
            float yshift = (float)Math.Sin(Math.PI / (double)6) * radius+spacing;

            return new Vector3(   (this.x * xshift * 2) + (xshift*this.y) , (yshift + radius) * this.y, 1);

        }

        //public void Update(Ray mouseray)
        //{
        //    if (mouseray.Intersects(boundsphere) != null) { color = Color.Red; }
        //    else color = hexcolor;
        //}

        public bool IsMouseOver(Ray mouseray)
        {
            if (mouseray.Intersects(boundsphere) != null) { return true; }
            else return false;
        }


        public void Draw(Matrix world, Matrix view, Matrix projection)
        {
            if (color == Color.Black) { return; }
            world.Translation = world.Translation + getCenter();
            HexModel.Draw(world, view, projection, color);
            
        }

        public void DrawObject(Matrix world, Matrix view, Matrix projection)
        {
            
            if (gameobject != null) { gameobject.Draw(world, view, projection); }
        }

        public void AddObject(GameObject go)
        {
            gameobject = go;
        }

        public GameObject GetGameObject() {
            return gameobject;
        }

        public void RemoveObject() {
            gameobject = null;
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }


        
    }
}