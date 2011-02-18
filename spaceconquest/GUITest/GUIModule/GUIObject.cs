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

namespace GUIModule
{
    public interface GUIObject
    {

        int GetWidth();
        void SetWidth(int newWidth);

        int GetHeight();
        void SetHeight(int newHeight);

        void SetLocation(Vector2 newLocation);
        Vector2 GetLocation();

        Boolean MouseIsOverlapping(MouseState m );

        Boolean HasFocus();
        void setFocus(Boolean hasFocus);

        void Draw(SpriteBatch s);


    }
}
