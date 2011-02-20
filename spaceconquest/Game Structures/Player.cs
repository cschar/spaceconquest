using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        Planet startingPlanet;
        //some sort of color

        public Player(Planet start, String n) {

            army = new List<Unit>();
            army.Add(start);
            start.setAffiliation(this);
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
    }
}
