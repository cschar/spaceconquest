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
        public SoundEffectBox(ContentManager manager, string directory)
        {
            volume = 1.0f;
            soundEffectManager = manager;
            soundHash = new Dictionary<string, List<SoundEffect>>();
            rng = new Random();
            //SelectIcon List
            List<SoundEffect> selectIconList = new List<SoundEffect>();
            selectIconList.Add(soundEffectManager.Load<SoundEffect>(directory + "SelectIcon1"));
            selectIconList.Add(soundEffectManager.Load<SoundEffect>(directory + "SelectIcon2"));
            soundHash.Add("SelectIcon", selectIconList);

            //Teleport list
            List<SoundEffect> TeleportList = new List<SoundEffect>();
            TeleportList.Add(soundEffectManager.Load<SoundEffect>(directory + "Teleport1"));
            TeleportList.Add(soundEffectManager.Load<SoundEffect>(directory + "Teleport2"));
            soundHash.Add("Teleport", TeleportList);

            List<SoundEffect> HostingGameList = new List<SoundEffect>();
            HostingGameList.Add(soundEffectManager.Load<SoundEffect>(directory + "Hosting1"));
            HostingGameList.Add(soundEffectManager.Load<SoundEffect>(directory + "Hosting2"));
            soundHash.Add("Hosting", HostingGameList);

            List<SoundEffect> JoiningGameList = new List<SoundEffect>();
            JoiningGameList.Add(soundEffectManager.Load<SoundEffect>(directory + "Joining1"));
            soundHash.Add("Joining", JoiningGameList);

            List<SoundEffect> ToggleList = new List<SoundEffect>();
            ToggleList.Add(soundEffectManager.Load<SoundEffect>(directory + "Toggle1"));
            soundHash.Add("Toggle", ToggleList);

            List<SoundEffect> miningShipList = new List<SoundEffect>();
            miningShipList.Add(soundEffectManager.Load<SoundEffect>(directory + "Mining2"));
            miningShipList.Add(soundEffectManager.Load<SoundEffect>(directory + "Mining3"));
            miningShipList.Add(soundEffectManager.Load<SoundEffect>(directory + "Mining4"));
            soundHash.Add("MiningShip", miningShipList);

            List<SoundEffect> fighterShip = new List<SoundEffect>();
            miningShipList.Add(soundEffectManager.Load<SoundEffect>(directory + "Fighter1"));
            miningShipList.Add(soundEffectManager.Load<SoundEffect>(directory + "Fighter2"));
            miningShipList.Add(soundEffectManager.Load<SoundEffect>(directory + "Fighter3"));
            soundHash.Add("Fighter", fighterShip);

            List<SoundEffect> transportShip = new List<SoundEffect>();
            transportShip.Add(soundEffectManager.Load<SoundEffect>(directory + "Transport1"));
            transportShip.Add(soundEffectManager.Load<SoundEffect>(directory + "Transport2"));
            transportShip.Add(soundEffectManager.Load<SoundEffect>(directory + "Transport3"));
            transportShip.Add(soundEffectManager.Load<SoundEffect>(directory + "Transport4"));
            soundHash.Add("Transport", transportShip);

            List<SoundEffect> starCruiser = new List<SoundEffect>();
            starCruiser.Add(soundEffectManager.Load<SoundEffect>(directory + "StarCruiser1"));
            starCruiser.Add(soundEffectManager.Load<SoundEffect>(directory + "StarCruiser2"));
            starCruiser.Add(soundEffectManager.Load<SoundEffect>(directory + "StarCruiser3"));
            starCruiser.Add(soundEffectManager.Load<SoundEffect>(directory + "StarCruiser4"));
            soundHash.Add("StarCruiser", starCruiser);

            List<SoundEffect> colonyShip = new List<SoundEffect>();
            colonyShip.Add(soundEffectManager.Load<SoundEffect>(directory + "ColonyShip1"));
            colonyShip.Add(soundEffectManager.Load<SoundEffect>(directory + "ColonyShip1"));
            colonyShip.Add(soundEffectManager.Load<SoundEffect>(directory + "ColonyShip1"));
            colonyShip.Add(soundEffectManager.Load<SoundEffect>(directory + "ColonyShip1"));
            colonyShip.Add(soundEffectManager.Load<SoundEffect>(directory + "ColonyShip1"));
            soundHash.Add("ColonyShip", colonyShip);
        
        
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
                    int randomIndex = rng.Next(seList.Count);
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
