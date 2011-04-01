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
    class Player
    {
        int id;
        int metal;
        int score;
        public static int playerIDs = 0;
        String name;
        public List<Unit> army;
        public readonly Planet startingPlanet;
        public Color color;

        public Player(Planet start, String n) {
            Random rand = new Random(n.GetHashCode());
            color = new Color(rand.Next(255), rand.Next(255), rand.Next(255));
            army = new List<Unit>();
            army.Add(start);
            start.setAffiliation(this);
            startingPlanet = start;
            metal = 1000000;
            score = 0;
            name = n; 
            id = playerIDs;
            playerIDs++;
        }
        public String getName() {
            return name;
        }
        public int getMetal() {
            return metal;
        }
        public Boolean payMetal(int cost) {
            if (cost <= metal) {
                metal -= cost;
                return true;
            }
            return false;
        }

        public void giveMetal(int x)
        {
            metal = metal + x;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Player)) { return false; }
            else return id.Equals(((Player)obj).id);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
