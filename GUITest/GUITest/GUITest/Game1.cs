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
using System.Threading;

namespace GUITest
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 chatGUIPos = new Vector2(30, 30);
        Vector2 chatGUIContainer = new Vector2(400, 400);
        Vector2 chatGUITextBox = new Vector2(300, 200);
        Color chatGUIbackgroundColor = Color.FromNonPremultiplied(new Vector4(70, 70, 70, 70));
        Color chatGUITextBoxColor = Color.FromNonPremultiplied(new Vector4(70, 70, 70, 70));
        Texture2D treeTexture;
        Texture2D whiteTexture;
        List<Texture2D> textureList;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            textureList = new List<Texture2D>();
        }

        spaceconquest.GlobalChatClientGUI chatGUI;
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

      
           cur_seconds = DateTime.Now.Second;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            this.font = Content.Load<SpriteFont>("spriteFont1");
            // TODO: use this.Content to load your game content here

            this.treeTexture = Content.Load<Texture2D>("wood");
            this.whiteTexture = Content.Load<Texture2D>("white");
            textureList.Add(whiteTexture);
            textureList.Add(Content.Load<Texture2D>("gridBlack"));
            textureList.Add(Content.Load<Texture2D>("grey"));
            chatGUI = new spaceconquest.GlobalChatClientGUI(chatGUIPos,
                chatGUIContainer,
                chatGUITextBox,
                5000,
                this.font,
                Color.Blue,
                chatGUITextBoxColor,
                textureList);





        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //Add keyboard check for chatBox

            //Check if focus on chatBox
            
            if (Keyboard.GetState().GetPressedKeys().Length > 0)
            {
                this.chatGUI.ProcessKeyInput(Keyboard.GetState(), "geila");

            } 



            
            base.Update(gameTime);
        }



        private SpriteFont font;
        int cur_seconds;
        
        /// <summary>
        /// 
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            
            //update every 5 seconds
            if (DateTime.Now.Second % 5 == 0 && chatGUI.IsUpdated == false)
            {
                chatGUI.IsUpdated = true;
                chatGUI.UpdateChat();
            }
            if (DateTime.Now.Second % 3 == 0)
            {
                chatGUI.IsUpdated = false;
            }

            ((GUIModule.GUIObject)chatGUI).Draw(this.spriteBatch);

            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
