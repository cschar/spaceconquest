using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace spaceconquest
{
    class SlaveDriver
    {
        Map map;
        Galaxy galaxy;
        HashSet<Command> commands = new HashSet<Command>(); //hashset so that we ignore multiple commands to one unit
        //Player player;

        public SlaveDriver(Map m)
        {
            map = m;
            galaxy = map.galaxy;
        }

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
            commands.OrderBy(Sorter);

            Console.WriteLine("Executing " + commands.Count + " Commands");
            foreach (Command c in commands)
            {
                ExecuteCommand(c);
            }
            commands.Clear();

            foreach (Player p in map.players)
            {
                List<Unit> newlist = new List<Unit>();
                newlist.AddRange(p.army);
                foreach (Unit u in newlist)
                {
                    if (u is Planet) { ((Planet)u).upkeep(); }
                    if (u is Asteroid) { ((Asteroid)u).upkeep(); }
                }
            }
        }

        private int Sorter(Command c) { return (int)c.action; }

        private bool ExecuteCommand(Command c)
        {
            Console.WriteLine(c.ToString());
            GameObject subject;
            if (c.action == Command.Action.Move)
            {
                subject = galaxy.GetHex(c.start).GetGameObject();
                if (subject != null && subject is Ship)
                {
                    if (galaxy.GetHex(c.target).GetGameObject() != null) { return false; }
                    ((Ship)subject).move(galaxy.GetHex(c.target));
                    return true;
                }
            }
            if (c.action == Command.Action.Fire)
            {
                subject = galaxy.GetHex(c.start).GetGameObject();
                if (subject != null && subject is Warship)
                {
                    if (galaxy.GetHex(c.target).GetGameObject() == null) { return false; }
                    ((Warship)subject).Attack((Unit)galaxy.GetHex(c.target).GetGameObject()); //i should probly check that the target is a unit
                    return true;
                }
            }
            if (c.action == Command.Action.Jump)
            {
                subject = galaxy.GetHex(c.start).GetGameObject();
                if (subject != null && subject is Ship)
                {
                    if (galaxy.GetHex(c.target).GetGameObject() != null) { return false; }
                    ((Ship)subject).move(galaxy.GetHex(c.target));
                    return true;
                }
            }
            if (c.action == Command.Action.Build)
            {
                subject = galaxy.GetHex(c.start).GetGameObject();
                if (subject != null && subject is Planet)
                {
                    //if (galaxy.GetHex(c.target).GetGameObject() != null) { return false; }
                    ((Planet)subject).build(c.shiptype.CreateShip());
                    return true;
                }
            }

            if (c.action == Command.Action.Colonize)
            {
                subject = galaxy.GetHex(c.start).GetGameObject();
                if (subject != null && subject is Ship)
                {
                    if (galaxy.GetHex(c.target).GetGameObject() is Planet)
                    {
                        if (((Planet)galaxy.GetHex(c.target).GetGameObject()).getAffiliation() != null) { return false; }
                        ((Planet)galaxy.GetHex(c.target).GetGameObject()).setAffiliation(((Ship)subject).getAffiliation());
                        return true;
                    }
                    if (galaxy.GetHex(c.target).GetGameObject() is Asteroid)
                    {
                        if (((Asteroid)galaxy.GetHex(c.target).GetGameObject()).getAffiliation() != null) { return false; }
                        ((Asteroid)galaxy.GetHex(c.target).GetGameObject()).setAffiliation(((Ship)subject).getAffiliation());
                        return true;
                    }
                }
            }

            return false;
        }

    
    }
}
