using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    [Serializable()]
    class Command
    {
        //public readonly Hex3D starthex;
        public readonly Tuple<int, int, int> start;
        public readonly Tuple<int, int, int> target;
        //public readonly Hex3D targethex;
        public enum Action {None = 1, Move = 2, Fire = 3, Jump = 4, Enter = 5, Colonize = 6, Upgrade = 7, Build = 8};
        public readonly Action action;
        public readonly Ship ship;

        public Command(Hex3D sh, Hex3D th, Action a)
        {
            start = new Tuple<int,int,int>(sh.hexgrid.index,sh.x,sh.y);
            target = new Tuple<int, int, int>(th.hexgrid.index, th.x, th.y);
            action = a;
        }

        public Command(Hex3D sh, Hex3D th, Action a, Ship s)
        {
            start = new Tuple<int, int, int>(sh.hexgrid.index, sh.x, sh.y);
            target = new Tuple<int, int, int>(th.hexgrid.index, th.x, th.y);
            action = a;
            ship = s;
        }

        public override string ToString()
        {
            return "";//starthex.ToString() + " : " + action.ToString() + " : " + targethex.ToString();
        }

        public override int GetHashCode() //we're gonna hash by starthex so that only one command per unit will be used. Also by ship so you can queue multiple ships.
        {
            //int i;
            //if (action == Action.Move || 
            return (start.GetHashCode() + action.GetHashCode()).GetHashCode() ;
        }

    }
}