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
    class TextInput : MenuComponent
    {
        public String input = "";
        KeyboardState oldstate = Keyboard.GetState();
        Texture2D texture;
        Rectangle area;
        Vector2 stringvector;
        PressedEnter enter;
       // GlobalChatClient chat;

        public TextInput(Rectangle r, PressedEnter e)
        {
            //chat = g;
            enter = e;
            area = r;
            texture = new Texture2D(MenuManager.batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            stringvector = new Vector2(area.Center.X, area.Center.Y) - (MenuManager.font.MeasureString(input) / 2);

        }

        public override void Update(MouseState mscurrent, MouseState msold)
        { this.Update(); }

        public void Update()
        {
            KeyboardState keystate = Keyboard.GetState();
            if (keystate.IsKeyDown(Keys.A) && oldstate.IsKeyUp(Keys.A)) { input += 'A'; }
            if (keystate.IsKeyDown(Keys.B) && oldstate.IsKeyUp(Keys.B)) { input += 'B'; }
            if (keystate.IsKeyDown(Keys.C) && oldstate.IsKeyUp(Keys.C)) { input += 'C'; }
            if (keystate.IsKeyDown(Keys.D) && oldstate.IsKeyUp(Keys.D)) { input += 'D'; }
            if (keystate.IsKeyDown(Keys.E) && oldstate.IsKeyUp(Keys.E)) { input += 'E'; }
            if (keystate.IsKeyDown(Keys.F) && oldstate.IsKeyUp(Keys.F)) { input += 'F'; }
            if (keystate.IsKeyDown(Keys.G) && oldstate.IsKeyUp(Keys.G)) { input += 'G'; }
            if (keystate.IsKeyDown(Keys.H) && oldstate.IsKeyUp(Keys.H)) { input += 'H'; }
            if (keystate.IsKeyDown(Keys.I) && oldstate.IsKeyUp(Keys.I)) { input += 'I'; }
            if (keystate.IsKeyDown(Keys.J) && oldstate.IsKeyUp(Keys.J)) { input += 'J'; }
            if (keystate.IsKeyDown(Keys.K) && oldstate.IsKeyUp(Keys.K)) { input += 'K'; }
            if (keystate.IsKeyDown(Keys.L) && oldstate.IsKeyUp(Keys.L)) { input += 'L'; }
            if (keystate.IsKeyDown(Keys.M) && oldstate.IsKeyUp(Keys.M)) { input += 'M'; }
            if (keystate.IsKeyDown(Keys.N) && oldstate.IsKeyUp(Keys.N)) { input += 'N'; }
            if (keystate.IsKeyDown(Keys.O) && oldstate.IsKeyUp(Keys.O)) { input += 'O'; }
            if (keystate.IsKeyDown(Keys.P) && oldstate.IsKeyUp(Keys.P)) { input += 'P'; }
            if (keystate.IsKeyDown(Keys.Q) && oldstate.IsKeyUp(Keys.Q)) { input += 'Q'; }
            if (keystate.IsKeyDown(Keys.R) && oldstate.IsKeyUp(Keys.R)) { input += 'R'; }
            if (keystate.IsKeyDown(Keys.S) && oldstate.IsKeyUp(Keys.S)) { input += 'S'; }
            if (keystate.IsKeyDown(Keys.T) && oldstate.IsKeyUp(Keys.T)) { input += 'T'; }
            if (keystate.IsKeyDown(Keys.U) && oldstate.IsKeyUp(Keys.U)) { input += 'U'; }
            if (keystate.IsKeyDown(Keys.V) && oldstate.IsKeyUp(Keys.V)) { input += 'V'; }
            if (keystate.IsKeyDown(Keys.W) && oldstate.IsKeyUp(Keys.W)) { input += 'W'; }
            if (keystate.IsKeyDown(Keys.X) && oldstate.IsKeyUp(Keys.X)) { input += 'X'; }
            if (keystate.IsKeyDown(Keys.Y) && oldstate.IsKeyUp(Keys.Y)) { input += 'Y'; }
            if (keystate.IsKeyDown(Keys.Z) && oldstate.IsKeyUp(Keys.Z)) { input += 'Z'; }
            if (keystate.IsKeyDown(Keys.Decimal) && oldstate.IsKeyUp(Keys.Decimal)) { input += '.'; }
            if (keystate.IsKeyDown(Keys.OemPeriod) && oldstate.IsKeyUp(Keys.OemPeriod)) { input += '.'; }
            if (keystate.IsKeyDown(Keys.Space) && oldstate.IsKeyUp(Keys.Space)) { input += ' '; }
            if (keystate.IsKeyDown(Keys.Back) && oldstate.IsKeyUp(Keys.Back)) { if (input.Length > 0) input = input.Substring(0, input.Length - 1); }
            if (keystate.IsKeyDown(Keys.D1) && oldstate.IsKeyUp(Keys.D1)) { input += '1'; }
            if (keystate.IsKeyDown(Keys.D2) && oldstate.IsKeyUp(Keys.D2)) { input += '2'; }
            if (keystate.IsKeyDown(Keys.D3) && oldstate.IsKeyUp(Keys.D3)) { input += '3'; }
            if (keystate.IsKeyDown(Keys.D4) && oldstate.IsKeyUp(Keys.D4)) { input += '4'; }
            if (keystate.IsKeyDown(Keys.D5) && oldstate.IsKeyUp(Keys.D5)) { input += '5'; }
            if (keystate.IsKeyDown(Keys.D6) && oldstate.IsKeyUp(Keys.D6)) { input += '6'; }
            if (keystate.IsKeyDown(Keys.D7) && oldstate.IsKeyUp(Keys.D7)) { input += '7'; }
            if (keystate.IsKeyDown(Keys.D8) && oldstate.IsKeyUp(Keys.D8)) { input += '8'; }
            if (keystate.IsKeyDown(Keys.D9) && oldstate.IsKeyUp(Keys.D9)) { input += '9'; }
            if (keystate.IsKeyDown(Keys.D0) && oldstate.IsKeyUp(Keys.D0)) { input += '0'; }
            if (keystate.IsKeyDown(Keys.Enter) && oldstate.IsKeyUp(Keys.Enter)) { enter(input); input = ""; }

            oldstate = keystate;
            stringvector = new Vector2(area.Center.X, area.Center.Y) - (MenuManager.font.MeasureString(input) / 2);

        }

        public delegate void PressedEnter(String s);

        public override bool Contains(int x, int y)
        {
            return area.Contains(x, y);
        }

        public override void Draw()
        {
            MenuManager.batch.Draw(texture, area, Color.Green);
            MenuManager.batch.DrawString(MenuManager.font, input, stringvector, Color.Purple);
        }
    }
}
