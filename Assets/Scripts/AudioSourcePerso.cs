using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class AudioSourcePerso : ObjetPoolable<AudioSourcePerso>
    {
        public GameObject gameO;
        public static AudioSource audioSource = null;
        //public AudioSourcePersoManager aspm;

        public AudioSourcePerso()
        {

        }

        public AudioSourcePerso(GameObject go)
        {
            if (audioSource == null)
                audioSource = go.GetComponent<AudioSource>();
            gameO = go;
            gameO.AddComponent<AudioSourcePersoManager>();


            if (GameObject.Find("soundPool") == null)
                new GameObject("soundPool");

            gameO.transform.parent = GameObject.Find("soundPool").transform;
        }

        public bool isAvailable()
        {
            return !gameO.activeSelf;
        }
        public void Copy(AudioSourcePerso asp)
        {
            gameO = GameObject.Instantiate<GameObject>(asp.gameO);
            gameO.transform.parent = GameObject.Find("soundPool").transform;
        }

        public void putUnavailable()
        {
            gameO.SetActive(true);

            AudioSource aas = gameO.GetComponent<AudioSource>();

            aas.bypassEffects = audioSource.bypassEffects;
            aas.bypassListenerEffects = audioSource.bypassListenerEffects;
            aas.bypassReverbZones = audioSource.bypassReverbZones;
            aas.clip = audioSource.clip;
            aas.dopplerLevel = audioSource.dopplerLevel;
            aas.enabled = audioSource.enabled;
            aas.hideFlags = audioSource.hideFlags;
            aas.ignoreListenerPause = audioSource.ignoreListenerPause;
            aas.ignoreListenerVolume = audioSource.ignoreListenerVolume;
            aas.loop = audioSource.loop;
            aas.maxDistance = audioSource.maxDistance;
            aas.minDistance = audioSource.minDistance;
            aas.mute = audioSource.mute;
            aas.name = audioSource.name;
            aas.outputAudioMixerGroup = audioSource.outputAudioMixerGroup;
            aas.panStereo = audioSource.panStereo;
            aas.pitch = audioSource.pitch;
            aas.playOnAwake = audioSource.playOnAwake;
            aas.priority = audioSource.priority;
            aas.reverbZoneMix = audioSource.reverbZoneMix;
            aas.rolloffMode = audioSource.rolloffMode;
            aas.spatialBlend = audioSource.spatialBlend;
            aas.spatialize = audioSource.spatialize;
            aas.spread = audioSource.spread;
            aas.tag = audioSource.tag;
            aas.time = audioSource.time;
            aas.timeSamples = audioSource.timeSamples;
            aas.velocityUpdateMode = audioSource.velocityUpdateMode;
            aas.volume = audioSource.volume;
        }
    }
}
