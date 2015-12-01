using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int cpt = 0;
    public bool first = true;
    private int life = 15;
    private int currentLife = 15;
    public bool isDeletable = true;
    private float timedOut = 1.0f;
    private float currentTime = 0;
    public bool isFrozen = false;

    // Use this for initialization
    void Awake()
    {
        
    }


    void Start()
    {
        
    }

    void Update()
    {
        if(!isDeletable)
        {
            if (currentTime < timedOut)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                currentTime = 0;
                isDeletable = true;
            }
        }
        

        if (wayPoints[0] != null)
        {
            if (first)
            {
                transform.position = wayPoints[0].position;
                first = false;
            }
            else
            {
                if (Vector3.Distance(transform.position, wayPoints[cpt + 1].position) < 0.1f)
                {
                    if (cpt + 1 < wayPoints.Count - 1)
                        cpt++;
                    else
                    {
                        //cpt = -1;
                        gameObject.SetActive(false);
                    }
                }
                else
                {
                    if(!isFrozen)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, wayPoints[cpt + 1].position, 0.05f);
                    }
                }
            }
        }
        //Debug.Log(transform.GetComponent<Rigidbody>().angularVelocity.magnitude);
        if (currentLife <= 0 && isDeletable)
        {
            isFrozen = false;
            gameObject.SetActive(false);
            currentLife = life;
        }    
    }

    public void lifeDown(int nb)
    {
        currentLife -= nb;
    }
        
}
