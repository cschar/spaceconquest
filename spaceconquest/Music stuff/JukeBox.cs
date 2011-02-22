using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace spaceconquest.Music_stuff
{
    /*
     * JUkebox class that acts as a container / iterator 
     * for the game's sound track.
     * 
     * When a song finishes, it will LOOP again 
     * the jukebox must have a call to nextTrack()
     * for the next song to play.
     * 
     * Author : Cody Scharfe
     * */
    public class JukeBox : System.Collections.ObjectModel.Collection<Song>
    {
        private List<Song> songList;
        private int songIndex = 0;
        private bool isPlaying = false;


              /// <summary>
        /// Constructor,
        /// 
        /// tracknames do not need to have file extension on end
        ///  i.e. if tracks are 
        ///  track1.mp3, track2.mp3,
        ///  have those files in the contentManagers 'sound' directory
        ///   then pass the appropriate string list along to load them
        ///   i.e.  < "track1", "track2">
        ///   
        /// </summary>
        /// <param name="trackNames"></param>
        /// <param name="trackSource"></param>
        public JukeBox(List<string> trackNames, ContentManager trackSource)
        {
            songList = new List<Song>();
            foreach (string songName in trackNames)
             {
             //   songList.Add(trackSource.Load<Song>(songName));
                 this.Add(trackSource.Load<Song>(songName));
             }

            //add a listener for when each track ends
            MediaPlayer.MediaStateChanged += new System.EventHandler<System.EventArgs>(MediaPlayer_MediaStateChanged);
          
        }

        //this listener keeps the music going!!!
        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            //Check if the end of the track has been reached
            if (MediaPlayer.State == MediaState.Stopped &&
                isPlaying == true)
            {
                this.goToNextTrack();
                this.play();
            }
        }


        public void play()
        {
            if (MediaPlayer.State == MediaState.Playing)
            {
                // check if trackIndex's are synced up
                if (((Song)this.Items[songIndex]).Equals(MediaPlayer.Queue.ActiveSong) == true)
                {
                    //don't need to do anything, songs are synched
                }
                else
                {
                    MediaPlayer.Play(this.Items[songIndex]);
                }
            }
            else if (MediaPlayer.State == MediaState.Paused)
            {
                MediaPlayer.Resume();
            }
            else if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Play(this.Items[songIndex]);
                
            }
            this.isPlaying = true;
        }

        public void pause()
        {
            MediaPlayer.Pause();
            this.isPlaying = false;
        }

        /// <summary>
        /// Moves Jukebox iterator to next track
        /// Does not start playing track.
        /// </summary>
        public void goToNextTrack()
        {
            songIndex++;
            if (songIndex >= this.Items.Count) songIndex = 0;
            
        }

       
    

        public float Volume
        {
            get { return MediaPlayer.Volume; }
            set {

                if (value < 0.0f) value = 0f;
                if (value >= 1.0f) value = 1.0f;

                MediaPlayer.Volume = value;

                 }
        }

    }

}
