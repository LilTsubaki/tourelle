using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ArmLenghtManager : MonoBehaviour {
	[Range(0.0f,30.0f)]
	public float armLenght;
	public GameObject localCamera;

	void Awake(){
		localCamera.transform.localPosition = new Vector3(0,0,-armLenght);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		localCamera.transform.localPosition = new Vector3(0,0,-armLenght);
	}
}
