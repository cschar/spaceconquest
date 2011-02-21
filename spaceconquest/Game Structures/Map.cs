using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class Map
    {
        private Galaxy galaxy;
        private List<Player> players;
        private String MapName;

        public Map(int nPlayers, String name)
        {//For if we want a specific name
            galaxy = new Galaxy("test galaxy",1);
            //populate galaxy with non-Units and non-affiliated Units
            players = new List<Player>(nPlayers);
            //Initialize players - agendum when we have a Player class
            //i.e. give each player a starting planet. 
            this.MapName = name;
        }

        //public Map(Galaxy startingGalaxy, int nPlayers, String name, List<Planet> startingPlanets) //For designed maps. 
        //assert length of startingplanets is nPlayers, and that each comes from within startingGalaxy


    }
}
