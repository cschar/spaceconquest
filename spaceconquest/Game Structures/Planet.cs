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
    [Serializable]
    class Planet:Unit
    {
        private String name;
        private List<Unit> buildQueue;
        private List<int> buildTimes ;

        public override void kill(){
            affiliation.army.Remove(this);
            this.affiliation = null;
            this.maxHealth = 1;
            //this.hue = Color.Gray;
        }

        public Planet(String n, Hex3D loc) {
            this.SetHex(loc);
            this.name = n;
            loc.defaultcolor = Color.Black;
            buildQueue = new List<Unit>();
            buildTimes = new List<int>();
            //loc.passable = false;
        }

        //Will cange to Ship ship when we have the class. 
        public void build(Ship ship) {
            int cost = ship.getCost();
            if (affiliation.payMetal(cost)) {
                buildQueue.Add(ship);
                buildTimes.Add(ship.getTime());
            }
        }

        public override void upkeep() {
            this.health = this.getMaxHealth();
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
                            buildQueue.ElementAt(i).setAffiliation(affiliation);
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



        private float angle = 0;
        private float angleIncr = 0.3f;

        public override void Draw(Matrix world, Matrix view, Matrix projection)
        {
            //world = world + Matrix.CreateTranslation(getCenter());
            // if (affiliation == null) { SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, Color.Gray, 30); }
            // else SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, affiliation.color, 30);

            if (affiliation == null) { PlanetModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, Color.Gray, 30, false, angle); }
            else PlanetModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, affiliation.color, 30, true, angle);

            angle += angleIncr;
            if (angle > 360) angle = 0;

            for (int i = this.health; i > 1; i--)
            {
                SphereModel.Draw(Matrix.CreateTranslation(getCenter()) * world, view, projection, new Color(0,50 , 255, 50), 50 + 5 * i);
            }

        }

    }
}
