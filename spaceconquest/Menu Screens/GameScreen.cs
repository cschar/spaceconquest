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
        Command.Action clickedaction;

        Color selectedcolor = Color.Teal;
        public Hex3D selectedhex = new Hex3D(0,0,null); //ignore this construction, its just to prevent nulls on the first update
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
            solar = new SolarSystem3D(10, new Rectangle(0,0,800, 600));
            offset = new Vector3(0,0,0);
            shipmenu = new MenuList(new Rectangle(600, 400, 200, 200));
            components.Add(shipmenu);
            shipmenu.Add(new MenuButton(new Rectangle(605, 405, 60, 60), MenuManager.batch, MenuManager.font, "Move", MoveClick));
        }


        void MoveClick(Object o, EventArgs e) { clickedaction = Command.Action.Move; }

        public void Update()
        {
            MouseState mousestate = Mouse.GetState();
            KeyboardState keystate = Keyboard.GetState();


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

            ////mouse stuff
            if (oldmousehex != null && !oldmousehex.Equals(selectedhex)) oldmousehex.color = Hex3D.hexcolor;

            Hex3D mousehex = solar.GetMouseOverHex();
            if (mousehex != null && !mousehex.Equals(selectedhex)) { mousehex.color = Color.Red; }

            if ((mousestate.LeftButton == ButtonState.Released) && (oldmousestate.LeftButton == ButtonState.Pressed) && !shipmenu.Contains(mousestate.X,mousestate.Y))
            {
                if (mousehex != null) { selectedhex = mousehex; }
            }

            oldmousestate = mousestate;
            oldmousehex = mousehex;


            //stuff to do if a ship is selected
            GameObject selectedobject = selectedhex.GetGameObject();

            if (selectedobject != null && selectedobject is Ship) { shipmenu.Show(); }
            else { shipmenu.Hide(); }

            if (selectedobject != null && selectedobject is Ship)
            {
                foreach (Hex3D h in ((Ship)selectedobject).GetReachable())
                {
                    h.color = Color.Pink;
                }
            }
            if (selectedobject != null && selectedobject is Warship)
            {
                foreach (Hex3D h in ((Warship)selectedobject).GetShootable())
                {
                    h.color = Color.Red;
                }
            }
            

            //update the 2d stuff
            foreach (MenuComponent c in components)
            {
                c.Update(mousestate, oldmousestate);
            }
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
