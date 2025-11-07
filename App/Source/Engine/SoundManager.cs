using SFML.Audio;
using System;
using System.Collections.Generic;

namespace GameJam
{
    public class SoundManager
    {
        private List<Sound> sounds = new List<Sound>();

        public void Update(float deltaSeconds)
        {
            var soundsToRemove = sounds.FindAll(x => x.Status == SoundStatus.Stopped);
            soundsToRemove.ForEach(x => x.Dispose());
            sounds.RemoveAll(soundsToRemove.Contains);
        }

        public Sound PlaySound(string soundName, float volume = 100.0f, bool loop = false)
        {
            //SoundBuffer buffer = new SoundBuffer("Data/Sounds/" + soundName + ".wav");
            SoundBuffer buffer = new SoundBuffer(Assets.Open("Lightbound.Data.Sounds." + soundName + ".wav"));
            Sound sound = new Sound(buffer);
            sound.Volume = volume;
            sound.Loop = loop;
            sound.Play();
            sounds.Add(sound);
            return sound;
        }
        public void RemoveSound(Sound sound)
        {
            if (sound != null)
            {
                sound.Stop();
                sound.Dispose();
                sounds.Remove(sound);
            }
        }
    }
}