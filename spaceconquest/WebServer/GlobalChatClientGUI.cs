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
        private string SERVER_URL = "http://www.openportone.appspot.com/";
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


        public GlobalChatClientGUI(Vector2 position, Vector2 containerSize, Vector2 chatBoxSize, int refreshTimeInMilliseconds, Color containerColor, Color chatBoxColor)
        {
            try
            {
                this.chatClient = new GlobalChatClient("http://www.openportone.appspot.com/");
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
            if(this.chatBoxSize.Y > containerSize.Y)
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
        
        }


        

        public void Draw(SpriteBatch s, SpriteFont f)
        {

            //Draw container
            Rectangle screenRectangle = new Rectangle((int)position.X, (int)position.Y, (int)borderSize.X, (int)borderSize.Y);
            s.Draw(null, screenRectangle, containerColor);


            //Draw chatWindow
            Rectangle chatRectangle = new Rectangle((int)position.X, (int)position.Y, (int)chatBoxSize.X, (int)chatBoxSize.Y);
            s.Draw(null, chatRectangle, chatBoxColor);


            if (isOnline == true)
            {
                
                //incrementTime
                ref_Counter += (DateTime.Now - ref_Counter);
                //Check for chatLogRefresh
                if (ref_Counter.Millisecond > ref_ms)
                {
                    this.chatLogs = this.chatClient.GetLogs();
                }


                //Draw First 10 ChatMessages
                for(int i = 1; i <= chatLogs.Count && i < 10; i++){

                    string message = chatLogs[i].playerName + " : " + chatLogs[i].message; 
                    s.DrawString(f, message , new Vector2(box_Margin.X + 5, box_Margin.Y + 14*i) , Color.Black);
            
                }


                //draw buttons
                Rectangle Rectangle = new Rectangle((int)position.X, (int)position.Y, (int)chatBoxSize.X, (int)chatBoxSize.Y);
                s.Draw(null, chatRectangle, chatBoxColor);

                //draw textEntryBox

            }
            else
            {
                
                ///Draw error message

            }
        }


    }
}
