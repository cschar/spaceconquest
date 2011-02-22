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
        public enum Action {None, Move, Fire, Jump, Enter, Colonize, Upgrade, Build};
        public readonly Action action;

        public Command(Hex3D sh, Hex3D th, Action a)
        {
            starthex = sh;
            targethex = th;
            action = a;
        }

        public override string ToString()
        {
            return starthex.ToString() + " : " + action.ToString() + " : " + targethex.ToString();
        }



    }
}
