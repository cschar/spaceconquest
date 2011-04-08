using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.IO;


namespace spaceconquest
{
    public class SoundEffectBox
    {
        private float volume;

        public float Volume
        {
            get { return volume; }
            set {
                if (value > 1.0f)
                {
                    volume = 1.0f;
                    return;
                }
                else if (value < 0.0f)
                {
                    volume = 0.0f;
                }
                volume = value;
            }
        }
        private Random rng;
        private ContentManager soundEffectManager;
        private Dictionary<string, List<SoundEffect>> soundHash; 
        /// <summary>
        /// Loads static Default sounds
        /// 
        /// Options include : "SelectIcon", "Teleport" , "Hosting", 
        /// "Joining", "MiningShip", "Fighter", "Transport" ,
        /// "StarCruiser", "ColonyShip"
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="directory"></param>
        /// 


        public SoundEffectBox(ContentManager manager, string directory)
        {
            Console.WriteLine(manager.RootDirectory);
            Console.WriteLine(directory);
            String dir = manager.RootDirectory+"\\"+directory;
            ConfigParser cp = new ConfigParser(dir + "SFXConfig.txt");
            Dictionary<String, List<String>> opts = cp.ParseConfig();
            String DefaultDir = opts["DEFAULT"][0];
            String DefDir = opts["DEFDIR"][0];

            //Page_Load(directory);
            volume = 1.0f; 
            soundEffectManager = manager;
            soundHash = new Dictionary<string, List<SoundEffect>>();
            rng = new Random();
            //SelectIcon List

            String[] SoundTypes = {"BuildQueueFull", "ColonyShip", "Fighter", "Hosting", 
                                    "Joining", "Mining", "SelectIcon", "StarCruiser", 
                                    "Teleport", "Toggle", "Transport"};
            String [] Attempts = {DefDir, DefaultDir};
            foreach (String attempt in Attempts) {
                try
                {
                    String newDir = directory + attempt + "/";
                    foreach (String st in SoundTypes)
                    {
                        List<SoundEffect> adder = new List<SoundEffect>();
                        DirectoryInfo di = new DirectoryInfo(manager.RootDirectory+"/"+newDir + st + "/");
                        FileInfo[] fis = di.GetFiles("*");
                        foreach (FileInfo fi in fis)
                        {
                            String loc = newDir + st + "/" + (fi.Name.Split('.'))[0];
                            //Console.WriteLine(loc);
                            adder.Add(soundEffectManager.Load<SoundEffect>(loc));
                        }
                        soundHash.Add(st, adder); 

                    }
                    break;
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);

                }
            }
            
           
            
            
        
        }


        /// <summary>
        /// Options include : "SelectIcon", "Teleport" , "Hosting", 
        /// "Joining", "MiningShip", "Fighter", "Transport" ,
        /// "StarCruiser", "ColonyShip"
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="directory"></param>
        public void PlaySound(string name)
        {
            try
            {
                if (!soundHash.ContainsKey(name)) return;
                else   //generics
                {
                    List<SoundEffect> seList = soundHash[name];
                    if (seList.Count == 0) { return; }
                    int randomIndex = rng.Next(seList.Count);
                    Console.WriteLine(" randomIndex is " + randomIndex + "  the list size is " + seList.Count);
                    seList[randomIndex].Play(volume, 0.0f, 0.0f);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error playing sound");
            }

        }

       

    }
}
