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
        private Color volBarColor = Color.FromNonPremultiplied(130, 245, 100, 100); //greenish
        private Texture2D volBarTexture;
        private Vector2 soundBarPos;
        private Vector2 effectBarPos;
        private int barWidth = 7;
        private int barHeight = 30;
        private GameScreen gameScreen;

        public OptionsScreen(GameScreen s)
        {
            gameScreen = s;
            buttons = new List<MenuButton>();

            //Music Option Buttons
            int SoundX = 125;
            int SoundY = 200;
            soundBarPos = new Vector2(SoundX, SoundY - 50);
            buttons.Add(new MenuButton(new Rectangle(SoundX, 200, 150, 40), "Volume + ", IncreaseSoundVolume));
            buttons.Add(new MenuButton(new Rectangle(SoundX, 250, 150, 40), "Volume - ", DecreaseSoundVolume));
            buttons.Add(new MenuButton(new Rectangle(SoundX, 300, 150, 40), "Next Track ", PlayNextTrack));

            //Sound Option Buttons
            int EffectX = 350;
            int EffectY = 200;
            effectBarPos = new Vector2(EffectX, EffectY - 50);
            buttons.Add(new MenuButton(new Rectangle(EffectX, 200, 150, 40), "Volume + ", IncreaseEffectVolume));
            buttons.Add(new MenuButton(new Rectangle(EffectX, 250, 150, 40), "Volume - ", DecreaseEffectVolume));
          

            //Back button
            buttons.Add(new MenuButton(new Rectangle(125, 550, 150, 40), "Back ", MenuManager.ClickPrevScreen));
            if (gameScreen != null)
            {
                buttons.Add(new MenuButton(new Rectangle(125, 450, 150, 40), "Save Game", SaveGameScreen)); 
            }
         

            volBarTexture = new Texture2D(Game1.device, 1, 1, true, SurfaceFormat.Color);
            volBarTexture.SetData(new[] { volBarColor });
        }


        private void SaveGameScreen(Object o, EventArgs e)
        {
            if (gameScreen != null)
            {
                Console.WriteLine("Saving Game from options menu");
                gameScreen.Save();
            }
        }

        public void DecreaseSoundVolume(Object o, EventArgs e)
        {
            Game1.jukeBox.Volume -= 0.1f;

        }

        public void IncreaseSoundVolume(Object o, EventArgs e)
        {
            Game1.jukeBox.Volume += 0.1f;
        }

        public void PlayNextTrack(Object o, EventArgs e)
        {
            
            Game1.jukeBox.goToNextTrack();
            Game1.jukeBox.play();
        }

        public void IncreaseEffectVolume(Object o, EventArgs e)
        {
            Game1.soundEffectBox.Volume += 0.1f;
            Game1.soundEffectBox.PlaySound("Toggle");
        }
        public void DecreaseEffectVolume(Object o, EventArgs e)
        { 
            Game1.soundEffectBox.Volume -= 0.1f;
            Game1.soundEffectBox.PlaySound("Toggle");
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

            //Draw Sound Volume bars
            int Snd_bars = (int) Math.Round(10.0f * Game1.jukeBox.Volume, 0) ;
            for (int i = 0; i < Snd_bars; i++)
            {
                Rectangle volBar = new Rectangle((int)soundBarPos.X + i * 10, (int)soundBarPos.Y, barWidth, barHeight);
                MenuManager.batch.Draw(volBarTexture, volBar, Color.White);
            }

            //Draw Effect Volume bars
            int Efk_bars = (int)Math.Round(10.0f * Game1.soundEffectBox.Volume, 0);
            for (int i = 0; i < Efk_bars; i++)
            {
                Rectangle EfkBar = new Rectangle((int)effectBarPos.X + i * 10, (int)effectBarPos.Y, barWidth, barHeight);
                MenuManager.batch.Draw(volBarTexture, EfkBar, Color.White);
            }
        }

      
    }
}
