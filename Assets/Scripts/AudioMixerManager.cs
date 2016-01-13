using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

namespace Assets.Scripts
{

    public class AudioMixerManager : MonoBehaviour
    {
        private bool muted = false;
        public AudioMixer am;


        public void mute()
        {
            muted = !muted;
            if (muted)
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
