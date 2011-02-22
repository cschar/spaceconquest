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
    /// <summary>
    /// Options screen
    /// 
    ///  Options to lower,increase volume
    /// and iterate over preloaded tracks
    /// 
    /// To DO:  ? 
    /// 
    /// Author: Cody Scharfe
    /// </summary>
    class OptionsScreen : Screen    
    {
        public List<MenuButton> buttons;
        private MouseState mousestateold = Mouse.GetState();
        private SpriteBatch batch;
        private Color volBarColor = Color.FromNonPremultiplied(130, 245, 100, 100); //greenish
        private Texture2D volBarTexture;
        private Vector2 volBarLocation;


        public OptionsScreen(SpriteBatch sb, SpriteFont sf)
        {
            buttons = new List<MenuButton>();
            buttons.Add(new MenuButton(new Rectangle(225, 200, 150, 40), sb, sf, "Volume + ", IncreaseVolume));
            buttons.Add(new MenuButton(new Rectangle(225, 250, 150, 40), sb, sf, "Volume - ", DecreaseVolume));
            buttons.Add(new MenuButton(new Rectangle(225, 300, 150, 40), sb, sf, "Next Track ", PlayNextTrack));
            buttons.Add(new MenuButton(new Rectangle(225, 350, 200, 40), sb, sf, "Back to Title ", MenuManager.ClickTitle));
            batch = sb;
            volBarLocation = new Vector2(400, 200);

            volBarTexture = new Texture2D(batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            volBarTexture.SetData(new[] { volBarColor });
        }

        public void DecreaseVolume(Object o, EventArgs e)
        {
            Game1.jukeBox.Volume -= 0.1f;

        }

        public void IncreaseVolume(Object o, EventArgs e)
        {
            Game1.jukeBox.Volume += 0.1f;
        }

        public void PlayNextTrack(Object o, EventArgs e)
        {
            Game1.jukeBox.goToNextTrack();
            Game1.jukeBox.play();
        }


        

        public void Update()
        {
            
            MouseState mousestate = Mouse.GetState();
            foreach (MenuButton mb in buttons)
            {
                mb.Update(mousestate, mousestateold);
            }
            mousestateold = mousestate;


         
        }

        public void Draw()
        {
            //draw buttons (if highlighted or not! )
            foreach (MenuComponent mb in buttons)
            {
                mb.Draw();
            }

            //Draw Volume bars
            int bars = (int) Math.Round(10.0f * Game1.jukeBox.Volume, 0) ;
   
            for (int i = 0; i < bars; i++)
            {
                Rectangle volBar = new Rectangle((int)volBarLocation.X + i * 10, (int)volBarLocation.Y, 7, 30);
                batch.Draw(volBarTexture, volBar, Color.White);
            }
        }

      
    }
}
