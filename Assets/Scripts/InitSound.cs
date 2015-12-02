using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{

    public class InitSound : MonoBehaviour
    {
        private AudioData ad;
        public AudioClip ac1;

        // Use this for initialization
        void Start()
        {
            ad = new AudioData();
            ad.addSound(ac1);
            SoundManager.getInstance().addAudioData("pewpew", ad);

            AudioSourcePerso asp = SoundManager.getInstance().getSound("pewpew", new Vector3(1.9f, 10.85f, -22.61f));
            asp.gameO.GetComponent<AudioSource>().Play();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
