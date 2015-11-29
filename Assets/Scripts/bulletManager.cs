using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class bulletManager : MonoBehaviour
{
    public float timedOut = 3;
    private float currentTime = 0;
    public int degats;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(gameObject.activeSelf)
        {
            GameObject currentTarget = getClosestTarget();
            if(currentTarget != null)
            {
                if (Vector3.Distance(gameObject.transform.position, currentTarget.transform.position) < 1.0)
                {
                    //Debug.Log("hit");
                    EnemyMovement enem = currentTarget.GetComponent<EnemyMovement>();
                    if(enem != null)
                    {
                        enem.lifeDown(degats);
                        
                    }
                    gameObject.SetActive(false);
                }
            }
           

            if(currentTime < timedOut)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                gameObject.SetActive(false);
            }
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
                if (Vector3.Distance(gameObject.transform.position, target.transform.position) < Vector3.Distance(gameObject.transform.position, currentTarget.transform.position))
                    currentTarget = target;
            }
        }
        return currentTarget;
    }
}
