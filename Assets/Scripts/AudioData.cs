using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    class AudioData
    {
        private List<AudioClip> sounds = new List<AudioClip>();
        private int cpt = 0;

        public AudioClip getSound()
        {
            AudioClip ac = null;
            if (cpt < sounds.Count)
            {
                ac = sounds[cpt];
                cpt++;
            }
            return ac;
        }

        public void addSound(AudioClip ac)
        {
            sounds.Add(ac);
        }
    }
}
