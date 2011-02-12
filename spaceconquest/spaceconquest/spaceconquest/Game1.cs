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
    /// This is the main type for your game
    /// ted's back
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public static GraphicsDevice device;
        private SpriteFont mainFont;
        RasterizerState wireFrameState;
        Hex3D h, h2;
        

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
            Window.Title = ":: Space Conquest ::";
            this.IsMouseVisible = true;



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
            device = graphics.GraphicsDevice;


            // TODO: use this.Content to load your game content here
            mainFont = Content.Load<SpriteFont>("TitleFont");

            MenuManager.Init(spriteBatch, mainFont);

            wireFrameState = new RasterizerState()
            {
                FillMode = FillMode.WireFrame,
                CullMode = CullMode.None,
            };


            h = new Hex3D(0, 0, null);
            h2 = new Hex3D(2, 0, null);
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
            
            
            MenuManager.screen.Update();
            
            
            base.Update(gameTime);
        }
        
        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            device.Clear(Color.Black);
            // TODO: Add your drawing code here
            spriteBatch.Begin();

            //titleScreen uses spritebatch object
            //MenuManager.screen.Draw();
            
            spriteBatch.End();


            GraphicsDevice.RasterizerState = wireFrameState;

            float time = (float)gameTime.TotalGameTime.TotalSeconds;

            float yaw = 0;// time * 0.4f;
            float pitch = 0;// time * 0.7f;
            float roll = 0;// time * 1.1f;

            Vector3 cameraPosition = new Vector3(0, 0, 10f);

            float aspect = GraphicsDevice.Viewport.AspectRatio;

            Matrix world = Matrix.Identity;
            //Matrix world = Matrix.CreateFromYawPitchRoll(yaw, pitch, roll);
            Matrix view = Matrix.CreateLookAt(cameraPosition, Vector3.Zero, Vector3.Up);
            Matrix projection = Matrix.CreatePerspectiveFieldOfView(1, aspect, 1, 20);
            Matrix orthog = Matrix.CreateOrthographic(800, 600, 1, 20);

            Color color = Color.Green;
            h.Draw(world, view, orthog, color);
            h2.Draw(world, view, orthog, color);



            // Reset the fill mode renderstate.
            //GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;


            base.Draw(gameTime);
        }
    }
}
