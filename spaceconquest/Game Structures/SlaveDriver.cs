using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class SlaveDriver
    {
        //Galaxy galaxy;
        HashSet<Command> commands = new HashSet<Command>(); //hashset so that we ignore multiple commands to one unit


        public void Recieve(List<Command> cl)
        {
            Console.WriteLine("Recieved " + cl.Count + " Commands");
            cl.Reverse(); //reverse the list because we want the most recent command to each unit to be the one recorded
            foreach (Command c in cl)
            {
                commands.Add(c);
            }
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
                if (subject != null && subject is Ship)
                {
                    if (c.targethex.GetGameObject() != null) { return false; }
                    ((Ship)subject).move(c.targethex);
                    return true;
                }
            }
            if (c.action == Command.Action.Fire)
            {
                subject = c.starthex.GetGameObject();
                if (subject != null && subject is Warship)
                {
                    if (c.targethex.GetGameObject() == null) { return false; }
                    ((Warship)subject).Attack((Unit)c.targethex.GetGameObject()); //i should probly check that the target is a unit
                    return true;
                }
            }
            if (c.action == Command.Action.Jump)
            {
                subject = c.starthex.GetGameObject();
                if (subject != null && subject is Ship)
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
