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
    public class GlobalChatClientGUI 
    {
        private spaceconquest.GlobalChatClient chatClient;
        private string ROOT_SERVER_URL
            = "http://www.openportone.appspot.com/";
        private int ref_ms;
        private Vector2 position;
        private Vector2 borderSize;
        private Vector2 chatBoxSize;
        private Boolean isOnline = false;
        private Color containerColor;
        private Color chatBoxColor;
        private List<ChatLog> chatLogs;
        private DateTime ref_Counter;
        private Vector2 box_Margin;
        private Boolean isUpdated = true;

        public Boolean IsUpdated
        {
            get { return isUpdated; }
            set { isUpdated = value; }
        }


        private Texture2D backgroundTexture;
        private List<Texture2D> textures;
        private SpriteFont chatFont;


        public GlobalChatClientGUI(Vector2 position, Vector2 containerSize, Vector2 chatBoxSize, int refreshTimeInMilliseconds, SpriteFont chatFont, Color containerColor, Color chatBoxColor, List<Texture2D> textureList)
        {
            this.chatFont = chatFont;
            textures = textureList;

            try
            {
                this.chatClient = new GlobalChatClient(this.ROOT_SERVER_URL);
                isOnline = true;
                ref_Counter = DateTime.Now;
                this.chatLogs = this.chatClient.GetLogs();
            }

            catch (System.Net.WebException e)
            {
                isOnline = false;
            }
            //Check borders and make sure chatBox fits inside container
            if (this.chatBoxSize.X > containerSize.X)
            {
                this.chatBoxSize.X = containerSize.X;
            }
            if (this.chatBoxSize.Y > containerSize.Y)
            {
                this.chatBoxSize.Y = containerSize.Y;
            }

            this.position = position;
            this.borderSize = containerSize;
            this.chatBoxSize = chatBoxSize;
            this.chatBoxColor = chatBoxColor;
            this.containerColor = containerColor;
            this.ref_ms = refreshTimeInMilliseconds;
            this.box_Margin = this.borderSize - this.chatBoxSize;
            this.messageBuffer = new StringBuilder();
        }

        public void UpdateChat()
        {

            this.chatLogs = this.chatClient.GetLogs();


        }


        private void Draw(SpriteBatch s, SpriteFont f, List<Texture2D> textureList)
        {

            //Draw container
            Rectangle screenRectangle = new Rectangle((int)position.X, (int)position.Y, (int)borderSize.X, (int)borderSize.Y);
            s.Draw(textureList[1], screenRectangle, containerColor);


            //Draw chatWindow
            Rectangle chatRectangle = new Rectangle((int)position.X + 30, (int)position.Y + 30, (int)chatBoxSize.X, (int)chatBoxSize.Y);
            s.Draw(textureList[0], chatRectangle, chatBoxColor);


            if (isOnline == true)
            {


                s.DrawString(f, "=====Global===Chat=======", new Vector2(position.X + 20, position.Y + 10), Color.Black);

                //Draw First 10 ChatMessages
                for (int i = 1; i <= chatLogs.Count && i < 10; i++)
                {

                    string message = this.MessageParser(chatLogs[chatLogs.Count - i].playerName, chatLogs[chatLogs.Count - i].message);

                    s.DrawString(f, message, new Vector2(position.X + 30, position.Y + 34 + 14 * i), Color.Black);

                }


                //draw buttons

                
                //draw textEntryBox
                Rectangle Rectangle = new Rectangle((int)position.X + 20, (int)(position.Y + chatBoxSize.Y + 50), (int)chatBoxSize.X, 100);
                if (this.hasFocus == true)
                {
                    s.Draw(textureList[0], Rectangle, chatBoxColor);
                }
                else
                {
                    s.Draw(textureList[2], Rectangle, chatBoxColor);
                }

                //Draw Message
                s.DrawString(f, this.messageBuffer.ToString(), new Vector2(Rectangle.X + 10, Rectangle.Y + 10), Color.Blue);

            }
            else
            {
                s.DrawString(f, "Chat Not connected ", new Vector2(box_Margin.X + 50, box_Margin.Y + 50), Color.Red);

                ///Draw error message

            }
        }

        private String MessageParser(string name, string message)
        {
            string s = String.Format("{0,6} wrote : {1,5} ", name, message);
            return s;
        }

        private StringBuilder messageBuffer;
        private TimeSpan messageCoolDown = new TimeSpan(0, 0, 4); //x second cooldown
        private DateTime lastMessageSent;
        public void ProcessKeyInput(KeyboardState k, String playerName)
        {
            if (lastMessageSent != null && (lastMessageSent.Subtract(DateTime.Now).Duration() < messageCoolDown))
            {
                lastMessageSent = DateTime.Now;
                
                messageBuffer.Append("a-" + k.GetPressedKeys()[0]);
            }

        }


        #region GUIObject Members

        public int GetWidth()
        {
            return (int)this.borderSize.X;
        }

        public void SetWidth(int newWidth)
        {
            if (newWidth > 170)
            {
                this.borderSize.X = newWidth;
            }
        }

        public int GetHeight()
        {
            return (int)this.borderSize.Y;
        }

        public void SetHeight(int newHeight)
        {
            if (newHeight > 500)
            {
                this.borderSize.Y = newHeight;
            }
        }

        public void SetLocation(Vector2 newLocation)
        {
            this.position = newLocation;
        }

        public Vector2 GetLocation()
        {
            return new Vector2(this.position.X, this.position.Y);
        }

        public bool MouseIsOverlapping(MouseState m)
        {
            Rectangle thisGUIObject = new Rectangle((int)this.position.X, (int) this.position.Y, this.GetWidth(), this.GetHeight());
            if (thisGUIObject.Contains(new Point(m.X, m.Y))) return true;
            else return false;
        }

        private Boolean hasFocus;

        public void setFocus(Boolean b)
        {
            this.hasFocus = b;
        }
        public bool HasFocus()
        {
            return hasFocus;
        }


        public void Draw(SpriteBatch s)
        {

            this.Draw(s, this.chatFont, this.textures);

        }
        #endregion
    }  //end class
}//end namespace
