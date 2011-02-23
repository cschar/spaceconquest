using System;
using System.Collections.Generic;
using System.Linq;

namespace spaceconquest
{
    abstract class MiddleMan
    {
        private List<Command> queue;
        //This will add a move to the MiddleMan's internal list of moves
        public void AddCommand(Command cmd)
        {
            this.queue.Add(cmd);
        }
        //protected abstract void EndTurnHelper(List<Command> q);
        public void EndTurn() {
            //EndTurnHelper(queue);
            queue.Clear();
        }

    }
}
