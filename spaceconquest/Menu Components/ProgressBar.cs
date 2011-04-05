using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace spaceconquest
{
    class ProgressBar : MenuComponent
    {

        private Texture2D barTex;
        private Texture2D perimeterTex;
        private Texture2D remainingTex;
        private int width;
        private int height;
        private bool isVertical;
        private int goalNumber;
        private int curBarNumber;
        private int x;
        private int y;
        private int pad = 2;


        /// <summary>
        /// Constructor for a progresBar widget for menu manager
        /// </summary>
        /// <param name="goalNumber">The number that indicates progressBar is full</param>
        /// <param name="width">GUI width of bar</param>
        /// <param name="height">GUI height ofb ar</param>
        /// <param name="isVertical">sets orientation of progress bar</param>
        /// <param name="barColor">color of bar</param>
        public ProgressBar(int goalNumber, int x, int y, int width, int height, bool isVertical, Color barColor)
        {
            this.goalNumber = goalNumber;
            this.x = x;
            this.y = y;
            this.height = height;
            this.width = width;

            //set bar color
            barTex = new Texture2D(Game1.device, 1, 1, true, SurfaceFormat.Color);
            barTex.SetData(new[] { barColor });

            //set perimeter color
            perimeterTex = new Texture2D(Game1.device, 1, 1, true, SurfaceFormat.Color);
            perimeterTex.SetData(new[] { Color.LightSteelBlue });

            //set remaingin color
            remainingTex = new Texture2D(Game1.device, 1, 1, true, SurfaceFormat.Color);
            remainingTex.SetData(new[] { Color.LightSteelBlue });
        }

        public void SetVertical(bool isVertical)
        {
            this.isVertical = isVertical;
        }
        public void SetGoalNumber(int num)
        {
            this.goalNumber = num;
            //check if this breaks the current bar number
            if (curBarNumber > goalNumber) curBarNumber = goalNumber;
        }
        public void SetCurrentNumber(int curNum)
        {
            if (curNum < 0) { curBarNumber = 0; return; }
            else if (curNum > goalNumber) { curBarNumber = goalNumber; return; }
            else
            {
                curBarNumber = curNum;
            }

        }
        public override bool Contains(int x, int y)
        {
            //do nothing?
            return false;
        }

        public override void Draw()
        {
            //Draw perimeter
            MenuManager.batch.Draw(perimeterTex, new Rectangle(x, y, width, height), Color.White);

            if (isVertical)
            {

                //Draw progressBar  going up bar     
                float progress = (float)curBarNumber / (float)goalNumber;
                int barHeight = (int)(height * progress);
                Rectangle progRect = new Rectangle(x + pad, y + (height - pad - barHeight), width - (2 * pad), barHeight);
                MenuManager.batch.Draw(barTex, progRect, Color.White);

                //Draw remaining progress
            }
            else
            {
                //Draw progresBar going across
                float progress = (float)curBarNumber / (float)goalNumber;
                int barWidth = (int)(width * progress);
                Rectangle progRect = new Rectangle(x + pad, y + pad, barWidth - (2 * pad), height - (2 * pad));
                MenuManager.batch.Draw(barTex, progRect, Color.White);

                //draw remaining progress
                Rectangle remainingProgRect = new Rectangle(x + pad + barWidth, y + pad, width - barWidth - (2 * pad), height - (2 * pad));
                MenuManager.batch.Draw(remainingTex, remainingProgRect, Color.White);
            }
        }
    }
}
