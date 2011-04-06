using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace spaceconquest
{
    [Serializable]
    class Map
    {
        public readonly Galaxy galaxy;
        public readonly List<Player> players;
        private String MapName;
        private Player instancePlayer;

        public Map(int nPlayers, int instanceIndex, String name, Int64 seed)
        {//For if we want a specific name
            Int64 size1 = CommonRNG.getRandom(seed);
            Int64 size2 = CommonRNG.getRandom(size1);

            int s1, s2;

            s1 = 3;// (int)(size1 % (nPlayers + 1));
            s2 = 3;// (int)(size2 % (nPlayers + 1));

            Console.WriteLine("S1, S2 = " + s1 + ", " + s2);

            galaxy = new Galaxy("random galaxy", nPlayers+s1+s2, size2);
            //populate galaxy with non-Units and non-affiliated Units
            List<SolarSystem3D> cands = new List<SolarSystem3D>();
            foreach (SolarSystem3D sys in galaxy.systems) {
                cands.Add(sys);
            }
            players = new List<Player>(nPlayers);
            //Initialize players - agendum when we have a Player class
            CommonRNG.resetSeed();
            for (int i = 0; i < nPlayers; i++) {
                size2 = CommonRNG.getRandom(size2);
                SolarSystem3D sTemp = cands.ElementAt((int)(size2 % cands.Count));
                cands.Remove(sTemp);
                size2 = CommonRNG.getRandom(size2);
                players.Add(new Player(sTemp.planets.ElementAt((int)(size2 % sTemp.planets.Count)), "Player " + i));
            }

            //i.e. give each player a starting planet. 
            this.MapName = name;
            this.instancePlayer = players.ElementAt(instanceIndex);
        }

        //public Map(Galaxy startingGalaxy, int nPlayers, String name, List<Planet> startingPlanets) //For designed maps. 
        //assert length of startingplanets is nPlayers, and that each comes from within startingGalaxy

        public Player SetPlayer(int i)
        {
            instancePlayer = players.ElementAt(i);
            return instancePlayer;
        }

        public SolarSystem3D GetHomeSystem()
        {
            return instancePlayer.startingPlanet.hex.hexgrid;
        }

        public Player GetInstancePlayer()
        {
            return instancePlayer;
        }

    }
}
