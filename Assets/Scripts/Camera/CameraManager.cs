using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {
	public GameObject followedActor;
	[Range(0.1f,10.0f)]
	public float focusSpeed =1.0f;
	[Range(1.0f,5.0f)]
	public float zoomSpeed = 2.0f;
	[Range(1.0f,10.0f)]
	public float rotationSpeed = 2.0f;
	[Range(0.2f,20.0f)]
	public float zoomMin = 5.0f;
	[Range(2.0f,20.0f)]
	public float zoomMax = 15.0f;
	[Range(2.0f,10.0f)]
	public float dragSpeed = 3.0f;
	[Range(1.0f,5.0f)]
	public float smoothSpeed = 2.0f;
	private Vector3 initialPosition;
	private Quaternion initialRotation;
	private ArmLenghtManager armLenght;
	private float initialArmLenght;
	private float newArmLenght;
	private float newZSmooth =0.0f;
	private float newXSmooth =0.0f;
	private float[] sampleSmoothX;
	private float[] sampleSmoothZ;
	private int indexSampleSmooth;
	private bool sampleSmoothReseted = true;
	public GameObject planet;
	public GameObject pivotY;
	public GameObject pivotX;
	private float rotationY;
	private bool manualmode = false;
	private float actualRotationX = 0.0f;
	private float newSmoothedX = 0.0f;
	private float actualSmoothedX = 0.0f;

	private float actualRotationY = 0.0f;
	private float newSmoothedY = 0.0f;
	private float actualSmoothedY = 0.0f;
	private float newX;
	private float newY;
	private float newZ;
	// Use this for initialization
	void Awake(){
		armLenght = GetComponent<ArmLenghtManager>();
		initialPosition = transform.position;
		initialRotation= transform.rotation;
		initialArmLenght = armLenght.armLenght;
		rotationY = pivotY.transform.rotation.eulerAngles.y;
		newArmLenght = initialArmLenght;
		newZSmooth = transform.position.z;
		newXSmooth = transform.position.x;
		sampleSmoothX = new float[10];
		sampleSmoothZ = new float[10];
	}

	void Start () {
	}
	
	// Update is called once per frame
	void Update(){
		if(Input.GetKeyDown(KeyCode.M)){
			Debug.Log("coucou");
			manualmode = !manualmode;
		}
		if(Input.GetKey(KeyCode.LeftControl) && manualmode){
			rotationY += Input.GetAxis("Mouse ScrollWheel") * rotationSpeed * 20.0f;
		}else{
			newArmLenght -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed*5.0f;
			newArmLenght = Mathf.Clamp(newArmLenght,zoomMin,zoomMax);
		}
	}

	void FixedUpdate () {
		initialArmLenght = Mathf.Lerp(initialArmLenght,newArmLenght,Time.deltaTime * Mathf.Sqrt(zoomSpeed*20.0f));
		pivotY.transform.localRotation = Quaternion.Euler(pivotY.transform.localRotation.eulerAngles.x,Mathf.LerpAngle(pivotY.transform.localRotation.eulerAngles.y,rotationY,Time.deltaTime*Mathf.Sqrt(rotationSpeed*20.0f)),pivotY.transform.localRotation.eulerAngles.z);
		armLenght.armLenght = initialArmLenght;

		if(!manualmode){ //if(followedActor.isSelected()){
			rotationY = 0.0f;
			transform.position = Vector3.Lerp(transform.position, followedActor.transform.position, Time.deltaTime * focusSpeed * 5.0f);
			transform.rotation = Quaternion.Lerp(transform.rotation, followedActor.transform.rotation, Time.deltaTime * focusSpeed * 5.0f);

		}else{
			if (Input.GetMouseButton(1))
			{
				float ztmp,xtmp;
				ztmp = 0.0f;
				xtmp = 0.0f;
				float z = Input.GetAxis("Mouse Y")*dragSpeed;
				float x = Input.GetAxis("Mouse X")*dragSpeed;

				float cosinus = Mathf.Cos(pivotY.transform.localRotation.eulerAngles.y * Mathf.Deg2Rad);
				float sinus = Mathf.Sin(pivotY.transform.localRotation.eulerAngles.y * Mathf.Deg2Rad);
				float newZ = z * cosinus - x * sinus;
				float newX = x * cosinus + z * sinus;
				actualRotationX += newX;
				actualRotationY += newZ;
				armLenght.armLenght = initialArmLenght;
				transform.RotateAround (planet.transform.position, transform.right,-newZ);
				transform.RotateAround (planet.transform.position, transform.forward, newX);

				if(indexSampleSmooth >= 10){
					indexSampleSmooth =0;
				}
				sampleSmoothX[indexSampleSmooth]=newX;
				sampleSmoothZ[indexSampleSmooth]=newZ;
				indexSampleSmooth +=1;
				for(int i=0;i<10;i++){
					ztmp += sampleSmoothZ[i];
					xtmp += sampleSmoothX[i];
				}
				ztmp /=smoothSpeed*15.0f;
				xtmp /=smoothSpeed*15.0f;
				if(ztmp<1.0f){
					newZSmooth = actualRotationY - ztmp*Mathf.Sqrt(armLenght.armLenght/3.0f);
				}else{
					newZSmooth = actualRotationY - ztmp*Mathf.Abs(ztmp)*Mathf.Sqrt(armLenght.armLenght/3.0f);
				}
				if(xtmp<1.0f){
					newXSmooth = actualRotationX - xtmp*Mathf.Sqrt(armLenght.armLenght/3.0f);
				}else{
					newXSmooth = actualRotationX - xtmp*Mathf.Abs(xtmp)*Mathf.Sqrt(armLenght.armLenght/3.0f);
				}
				actualSmoothedX = newXSmooth;
				actualSmoothedY = newZSmooth;
				newSmoothedX = actualSmoothedX - actualRotationX;
				newSmoothedY = actualSmoothedY - actualRotationY;
				sampleSmoothReseted = false;

			}else{
				if(!sampleSmoothReseted){
					for(int i=0;i<10;i++){
						sampleSmoothZ[i]=0.0f;
						sampleSmoothX[i]=0.0f;
					}
					sampleSmoothReseted = true;
				}
				newSmoothedX = Mathf.Lerp(newSmoothedX,0.0f,Time.deltaTime * 5.0f);
				newSmoothedY = Mathf.Lerp(newSmoothedY,0.0f,Time.deltaTime * 5.0f);
				transform.RotateAround (planet.transform.position, transform.forward, -newSmoothedX);
				transform.RotateAround (planet.transform.position, transform.right, newSmoothedY);
			}
		}
	}

	void OnEnable(){
	
	}

	void OnDestroy(){

	}

	public void SetFollowedActor(GameObject obj){
		followedActor = obj;
	}
}
