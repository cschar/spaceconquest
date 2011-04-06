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
    abstract class Unit:GameObject
    {
        protected int buildTime = 1;
        protected int buildCost = 1;
        protected Player affiliation;
        protected int health = 1;
        protected int maxHealth = 1;
        //protected Model model = null;

        public Unit(Player p) {
            affiliation = p;
        }

        public Unit() {
            affiliation = null;
        }

        public void setMaxHealth(int mh) {
            if (mh > 0) {
                maxHealth = mh;
            }
        }
        public int getMaxHealth() {
            return maxHealth;
        }
        public abstract void kill(); //will be overwritten by inhereting class

        public void hit(int damage){
            if (damage >= 0) {
                this.setHealth(this.health - damage);
            }
        }
        public void setHealth(int h) {
            this.health = Math.Max(0, Math.Min(h, maxHealth));
            if (this.health <= 0 && this.affiliation != null) {
                this.kill();
            }
        }
        public int getHealth() {
            return health;
        }
        public Player getAffiliation() {
            return affiliation;
        }

        public void setAffiliation(Player p) {
            this.affiliation = p;
            if(p!= null) p.army.Add(this);
        }

        public int getCost() {
            return buildCost;
        }

        public int getTime() {
            return buildTime;
        }

        public virtual void upkeep()
        {
            if (health < maxHealth) { health++; }
        }

        //public abstract void upkeep();

    }
}
