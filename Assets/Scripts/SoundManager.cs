using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts
{
    class SoundManager
    {
        public static SoundManager sm = null;
        public Pool<AudioSourcePerso> lesAudioSources;
        public Dictionary<String, AudioData> soundsList;
        private static AudioMixer mixer;

        private SoundManager()
        {
            GameObject go = new GameObject("AudioSourcePerso");
            go.AddComponent<AudioSource>();
            lesAudioSources = new Pool<AudioSourcePerso>(new AudioSourcePerso(go));
            soundsList = new Dictionary<string, AudioData>();
        }

        public static void initSoundManager(AudioMixer am)
        {
            mixer = am;
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

        public AudioSourcePerso getSound(String name, Vector3 pos, float volume, string group, bool loop)
        {
            AudioData ad;
            AudioSourcePerso asp;
            if(soundsList.TryGetValue(name, out ad))
            {
                asp = lesAudioSources.getFirst();
                AudioSource temp = asp.gameO.GetComponent<AudioSource>();
                if(temp != null)
                {
                    temp.clip = ad.getSound();
                    temp.volume = volume;
                    temp.loop = loop;
                    //Debug.Log(mixer.name);
                    temp.outputAudioMixerGroup = mixer.FindMatchingGroups(group)[0]; 
                    
                }

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
