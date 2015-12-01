﻿using UnityEngine;
using System.Collections;

public class Powers : MonoBehaviour
{
    private bool isPower1Up = true;
    private bool is1Pressed = false;

    private bool isPower2Up = true;
    private bool is2Pressed = false;

    private float timedOutPower1 = 5.0f;
    private float currentTimePower1 = 0;

    private float timedOutPower2 = 10.0f;
    private float currentTimePower2 = 0;
    private bool unfroze = true;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if(!isPower1Up)
        {
            if (currentTimePower1 < timedOutPower1)
            {
                currentTimePower1 += Time.deltaTime;
            }
            else
            {
                currentTimePower1 = 0;
                isPower1Up = true;
                Debug.Log("Power 1 is up !!!!!!!!!!!!!!!!!!!");
            }
        }

        if (!isPower2Up)
        {
            if (currentTimePower2 < timedOutPower2)
            {
                if (currentTimePower2 > timedOutPower2/4 && unfroze)
                {
                    Debug.Log("unfroze");
                    GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
                    foreach (GameObject target in targets)
                    {
                        target.transform.GetComponent<EnemyMovement>().isFrozen = false;
                    }
                    unfroze = false;
                }
                currentTimePower2 += Time.deltaTime;
            }
            else
            {    
                currentTimePower2 = 0;
                isPower2Up = true;
                unfroze = true;
                Debug.Log("Power 2 is up !!!!!!!!!!!!!!!!!!!");
            }
        }

        if (Input.GetKeyDown("1"))
        {
            is1Pressed = true;
            is2Pressed = false;
        }

        if (Input.GetKeyDown("2"))
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");

            foreach (GameObject target in targets)
            {
                target.transform.GetComponent<EnemyMovement>().isFrozen = true;
            }
            is1Pressed = false;
            is2Pressed = true;
            isPower2Up = false;
        }

        if (Input.GetMouseButtonDown(0) && is1Pressed && isPower1Up)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Debug.DrawLine(ray.origin, ray.direction * 20);
            RaycastHit rch;

            if (Physics.Raycast(ray, out rch))
            {
                RaycastHit[] hits = Physics.SphereCastAll(rch.point, 4.0f, gameObject.transform.up);
                foreach (RaycastHit hit in hits)
                {
                    EnemyMovement enem = hit.transform.GetComponent<EnemyMovement>();//currentTarget.GetComponent<EnemyMovement>();
                    if (enem != null)
                    {
                        hit.transform.GetComponent<Rigidbody>().AddExplosionForce(15, rch.point, 3.0f, 5.0f, ForceMode.Impulse);
                    }
                }
            }
            is1Pressed = false;
            isPower1Up = false;
        }   
    }
}
