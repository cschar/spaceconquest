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
using System.IO;

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
        Dictionary<String, List<String>> opts;
        ConfigParser cp;
        public static List<String> Races = new List<String>();

        public static int x;
        public static int y;

        public static Game1 thisgame;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            thisgame = this;
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
            bool full = true;

            if (full)
            {
                graphics.PreferredBackBufferWidth = graphics.GraphicsDevice.DisplayMode.Width;
                graphics.PreferredBackBufferHeight = graphics.GraphicsDevice.DisplayMode.Height;
            }
            else
            {
                graphics.PreferredBackBufferWidth = 800;
                graphics.PreferredBackBufferHeight = 600;
            }
            x = graphics.PreferredBackBufferWidth;
            y = graphics.PreferredBackBufferHeight;

            graphics.IsFullScreen = full;
            graphics.ApplyChanges();
            Window.Title = ":: Space Conquest ::";
            this.IsMouseVisible = true;


            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 

        int sorter(String[] a, String[] b) {
            return int.Parse(a[1])-int.Parse(b[1]);
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = graphics.GraphicsDevice;

            //Building a hex
            HexModel.InitializePrimitive(device);
            ProtonBeamModel.InitializePrimitive(Content.Load<Model>("protonbeam"));
            //Loading the sphere
            SphereModel.InitializePrimitive(Content.Load<Model>("sphere"));
            //Loadng the beam

            //Loading the sphere
            StarField.LoadStarModel(Content.Load<Model>("thing"));
            
            //Loading the Planet 
            PlanetModel.InitializePrimitive(Content.Load<Model>("planet_unpopulated"),
                    Content.Load<Model>("planet_populated"));

            //Loading the Asteroid
            AsteroidModel.InitializePrimitive(Content.Load<Model>("Asteroid"), Content.Load<Model>("DefaultShips/miningRobot"));

            //Loading a StarCruiser model
            //StarCruiserModel.InitializePrimitive(Content.Load<Model>("starcruiser"));

            cp = new ConfigParser("Content/Models/ModelConfig.txt");
            opts = cp.ParseConfig();

            List<String[]> raceTuples = new List<String[]>();
            List<String> racePreProc = opts["ASSIGN"];
            foreach (String s in racePreProc) {
                try {
                    String[] splits = s.Split('|');
                int index = int.Parse(splits[1]);
                String tup = splits[0];
                raceTuples.Add(splits);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            raceTuples.Sort(sorter);

            foreach (String[] s in raceTuples) {
                Races.Add(s[0]);
            }

            List<String> defs = opts["DEFINE"];
            Dictionary<String, String> raceToModel = new Dictionary<string, string>();
            foreach (String def in defs) {
                String[] split = def.Split('|');
                try
                {
                    raceToModel.Add(split[0], split[1]);
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }    
            }

            String[] sts = {"ColonyShip", "Fighter", "MiningRobot", "MiningShip", "StarCruiser",
                               "Telescope", "Transport"};

            foreach (String race in Races) {
                String[] attempts = { raceToModel[race], opts["DEFAULT"][0] };

                foreach (string att in attempts) {
                    try
                    {
                        String raceDir = "Content/Models/" + att + "/";
                        foreach (String st in sts)
                        {
                            Boolean fail = true;
                            DirectoryInfo di = new DirectoryInfo(raceDir + st);
                            //Console.WriteLine(di.FullName);
                            foreach (FileInfo fi in di.GetFiles())
                            {
                                try
                                {
                                    //Console.WriteLine(Content.RootDirectory);
                                    String loc = "Models/" + att + "/" + st + "/" + (fi.Name.Split('.'))[0];
                                    //Console.WriteLine(race + "." + st);
                                    ShipModel.AddShipModel(Content.Load<Model>(loc), race + "." + st);
                                    fail = false;
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }
                            if (fail) { throw new Exception(); }

                        }
                        break;

                    }
                    catch (Exception e) {
                        Console.WriteLine(e.Message);
                    }
                    
                
                }

                

            }

            Console.WriteLine("\n\n\n\n\n");
            foreach (String s in ShipModel.shipmodels.Keys) {
                Console.WriteLine(s);
            }

            /*ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/starCruiser"), "starcruiser");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/miningShip"), "miningship");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/miningRobot"), "miningrobot");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/transport"), "transport");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/colonyShip"), "colonyship");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/fighter"), "fightership");
            ShipModel.AddShipModel(Content.Load<Model>("DefaultShips/SpaceTelescope"), "SpaceTelescope");

            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2starCruiser"), "r2starcruiser");
            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2miningShip"), "r2miningship");
            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2miningRobot"), "r2miningrobot");
            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2transport"), "r2transport");
            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2colonyShip"), "r2colonyship");
            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2fighter"), "r2fightership");
            ShipModel.AddShipModel(Content.Load<Model>("ArdusShips/r2SpaceTelescope"), "r2SpaceTelescope");*/

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
            List<string> tmpTracks = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                tmpTracks.Add("DefaultMusic/track" + i);

            }
            jukeBox = new Music_stuff.JukeBox(tmpTracks, contentManager);
            //jukeBox.play();    //Play the tunes

            soundEffectBox = new SoundEffectBox(contentManager, "SFX/");
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
