using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class SoundManager
    {
        public static SoundManager sm = null;
        public Pool<AudioSourcePerso> lesAudioSources;
        public Dictionary<String, AudioData> soundsList;

        private SoundManager()
        {
            GameObject go = new GameObject("AudioSourcePerso");
            go.AddComponent<AudioSource>();
            lesAudioSources = new Pool<AudioSourcePerso>(new AudioSourcePerso(go));
            soundsList = new Dictionary<string, AudioData>();
        }

        public SoundManager getInstance()
        {
            if (sm == null)
                sm = new SoundManager();
         
            return sm;
        }

        public void addSound(String name, AudioData ad)
        {
            soundsList[name] = ad;
        }

        public AudioSourcePerso getSound(String name, Vector3 pos)
        {
            AudioData ad;
            AudioSourcePerso asp;
            if(soundsList.TryGetValue(name, out ad))
            {
                asp = lesAudioSources.getFirst();
                asp.audioSource.clip = ad.getSound();
                return asp;
            }
            else
            {
                return null;
            }
        }
    }
}
