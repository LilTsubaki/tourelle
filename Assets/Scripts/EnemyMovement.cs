﻿using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> wayPoints;
    public int cpt = 0;
    public bool first = true;
    public float life;
    public float currentLife;
    public bool isDeletable = true;
    private float timedOut = 1.0f;
    private float currentTime = 0;
    public bool isFrozen = false;
    public Scrollbar lifeBar;

    // Use this for initialization
    void Awake()
    {
        
    }


    void Start()
    {
        
    }

    void Update()
    {
        lifeBar.transform.LookAt(Camera.main.transform);
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
                        transform.position = Vector3.MoveTowards(transform.position, wayPoints[cpt + 1].position, 0.05f* Time.timeScale);
                    }
                }
            }
        }
        //Debug.Log(transform.GetComponent<Rigidbody>().angularVelocity.magnitude);
        if ((currentLife <= 0 && isDeletable) || transform.position.y < -4)
        {
            isFrozen = false;
            gameObject.SetActive(false);
            currentLife = life;
        }    
    }

    public void lifeDown(int nb)
    {
        currentLife -= nb;
        lifeBar.size = currentLife / life;
    }
        
}
