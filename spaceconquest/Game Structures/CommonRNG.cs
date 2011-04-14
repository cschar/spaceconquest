using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    public class CommonRNG
    {
        private static Int64 baseSeed = 4856101800003394322;
        private static Int64 reset = 4856101800003394322;
        public static Random rand = new Random();
        //public static Int64 getRandom(Int64 seed) {
        //    //Console.Write("Get Random: " + seed + " -> ");
        //    rand = new Random((int)seed);
        //    Int64 ret = rand.Next();
        //    //baseSeed = baseSeed ^ seed;
        //    //Console.WriteLine(ret);
        //    //return baseSeed ^ seed;
        //    return ret;

        //}

        public static Int64 getRandom(Int64 seed) //seed does nothing here
        {
            Int64 ret = rand.Next();
            return ret;

        }

        public static void resetSeed() {
            baseSeed = reset;
            //Console.WriteLine("Random Reset");
        }

    }
}

