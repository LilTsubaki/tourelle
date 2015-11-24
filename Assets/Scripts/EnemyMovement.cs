using UnityEngine;
using System.Collections.Generic;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int cpt = 0;
    public bool first = true;
    private int life = 3;
    private int currentLife = 3;

    // Use this for initialization
    void Awake()
    {
        
    }


    void Start()
    {
        
    }

    void Update()
    {
        
        if(wayPoints[0] != null)
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
                    transform.position = Vector3.MoveTowards(transform.position, wayPoints[cpt + 1].position, 0.05f);
                }
            }
        }
    }

    public void lifeDown()
    {
        currentLife--;
        if(currentLife == 0)
        {
            gameObject.SetActive(false);
            currentLife = life;
        }
    }
        
}
