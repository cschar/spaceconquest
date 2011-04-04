using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class QueuedCommand
    {
        public readonly Unit agent;
        public readonly Hex3D targetHex;
        public readonly Command.Action order;
        public readonly ShipType shiptype;
        public readonly int priority;

        public QueuedCommand(Command C, Galaxy g, int offset) {
            agent = (Unit)g.GetHex(C.start).GetGameObject();
            targetHex = g.GetHex(C.target);
            order = C.action;
            shiptype = C.shiptype;
            priority = 10*(int)order+offset;

        }
        //For moves-before-bording only
        public QueuedCommand(Ship s, Hex3D h, int offset) {
            agent = s;
            targetHex = h;
            order = Command.Action.Move;
            shiptype = null;
            priority = 50 + offset;
        }

        public static List<QueuedCommand> QueuedCommandList(Command C, Galaxy g){
            List<QueuedCommand> qcs = new List<QueuedCommand>();
            int dist = ((Ship)(g.GetHex(C.start).GetGameObject())).getSpeed();
            for (int i = 0; i < dist; i++) {
                qcs.Add(new QueuedCommand(C, g, i));
            }
            return qcs;
        }
    }
}
