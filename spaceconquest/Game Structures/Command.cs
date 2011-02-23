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
        public enum Action {None = 1, Move = 2, Fire = 3, Jump = 4, Enter = 5, Colonize = 6, Upgrade = 7, Build = 8};
        public readonly Action action;
        public readonly Ship ship;

        public Command(Hex3D sh, Hex3D th, Action a)
        {
            starthex = sh;
            targethex = th;
            action = a;
        }

        public Command(Hex3D sh, Hex3D th, Action a, Ship s)
        {
            starthex = sh;
            targethex = th;
            action = a;
            ship = s;
        }

        public override string ToString()
        {
            return starthex.ToString() + " : " + action.ToString() + " : " + targethex.ToString();
        }

        public override int GetHashCode() //we're gonna hash by starthex so that only one command per unit will be used. Also by ship so you can queue multiple ships.
        {
            //int i;
            //if (action == Action.Move || 
            return (starthex.GetHashCode() + action.GetHashCode()).GetHashCode() ;
        }

    }
}