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
    class MenuList : MenuComponent, ICollection<MenuComponent>
    {
        Rectangle area;
        SpriteBatch batch;
        SpriteFont font;
        Texture2D texture;
        List<MenuComponent> menucomponents;
        Color currentcolor = Color.Teal;
        int padding = 10;


        public MenuList(Rectangle r, SpriteBatch sb, SpriteFont sf)
        {
            area = r;
            batch = sb;
            font = sf;
            texture = new Texture2D(batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            menucomponents = new List<MenuComponent>();
        }

        public void AddNewButtonDefault(int height, int width, String t, EventHandler c)
        {
            menucomponents.Add(new MenuButton(new Rectangle(area.Left + padding, area.Top + (menucomponents.Count*(height+padding))+padding, area.Width - (2 * padding), height), batch, font, t, c));    
        }

        public override bool Contains(int x, int y)
        {
            return area.Contains(x, y);
        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState mscurrent, Microsoft.Xna.Framework.Input.MouseState msold)
        {
            foreach (MenuComponent mc in menucomponents)
            {
                mc.Update(mscurrent,msold);
            }
        }

        public override void Draw()
        {
            batch.Draw(texture, area, currentcolor);
            foreach (MenuComponent mc in menucomponents)
            {
                mc.Draw();
            }
        }




        //collections stuff below here

        public void Add(MenuComponent mc)
        {
            menucomponents.Add(mc);
        }

        public void Clear()
        {
            menucomponents.Clear();
        }

        public bool Contains(MenuComponent item)
        {
            return menucomponents.Contains(item);
        }

        public void CopyTo(MenuComponent[] array, int arrayIndex)
        {
            menucomponents.CopyTo(array,arrayIndex);
        }

        public int Count
        {
            get { return menucomponents.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(MenuComponent item)
        {
            return menucomponents.Remove(item);
        }

        public IEnumerator<MenuComponent> GetEnumerator()
        {
            return menucomponents.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return menucomponents.GetEnumerator();
        }
    }
}
