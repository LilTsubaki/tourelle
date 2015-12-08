using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{

    public class InitSound : MonoBehaviour
    {
        private AudioData ad1;
        public AudioClip ac1;
        public AudioClip ac2;
        public AudioClip ac3;

        private AudioData ad2;
        public AudioClip ac4;

        private AudioData ad3;
        public AudioClip ac5;

        // Use this for initialization
        void Awake()
        {
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
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
