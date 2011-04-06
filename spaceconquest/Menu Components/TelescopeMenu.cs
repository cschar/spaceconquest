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
    class TelescopeMenu
    {
        SolarSystem3D mousesystem;
        protected Rectangle area;
        protected List<MenuComponent> menucomponents = new List<MenuComponent>();
        TextLine planets, asteroids, ships, header;
        //TextLine
        Texture2D texture;
        Color currentcolor = new Color(0, 0, 0, 150);
        //Color barcolor = Color.FromNonPremultiplied(130, 245, 100, 100);
        public bool visible = true;
        public bool showbackround = true;
        Player plyr;



        public TelescopeMenu(Rectangle r, Player p)
        {
            plyr = p;
            area = r;
            texture = new Texture2D(MenuManager.batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            planets = new TextLine(new Rectangle(area.Left + 5, area.Top + 30, 200, 20), "dummy1");
            asteroids = new TextLine(new Rectangle(area.Left + 5, area.Top + 50, 200, 20), "dummy2");
            ships = new TextLine(new Rectangle(area.Left + 5, area.Top + 70, 200, 20), "dummy3");
            header = new TextLine(new Rectangle(area.Left + 5, area.Top + 10, 200, 20), "Object:    Friend Foe Total");



            menucomponents.Add(header);
            menucomponents.Add(planets);
            //add healthBar before health Line
            menucomponents.Add(asteroids);
            menucomponents.Add(ships);

        }

        public void Update(SolarSystem3D u)
        {
            if (mousesystem == u) {return;}
            mousesystem = u;
            Boolean vis = false;
            foreach (Unit u1 in plyr.army) {
                SolarSystem3D u1ss = u1.hex.hexgrid;
                if (u1ss == mousesystem) { Console.WriteLine("I can see!"); vis = true; break; }
                
                if (mousesystem.neighbors.Contains(u1ss) && u1 is Ship && ((Ship)u1).shiptype is Telescope) { vis = true; break; }
            }
            if (vis)
            {
                int totalShips = 0;
                int totalPlanets = 0;
                int totalRoids = 0;
                int enemyShips = 0;
                int enemyPlanets = 0;
                int enemyRoids = 0;
                int myShips = 0;
                int myPlanets = 0;
                int myRoids = 0;
                foreach (Hex3D h1 in mousesystem.getHexes())
                {
                    GameObject go = h1.GetGameObject();
                    if (go != null && go is Unit)
                    {
                        Unit u1 = ((Unit)go);
                        Player p1 = u1.getAffiliation();
                        if (u1 is Asteroid)
                        {
                            totalRoids++;
                            if (p1 == null) { }
                            else if (p1 == plyr) { myRoids++; }
                            else { enemyRoids++; }
                        }
                        if (u1 is Planet)
                        {
                            totalPlanets++;
                            if (p1 == null) { }
                            else if (p1 == plyr) { myPlanets++; }
                            else { enemyPlanets++; }
                        }
                        if (u1 is Ship)
                        {
                            totalShips++;
                            if (p1 == null) { }
                            else if (p1 == plyr) { myShips++; }
                            else { enemyShips++; }
                        }
                    }
                }
                String[] spacing = new String[3];
                if (myPlanets < 10) { spacing[0] = " "; }
                else { spacing[0] = ""; }
                if (enemyPlanets < 10) { spacing[1] = " "; }
                else { spacing[1] = ""; }
                if (totalPlanets < 10) { spacing[2] = " "; }
                else { spacing[2] = ""; }
                planets.text = "Planets:       " + spacing[0] + myPlanets + "  " + spacing[1] + enemyPlanets + "    " + spacing[2] + totalPlanets;
                if (myRoids < 10) { spacing[0] = " "; }
                else { spacing[0] = ""; }
                if (enemyRoids < 10) { spacing[1] = " "; }
                else { spacing[1] = ""; }
                if (totalRoids < 10) { spacing[2] = " "; }
                else { spacing[2] = ""; }
                asteroids.text = "Asteroids:     " + spacing[0] + myRoids + "  " + spacing[1] + enemyRoids + "    " + spacing[2] + totalRoids;
                if (myShips < 10) { spacing[0] = " "; }
                else { spacing[0] = ""; }
                if (enemyShips < 10) { spacing[1] = " "; }
                else { spacing[1] = ""; }
                if (totalShips < 10) { spacing[2] = " "; }
                else { spacing[2] = ""; }
                ships.text = "Vessels:       " + spacing[0] + myShips + "  " + spacing[1] + enemyShips + "    " + spacing[2] + totalShips;



            }
            else
            {

                //               "Object:    Friend Foe Total"
                planets.text = "Planets:       ??  ??    ??";
                asteroids.text = "Asteroids:     ??  ??    ??";
                ships.text = "Vessels::      ??  ??    ??";
            }
            
        }

        public void Draw()
        {
            if (visible)
            {
                if (showbackround) MenuManager.batch.Draw(texture, area, currentcolor);
                foreach (MenuComponent mc in menucomponents)
                {
                    mc.Draw();
                }
            }
        }

        public void Hide()
        {
            visible = false;
        }

        public void Show()
        {
            visible = true;
        }
    }
}
