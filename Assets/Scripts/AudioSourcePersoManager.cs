using UnityEngine;
using System.Collections;

public class AudioSourcePersoManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(!gameObject.GetComponent<AudioSource>().isPlaying)
        {
            gameObject.SetActive(false);
        }
	}
}
