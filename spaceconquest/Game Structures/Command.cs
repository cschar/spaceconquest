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
        public enum Action {None = 5, Move = 4, Fire = 1, Jump = 6, Enter = 0, Colonize = 7, Upgrade = 3, Build = 9};
        public readonly Action action;
        public readonly ShipType shiptype;
        //public readonly Type shiptype;

        public Command(Hex3D sh, Hex3D th, Action a)
        {
            if( sh != null) start = new Tuple<int,int,int>(sh.hexgrid.index,sh.x,sh.y);
            if (th != null) target = new Tuple<int, int, int>(th.hexgrid.index, th.x, th.y);
            action = a;
        }

        public Command(Hex3D sh, Hex3D th, Action a, ShipType s)
        {
            start = new Tuple<int, int, int>(sh.hexgrid.index, sh.x, sh.y);
            target = new Tuple<int, int, int>(th.hexgrid.index, th.x, th.y);
            action = a;
            shiptype = s;
            //if (shiptype.IsSubclassOf(Ship)
        }

        public override string ToString()
        {
            return "";//starthex.ToString() + " : " + action.ToString() + " : " + targethex.ToString();
        }

        public override int GetHashCode() //we're gonna hash by starthex so that only one command per unit will be used. Also by ship so you can queue multiple ships.
        {
            int i;
            if (action == Action.Move || action == Action.Jump || action == Action.Enter) { i = 1; }
            else { i = 2; }
            return (start.GetHashCode() + i.GetHashCode()).GetHashCode() ;
        }

        public override bool Equals(object obj)
        {
            return (this.GetHashCode() == obj.GetHashCode());
        }

    }
}