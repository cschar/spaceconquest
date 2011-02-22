using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace spaceconquest
{
    class GameScreen : Screen
    {
        //in the future, the map would be here to but i dont have a map class yet
        SolarSystem3D solar; //current solar system
        //maybe make solar system and galaxy implement the same interface so we can draw either of them in here

        List<MenuComponent> components = new List<MenuComponent>();
        List<Command> commands = new List<Command>();
        MenuList shipmenu;
        Command.Action clickedaction = Command.Action.None;
        SlaveDriver driver = new SlaveDriver();

        Color selectedcolor = Color.Green;
        Color movecolor = new Color(0,255,0);
        Color mousecolor = new Color(0,255,255);
        public Hex3D selectedhex;
        Hex3D nullhex = new Hex3D(0, 0, null); //i use this because i dont feel like checking for null on the selected hex
        MouseState oldmousestate = Mouse.GetState();
        Hex3D oldmousehex;

        public static float scrollspeed = 6f;
        public static float rotatespeed = .01f;
        public static float zoomspeed = 10f;
        float xr = 0;
        float yr = 0;
        float zr = 0;
        float height = 700;
        Vector3 offset;

        public GameScreen()
        {
            selectedhex = nullhex;
            solar = new SolarSystem3D(10, new Rectangle(0,0,800, 600));
            offset = new Vector3(0,0,0);
            shipmenu = new MenuList(new Rectangle(600, 400, 200, 200));
            components.Add(shipmenu);
            shipmenu.Add(new MenuButton(new Rectangle(605, 405, 60, 60), MenuManager.batch, MenuManager.font, "Move", MoveClick));
            shipmenu.Add(new MenuButton(new Rectangle(670, 405, 60, 60), MenuManager.batch, MenuManager.font, "Attack", FireClick));
            shipmenu.Add(new MenuButton(new Rectangle(605, 470, 60, 60), MenuManager.batch, MenuManager.font, "Enter", EnterClick));
            shipmenu.Add(new MenuButton(new Rectangle(670, 470, 60, 60), MenuManager.batch, MenuManager.font, "Jump", JumpClick));
            shipmenu.Add(new MenuButton(new Rectangle(735, 405, 60, 60), MenuManager.batch, MenuManager.font, "Upgrade", UpgradeClick));
            shipmenu.Add(new MenuButton(new Rectangle(605, 535, 60, 60), MenuManager.batch, MenuManager.font, "Colonize", ColonizeClick));
            //shipmenu.Add(new MenuButton(new Rectangle(605, 405, 60, 60), MenuManager.batch, MenuManager.font, "Build", BuildClick));
        }


        void MoveClick(Object o, EventArgs e) { clickedaction = Command.Action.Move; Console.WriteLine("clicked move"); }
        void FireClick(Object o, EventArgs e) { clickedaction = Command.Action.Fire; }
        void EnterClick(Object o, EventArgs e) { clickedaction = Command.Action.Enter; }
        void JumpClick(Object o, EventArgs e) { clickedaction = Command.Action.Jump; }
        void UpgradeClick(Object o, EventArgs e) { clickedaction = Command.Action.Upgrade; }
        void ColonizeClick(Object o, EventArgs e) { clickedaction = Command.Action.Colonize; }
        void BuildClick(Object o, EventArgs e) { clickedaction = Command.Action.Build; }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.Space)) { driver.Recieve(commands); commands = new List<Command>(); driver.Execute(); }
            //////camera controls////
            if (keystate.IsKeyDown(Keys.Left)) { offset.X = offset.X + scrollspeed; }
            if (keystate.IsKeyDown(Keys.Right)) { offset.X = offset.X - scrollspeed; }
            if (keystate.IsKeyDown(Keys.Down)) { offset.Y = offset.Y + scrollspeed; }
            if (keystate.IsKeyDown(Keys.Up)) { offset.Y = offset.Y - scrollspeed; }

            //if (keystate.IsKeyDown(Keys.Z)) { xr += rotatespeed; }
            if (keystate.IsKeyDown(Keys.X)) { yr += rotatespeed; }
            if (keystate.IsKeyDown(Keys.C)) { zr += rotatespeed; }
            //if (keystate.IsKeyDown(Keys.A)) { xr -= rotatespeed; }
            if (keystate.IsKeyDown(Keys.S)) { yr -= rotatespeed; }
            if (keystate.IsKeyDown(Keys.D)) { zr -= rotatespeed; }

            if (keystate.IsKeyDown(Keys.Q)) { height += zoomspeed; }
            if (keystate.IsKeyDown(Keys.E)) { height -= zoomspeed; }

            if (yr > 0) { yr = 0; }
            if (yr < (-Math.PI / (float)2)) { yr = (float)(-Math.PI / (float)2); }


            solar.Update(); //just resets the color of hexes
            selectedhex.color = selectedcolor;

            ////////mouse stuff////////
            if (oldmousehex != null && !oldmousehex.Equals(selectedhex)) oldmousehex.color = Hex3D.hexcolor;

            Hex3D mousehex = solar.GetMouseOverHex();
            

            if ((mousestate.LeftButton == ButtonState.Pressed) && (oldmousestate.LeftButton == ButtonState.Released) && !shipmenu.Contains(mousestate.X, mousestate.Y) && mousehex != null)
            {
                //selecting a hex
                if (clickedaction == Command.Action.None)
                {
                    selectedhex = mousehex;
                }

                //selecting a target of a action
                if (clickedaction != Command.Action.None) //should check for null here but i wont for testing purposes
                {
                    Console.WriteLine("a command");
                    commands.Add(new Command(selectedhex, mousehex, clickedaction));
                    clickedaction = Command.Action.None;
                    selectedhex = nullhex;
                }
            }

            
            /////stuff to do if a ship is selected/////
            GameObject selectedobject = selectedhex.GetGameObject();

            if (selectedobject != null && selectedobject is Ship) { shipmenu.Show(); }
            else { shipmenu.Hide(); }

            if (selectedobject != null && selectedobject is Ship)
            {
                foreach (Hex3D h in ((Ship)selectedobject).GetReachable())
                {
                    h.color = movecolor;
                }
            }
            if (selectedobject != null && selectedobject is Warship)
            {
                foreach (Hex3D h in ((Warship)selectedobject).GetShootable())
                {
                    h.color = Color.Red;
                }
            }


            //Color the mousedover hex
            if (mousehex != null && !mousehex.Equals(selectedhex) && !shipmenu.Contains(mousestate.X, mousestate.Y)) { mousehex.color = mousecolor; }

            //update the 2d stuff
            foreach (MenuComponent c in components)
            {
                c.Update(mousestate, oldmousestate);
            }

            oldmousestate = mousestate;
            oldmousehex = mousehex;
        }

        public void Draw()
        {
            
            solar.Draw(offset,xr,yr,zr,height);

            //2D stuff
            foreach (MenuComponent c in components)
            {
                c.Draw();
            }
        }
    }
}
