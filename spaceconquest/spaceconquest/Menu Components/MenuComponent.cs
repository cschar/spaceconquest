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
    abstract class MenuComponent
    {
        event EventHandler clicked;

        public void addClickEvent(EventHandler c)
        {
            clicked += c;
        }

        public abstract bool Contains(int x, int y);


        public virtual void Update(MouseState mscurrent, MouseState msold)
        {
            if (this.Contains(mscurrent.X, mscurrent.Y))
            {
                if ((mscurrent.LeftButton == ButtonState.Released) && (msold.LeftButton == ButtonState.Pressed)) { OnClick(EventArgs.Empty); }
            }
        }

        public abstract void Draw();

        protected virtual void OnClick(EventArgs e)
        {
            if (clicked != null)
                clicked(this, e);
        }
    }
}
