using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class Command
    {
        public readonly Hex3D starthex;
        public readonly Hex3D targethex;
        public enum Action {Move, Fire, Jump};
        public readonly Action action;

        public Command(Hex3D sh, Hex3D th, Action a)
        {
            starthex = sh;
            targethex = th;
            action = a;
        }



    }
}
