using System;
using System.Media;

namespace CyberAwareSA
{
    /// <summary>
    /// Handles audio playback for the chatbot including voice greetings.
    /// </summary>
    public class AudioService
    {
        /// <summary>
        /// Plays the voice greeting WAV file on startup.
        /// </summary>
        public void PlayGreeting()
        {
            try
            {
                string audioPath = "Greeting.wav";

                if (System.IO.File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.PlaySync();
                    }
                }
            }
            catch (Exception)
            {
                // Silent fail - program continues without audio
            }
        }
    }
}