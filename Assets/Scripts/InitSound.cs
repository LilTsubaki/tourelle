using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

namespace Assets.Scripts
{

    public class InitSound : MonoBehaviour
    {
        private bool muted = false;

        private AudioData ad1;
        public AudioClip ac1;
        public AudioClip ac2;
        public AudioClip ac3;

        private AudioData ad2;
        public AudioClip ac4;

        private AudioData ad3;
        public AudioClip ac5;

        private AudioData ad4;
        public AudioClip ac6;


        public AudioMixer am;

        // Use this for initialization
        void Awake()
        {
            SoundManager.initSoundManager(am);

            ad1 = new AudioData();
            ad1.addSound(ac1);
            ad1.addSound(ac2);
            ad1.addSound(ac3);
            SoundManager.getInstance().addAudioData("pewpew", ad1);

            ad2 = new AudioData();
            ad2.addSound(ac4);
            SoundManager.getInstance().addAudioData("boom", ad2);

            ad3 = new AudioData();
            ad3.addSound(ac5);
            SoundManager.getInstance().addAudioData("explo", ad3);

            ad4 = new AudioData();
            ad4.addSound(ac6);
            SoundManager.getInstance().addAudioData("music", ad4);
            
            AudioSourcePerso asp = SoundManager.getInstance().getSound("music", Camera.main.gameObject.transform.position, 0.2f, "Music", true);
            asp.gameO.GetComponent<AudioSource>().Play();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("m"))
            {
                muted = !muted;
                if(muted)
                {
                    am.FindMatchingGroups("SFX")[0].audioMixer.SetFloat("volumeSFX", -80);
                    muted = true;
                }
                else
                {
                    am.FindMatchingGroups("SFX")[0].audioMixer.SetFloat("volumeSFX", 0);
                    muted = false;
                }
            }
        }
    }
}
