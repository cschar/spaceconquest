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
    class CommandMenu : MenuList
    {
        GameScreen gamescreen;
        //Command.Action clickedaction;
        //MiddleMan middleman;

        IconButton move;
        IconButton fire;
        IconButton jump;
        IconButton colonize;
        IconButton enter;
        IconButton unload;
        bool ship = false;

        public CommandMenu(Rectangle r, GameScreen gs) : base(r)
        {
            //wierd syntax
            this.currentcolor = new Color(0, 0, 0, 150);
            padding = 5;
            gamescreen = gs;
            //clickedaction = gs.clickedaction;
            //middleman = gs.middleman;
        }

        public void AddShipStuff()
        {
            move = AddNewCommand(0, 0, "MoveButton.png", MoveClick);
            fire = AddNewCommand(1, 0, "AttackButton.png", FireClick);
            jump = AddNewCommand(1, 1, "JumpButton.png", JumpClick);
            colonize = AddNewCommand(0, 2, "ColonizeButton.png", ColonizeClick);
            enter = AddNewCommand(1, 2, "EnterButton.png", EnterClick);
            unload = AddNewCommand(2, 2, "ExitButton.png", UnloadClick);
            ship = true;
        }

        public IconButton AddNewCommand(int x, int y, String iconaddress, EventHandler c)
        {
            IconButton ib = new IconButton(new Rectangle(area.Left + padding + x * (60 + padding), area.Top + padding + y * (60 + padding), 60, 60), iconaddress, c);
            this.Add(ib);
            return ib;
        }

        public IconButton AddShipCommand(int x, int y, String iconaddress, ShipType c)
        {
            IconButton ib = new IconButton(new Rectangle(area.Left + padding + x * (60 + padding), area.Top + padding + y * (60 + padding), 60, 60), iconaddress, delegate(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.None; gamescreen.middleman.AddCommand(new Command(gamescreen.selectedhex, gamescreen.selectedhex, Command.Action.Build, c)); });
            this.Add(ib);
            return ib;
        }

        public override void Update(MouseState mscurrent, MouseState msold)
        {

            if (ship)
            {
                if (gamescreen.selectedhex != null && gamescreen.selectedhex.GetGameObject() != null)
                {
                    Hex3D hex = gamescreen.selectedhex;
                    GameObject ga = hex.GetGameObject();
                    if (ga is Ship)
                    {
                        Ship selected = (Ship)ga;
                        if (selected.shiptype.canenter) { enter.Update(mscurrent,msold); }
                        if (selected.shiptype.cancolonize) { colonize.Update(mscurrent, msold); }
                        if (selected is Warship) { fire.Update(mscurrent, msold); }
                        if (selected.shiptype.canjump && hex.hexgrid.GetWarpable().Contains(hex) ) { jump.Update(mscurrent, msold); }
                        if (selected is Carrier && ((Carrier)selected).GetLoad() > 0) { unload.Update(mscurrent, msold); }
                        move.Update(mscurrent, msold);
                    }
                }
            }

            else
            {
                base.Update(mscurrent, msold);
            }
        }

        public override void Draw()
        {
            if (!visible) { return; }
            if (ship)
            {
                if (gamescreen.selectedhex != null && gamescreen.selectedhex.GetGameObject() != null)
                {
                    Hex3D hex = gamescreen.selectedhex;
                    GameObject ga = hex.GetGameObject();
                    if (ga is Ship)
                    {
                        Ship selected = (Ship)ga;
                        if (selected.shiptype is MiningShip)
                        {
                            MenuManager.batch.DrawString(MenuManager.font, selected.GetMiningRobots() + "/3 robot packs", new Vector2(colonize.getArea().X , colonize.getArea().Y - 50), Color.Yellow);

                        }
                        if (selected.shiptype.canenter) { enter.Draw(); }
                        if (selected.shiptype.cancolonize) { colonize.Draw(); }
                        if (selected is Warship) { fire.Draw(); }
                        if (selected.shiptype.canjump && hex.hexgrid.GetWarpable().Contains(hex)) { jump.Draw(); }
                        if (selected is Carrier && ((Carrier)selected).GetLoad() > 0) { unload.Draw(); }
                        move.Draw();
                    }
                }
                if (showbackround) MenuManager.batch.Draw(texture, area, currentcolor);
            }

            else
            {
                base.Draw();
            }
        }

        void MoveClick(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.Move; Console.WriteLine("clicked move"); }
        void FireClick(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.Fire; }
        void EnterClick(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.Enter; }
        void UnloadClick(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.Enter; gamescreen.middleman.AddCommand(new Command(gamescreen.selectedhex, gamescreen.selectedhex, gamescreen.clickedaction)); gamescreen.clickedaction = Command.Action.None; }
        void JumpClick(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.Jump; gamescreen.space = gamescreen.galaxy; }
        void ColonizeClick(Object o, EventArgs e) { gamescreen.clickedaction = Command.Action.Colonize; }

    }
}

