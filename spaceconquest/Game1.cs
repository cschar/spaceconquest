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
        public static SpriteFont textFont;
        public static Texture2D hextexture;
        RasterizerState wireFrameState;
        public static ContentManager contentManager;
        public static spaceconquest.Music_stuff.JukeBox jukeBox;
        public static SoundEffectBox soundEffectBox;
        

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

            //Building a hex
            HexModel.InitializePrimitive(device);

            //Loading the sphere
            SphereModel.InitializePrimitive(Content.Load<Model>("sphere"));
            //Loading the sphere
            StarField.LoadStarModel(Content.Load<Model>("thing"));
            
            //Loading the Planet 
            PlanetModel.InitializePrimitive(Content.Load<Model>("planet_unpopulated"),
                    Content.Load<Model>("planet_populated"));

            //Loading the Asteroid
            AsteroidModel.InitializePrimitive(Content.Load<Model>("Asteroid"), Content.Load<Model>("DefaultShips/miningRobot"));

            //Loading a StarCruiser model
            //StarCruiserModel.InitializePrimitive(Content.Load<Model>("starcruiser"));
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/starCruiser"), "starcruiser");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/miningShip"), "miningship");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/miningRobot"), "miningrobot");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/transport"), "transport");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/colonyShip"), "colonyship");

            // TODO: use this.Content to load your game content here
            mainFont = Content.Load<SpriteFont>("TitleFont");
            textFont = Content.Load<SpriteFont>("TextFont");
            //Texture2D texture = Content.Load<Texture2D>("MoveButton.png");

            MenuManager.Init(spriteBatch, mainFont);

            wireFrameState = new RasterizerState()
            {
                FillMode = FillMode.WireFrame,
                CullMode = CullMode.None,
            };


            /////// USER  C ontent/////////////////////
            //Sounds
            contentManager = this.Content;
            List<string> tmpTracks = new List<string>() { "track1" ,
                    "track2", "track4"};
            jukeBox = new Music_stuff.JukeBox(tmpTracks, contentManager);
            //jukeBox.play();    //Play the tunes

            soundEffectBox = new SoundEffectBox(contentManager, "DefaultSoundEffects/");
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

            //GraphicsDevice.RasterizerState = wireFrameState;//
            // Reset the fill mode renderstate.
            GraphicsDevice.RasterizerState = RasterizerState.CullNone;
            //GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            spriteBatch.Begin();
            IconButton.batch.Begin(SpriteSortMode.Texture, BlendState.NonPremultiplied); //for nonpremultiplied stuff, like the command buttons


            //titleScreen uses spritebatch object
            MenuManager.screen.Draw();

            spriteBatch.End();
            IconButton.batch.End();
            

            



            


            base.Draw(gameTime);
        }
    }
}
