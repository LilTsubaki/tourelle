using UnityEngine;
using System.Collections.Generic;

using Assets.Scripts;
public class LanceGreandeScript : MonoBehaviour
{
    public GameObject baseTourelle;
    public GameObject grenade;
    public GameObject baseCanon;
    public GameObject canon;

    private Pool<Grenade> lesGrenades;
    private GameObject closestTarget;
    private int nbFrameParBalle = 0;


    void Awake()
    {
        lesGrenades = new Pool<Grenade>(new Grenade(grenade));
    }


    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        closestTarget = getClosestTarget();
        if (closestTarget != null && Vector3.Distance(baseCanon.transform.position, closestTarget.transform.position) < 30)
        {
            Vector3 direction = closestTarget.transform.position - baseCanon.transform.position;

            Debug.DrawLine(baseCanon.transform.position, closestTarget.transform.position, Color.red);

            Vector3 newDirection = Vector3.RotateTowards(baseCanon.transform.forward, direction, 2 * Time.deltaTime, 0.0f);

            baseCanon.transform.rotation = Quaternion.LookRotation(newDirection);


            float velocity = Mathf.Sqrt(15.0f * 9.8f);
            float dist = Vector3.Distance(new Vector3(baseCanon.transform.position.x, baseCanon.transform.position.y, baseCanon.transform.position.z) + 2.0f * baseCanon.transform.forward, closestTarget.transform.position);
            float angle = Mathf.Asin(9.8f * dist / (velocity * velocity)) / 2.0f;
            
            if(9.8f * dist / (velocity * velocity) <= 1)
            {
                angle = angle * Mathf.Rad2Deg;
                float vraiAngle = Mathf.Lerp(canon.transform.eulerAngles.z, -angle+90, Time.deltaTime * 2.0f);
                canon.transform.rotation = Quaternion.Euler(canon.transform.eulerAngles.x, canon.transform.eulerAngles.y, vraiAngle);
            }
            

            if (nbFrameParBalle >= 150)
            {
                GameObject grenadeTemp = lesGrenades.getFirst().prefab;
                if (grenadeTemp != null)
                {
                    grenadeTemp.transform.position = new Vector3(baseCanon.transform.position.x, baseCanon.transform.position.y, baseCanon.transform.position.z) + 2.0f * baseCanon.transform.forward;
                    grenadeTemp.GetComponent<Rigidbody>().velocity=baseCanon.transform.forward * velocity;
                    
                }
                nbFrameParBalle = 0;
            }
            nbFrameParBalle++;
        }
       
    }



    public GameObject getClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        GameObject currentTarget = null;

        foreach (GameObject target in targets)
        {
            if (currentTarget == null)
            {
                currentTarget = target;
            }
            else
            {
                if (Vector3.Distance(baseCanon.transform.position, target.transform.position) < Vector3.Distance(baseCanon.transform.position, currentTarget.transform.position))
                    currentTarget = target;
            }
        }
        return currentTarget;
    }
}
