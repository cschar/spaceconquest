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
    class StatusMenu
    {
        Unit selectedunit;
        protected Rectangle area;
        protected List<MenuComponent> menucomponents = new List<MenuComponent>();
        TextLine nameline;
        TextLine healthline;
        TextLine buildLine;
        TextLine upComingLine;


        //TextLine
        Texture2D texture;
        Color currentcolor = new Color(0, 0, 0, 150);
        //Color barcolor = Color.FromNonPremultiplied(130, 245, 100, 100);
        public bool visible = true;
        public bool showbackround = true;

        private ProgressBar healthBar;
        private ProgressBar buildBar;

        public StatusMenu(Rectangle r)
        {
            area = r;
            texture = new Texture2D(MenuManager.batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            nameline = new TextLine(new Rectangle(area.Left + 5, area.Top + 10, 200, 20), "dummy");
            healthline = new TextLine(new Rectangle(area.Left + 5, area.Top + 30, 100, 20), "dummy2");
            healthBar = new ProgressBar(area.Left + 55, area.Top + 39, 70, 20, 10, false, Color.Green);

            buildLine = new TextLine(new Rectangle(area.Left + 5, area.Top + 55, 100, 20), "dummy3");
            buildBar = new ProgressBar(area.Left + 82, area.Top + 55 + 9, 70, 20, 10, false, Color.Brown);
            upComingLine = new TextLine(new Rectangle(area.Left + 5, area.Top + 77, 100, 20), "");
            

            menucomponents.Add(nameline);
            //add healthBar before health Line
            menucomponents.Add(healthBar);
            menucomponents.Add(healthline);

            menucomponents.Add(buildBar);
            menucomponents.Add(buildLine);
            menucomponents.Add(upComingLine);

        }

        public void Update(Unit u)
        {
            selectedunit = u;

            //Update Health Display
            int MaxHealth = u.getMaxHealth();
            int CurHealth = u.getHealth();
            healthBar.SetGoalNumber(MaxHealth);
            healthBar.SetCurrentNumber(CurHealth);

            nameline.text = u.GetType().Name;
            healthline.text = "Health:    " + CurHealth + "/" + MaxHealth;

            //build display
            if (u is Planet)
            {
                Planet tmpPlanet = (Planet)u;
                List<Unit> upComingUnits = tmpPlanet.BuildQueue;
                if (upComingUnits.Count > 0)
                {
                    buildBar.IsVisible = true;
                    Ship tmpShip = (Ship)upComingUnits[0];
                    String shipname = tmpShip.GetShipType().modelstring;
                    int MaxBuildTime = tmpShip.GetBuildTime();
                    int nextBuildTime = tmpPlanet.BuildTimes[0];
                    buildLine.text = "Build Time:  " + nextBuildTime + "/" + MaxBuildTime + "  -> " + shipname;
                    //Any more ships queued up?

                    upComingLine.text = "";
                    if (upComingUnits.Count > 1)
                    {
                        upComingLine.text = "Next : ";
                        for(int i = 1; i < upComingUnits.Count; i++){
                            Ship nextShip = (Ship) upComingUnits[i];
                            upComingLine.text += "[" + nextShip.shiptype.modelstring.Substring(0, 6) + "]"; 
                        }
                    }


                    buildBar.SetGoalNumber(MaxBuildTime);
                    buildBar.SetCurrentNumber(nextBuildTime);
                }
                else
                {
                    buildLine.text = "No Ships Being Built";
                    buildBar.IsVisible = false;
                }
            }
            if (u is Carrier)
            {
                upComingLine.text = "Carrying: " + ((Carrier)u).GetLoad();
                buildLine.text = "";
                buildBar.IsVisible = false;
            }
            else
            {
                upComingLine.text = "";
                buildLine.text = "";
                buildBar.IsVisible = false;
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
