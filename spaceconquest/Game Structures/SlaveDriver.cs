using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class SlaveDriver
    {
        //Galaxy galaxy;
        List<Command> commands = new List<Command>();


        public void Recieve(List<Command> c)
        {
            Console.WriteLine("Recieved " + c.Count + " Commands");
            commands.AddRange(c);
        }

        public void Execute()
        {
            Console.WriteLine("Executing " + commands.Count + " Commands");
            foreach (Command c in commands)
            {
                ExecuteCommand(c);
            }
            commands.Clear();
        }

        private bool ExecuteCommand(Command c)
        {
            Console.WriteLine(c.ToString());
            GameObject subject;
            if (c.action == Command.Action.Move)
            {
                subject = c.starthex.GetGameObject();
                if (subject != null)
                {
                    if (c.targethex.GetGameObject() != null) { return false; }
                    ((Ship)subject).move(c.targethex);
                    return true;
                }
            }

            return false;
        }

    
    }
}
