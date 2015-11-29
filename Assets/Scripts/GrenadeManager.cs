using UnityEngine;
using System.Collections;

public class GrenadeManager : MonoBehaviour
{
    public float timedOut = 0.4f;
    private float currentTime = 0;
    public int degats;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (currentTime < timedOut)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                RaycastHit[] hits =  Physics.SphereCastAll(gameObject.transform.position, 2.0f, gameObject.transform.up);
                foreach(RaycastHit hit in hits)
                {
                    EnemyMovement enem  = hit.transform.GetComponent<EnemyMovement>();//currentTarget.GetComponent<EnemyMovement>();
                    if(enem != null)
                    {
                        hit.transform.GetComponent<Rigidbody>().AddExplosionForce(25, gameObject.transform.position, 3.0f, 5.0f, ForceMode.Impulse);
                        enem.isDeletable = false;
                        enem.lifeDown(degats);
                    }  
                }
                currentTime = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
