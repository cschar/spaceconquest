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
        }

        public void PlaySound(string name)
        {
            if (!soundHash.ContainsKey(name)) return;
            else   //generics
            {
                List<SoundEffect> seList = soundHash[name];
                int randomIndex = rng.Next(seList.Count);
                seList[randomIndex].Play(volume, 0.0f, 0.0f);

            }

        }

       

    }
}
