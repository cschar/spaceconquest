using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class Galaxy
    {
        public List<SolarSystem3D> systems;
        public String gName;

        public Galaxy (String g, int size){
            gName = g;
            systems = new List<SolarSystem3D>(size);
            for (int i = 0; i < size; i++) {
                //systems.Add(new SolarSystem3D()); //figure out the parameters. 
            }
        }
    }
}
