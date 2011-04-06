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
    class TopMenu : MenuList
    {
        GameScreen gamescreen;
        TextLine resource;

        public TopMenu(Rectangle r, GameScreen gs)
            : base(r)
        {
            //wierd syntax
            padding = 5;
            gamescreen = gs;
            this.currentcolor = new Color(0, 0, 0, 150);

            this.Add(new TextLine(this.area,gamescreen.space.ToString()));
            this.Add(new MenuButton(new Rectangle(area.Left + 200, area.Top, 100, area.Height), "Options", MenuManager.ClickMusicOptions ));
            

            resource = new TextLine(new Rectangle(area.Right - 100, area.Top, 100, area.Height), "banana");
            this.Add(resource);
        }

        public override void Update(MouseState mscurrent, MouseState msold)
        {
            base.Update(mscurrent, msold);
            resource.text = "Metal: " + gamescreen.player.getMetal();
        }

        

    }
}
