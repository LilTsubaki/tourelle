using UnityEngine;
using System.Collections;

using Assets.Scripts;
public class TourelleLaser : MonoBehaviour
{

    public GameObject baseTourelle;
    public GameObject baseCanon;

    private GameObject closestTarget;
    private int nbFrameParBalle = 0;


    // Use this for initialization
    void Start () {
	
	}

    void FixedUpdate()
    {
        closestTarget = getClosestTarget();
        if (closestTarget != null && Vector3.Distance(baseCanon.transform.position, closestTarget.transform.position) < 40)
        {
            Vector3 direction = closestTarget.transform.position - baseCanon.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(baseCanon.transform.forward, direction, 8 * Time.deltaTime, 0.0f);

            baseCanon.transform.rotation = Quaternion.LookRotation(newDirection);

            if (nbFrameParBalle >= 10)
            {
                Vector3 temp = new Vector3(baseCanon.transform.position.x, baseCanon.transform.position.y, baseCanon.transform.position.z) + 2.0f * baseCanon.transform.forward;
                RaycastHit rch;
                if(Physics.Raycast(temp, closestTarget.transform.position - baseCanon.transform.position, out rch))
                {
                    EnemyMovement enem = rch.transform.GetComponent<EnemyMovement>();
                    if (enem != null)
                    {
                        enem.lifeDown(1);
                    }
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
