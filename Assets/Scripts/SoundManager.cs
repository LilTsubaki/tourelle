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
            Debug.Log("Constructeur soundManager");
            GameObject go = new GameObject("AudioSourcePerso");
            go.AddComponent<AudioSource>();
            lesAudioSources = new Pool<AudioSourcePerso>(new AudioSourcePerso(go));
            soundsList = new Dictionary<string, AudioData>();
        }

        public static SoundManager getInstance()
        {
            if (sm == null)
                sm = new SoundManager();
         
            return sm;
        }

        public void addAudioData(String name, AudioData ad)
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
                asp.gameO.GetComponent<AudioSource>().clip = ad.getSound();
                asp.gameO.transform.position = pos;
                return asp;
            }
            else
            {
                return null;
            }
        }
    }
}
