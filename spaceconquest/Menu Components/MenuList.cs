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
        protected Rectangle area;
        SpriteBatch batch;
        SpriteFont font;
        protected Texture2D texture;
        protected List<MenuComponent> menucomponents;
        public Color currentcolor = Color.Teal;
        public int padding = 10;
        public bool visible = true;
        public bool showbackround = true;


        public MenuList(Rectangle r)
        {
            area = r;
            batch = MenuManager.batch;
            font = MenuManager.font;
            texture = new Texture2D(batch.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            texture.SetData(new[] { Color.White });
            menucomponents = new List<MenuComponent>();
        }

        public void AddNewButtonDefault(int height, int width, String t, EventHandler c)
        {
            menucomponents.Add(new MenuButton(new Rectangle(area.Left + padding, area.Top + (menucomponents.Count*(height+padding))+padding, area.Width - (2 * padding), height), t, c));    
        }

        public void AddNewTextLineDefault(int height, int width, String t)
        {
            menucomponents.Add(new TextLine(new Rectangle(area.Left + padding, area.Top + (menucomponents.Count * (height + padding)) + padding, area.Width - (2 * padding), height), t));
        }

        public override bool Contains(int x, int y)
        {
            if (visible) { return area.Contains(x, y); }
            else { return false; }
        }

        public override void Update(Microsoft.Xna.Framework.Input.MouseState mscurrent, Microsoft.Xna.Framework.Input.MouseState msold)
        {
            if (visible)
            {
                foreach (MenuComponent mc in menucomponents)
                {
                    mc.Update(mscurrent, msold);
                }
            }
        }

        public override void Draw()
        {
            if (visible)
            {
                if (showbackround) batch.Draw(texture, area, currentcolor);
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
