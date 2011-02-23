using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spaceconquest
{
    class Planet:Unit
    {
        private Color hue;
        private String name;
        private List<Unit> buildQueue;
        private List<int> buildTimes ;


        public override void kill(){
            this.affiliation = null;
            this.hue = Color.Gray;
        }

        public Planet(String n, Color c, Hex3D loc) {
            this.SetHex(loc);
            this.hue = c;
            this.name = n;
            loc.defaultcolor = Color.Black;
            buildQueue = new List<Unit>();
            buildTimes = new List<int>();
            //loc.passable = false;
        }

        //Will cange to Ship ship when we have the class. 
        public void build(Ship ship) {
            int cost = 0;
            if (affiliation.payMetal(cost)) {
                buildQueue.Add(ship);
                buildTimes.Add(ship.getTime());
            }
        }

        public void upkeep() {
            if (buildTimes.Count > 0) {
                buildTimes[0] = Math.Max(0, buildTimes[0]-1);
            }
            int j = buildTimes.Count;
            for (int i = 0; i < j; i++) {
                if (buildTimes[i] <= 0) {
                    //produce buildQueue.removeAt(i)
                    foreach (Hex3D neighbor in hex.getNeighbors()) {
                        if (neighbor.GetGameObject() == null) { 
                            buildQueue.ElementAt(i).SetHex(neighbor);
                            buildQueue.RemoveAt(i);
                            buildTimes.RemoveAt(i);
                            i--;
                            j = buildTimes.Count;
                            break;
                        }
                    }
                }
            }
        }


        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            //world = world + Matrix.CreateTranslation(getCenter());
            SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, hue, 30);
        }


    }
}
