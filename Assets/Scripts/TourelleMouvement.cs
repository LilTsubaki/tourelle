using UnityEngine;
using System.Collections.Generic;

using Assets.Scripts;

    public class TourelleMouvement : MonoBehaviour
{
    public GameObject canon;
    public GameObject baseCanon;
    public GameObject baseTourelle;
    public GameObject balle;
    public ParticleSystem effetTire;
    public int puissanceBalle;

    private float vitesseRotate = 0;
    private int nbFrameParBalle = 0;
    private Pool<Bullet> lesBalles;
    private float beginTime;
    private float currentTime;
    private GameObject closestTarget;

    void Awake()
    {
        lesBalles = new Pool<Bullet>(new Bullet(balle));
    }

    // Use this for initialization
    void Start()
    {
 
    }

    void Update()
    {

    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(baseCanon.transform.position, baseCanon.transform.position+35*baseCanon.transform.forward);
    }

    void FixedUpdate()
    {
            closestTarget = getClosestTarget();
        if (closestTarget != null && Vector3.Distance(baseCanon.transform.position, closestTarget.transform.position) < 20)
        {
            Vector3 direction = closestTarget.transform.position - baseCanon.transform.position;

            Debug.DrawLine(baseCanon.transform.position, closestTarget.transform.position, Color.red);

            Vector3 newDirection = Vector3.RotateTowards(baseCanon.transform.forward, direction, 2 * Time.deltaTime, 0.0f);

            baseCanon.transform.rotation = Quaternion.LookRotation(newDirection);



            if (vitesseRotate < 35)
            {
                vitesseRotate += 0.1f;
            }
            canon.transform.Rotate(canon.transform.up, vitesseRotate, Space.World);


            if (vitesseRotate > 15)
            {
                if (nbFrameParBalle >= 10)
                {
                    GameObject temp = lesBalles.getFirst().prefab;
                    if (temp != null)
                    {
                        temp.transform.position = new Vector3(baseCanon.transform.position.x, baseCanon.transform.position.y, baseCanon.transform.position.z) + 3.5f * baseCanon.transform.forward;
                        //temp.transform.Rotate(baseCanon.transform.forward);
                        temp.transform.Rotate(baseCanon.transform.eulerAngles);
                        temp.GetComponent<Rigidbody>().AddForce(baseCanon.transform.forward * puissanceBalle);
                        effetTire.Play();
                        nbFrameParBalle = 0;
                    }

                }
            }
            else
            {
                effetTire.Stop();
            }
            nbFrameParBalle++;
        }
    }


    public GameObject getClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        GameObject currentTarget = null ;

        foreach (GameObject target in targets)
        {
            if(currentTarget == null)
            {
                currentTarget =  target;
            }
            else
            {
                if(Vector3.Distance(baseCanon.transform.position, target.transform.position) < Vector3.Distance(baseCanon.transform.position, currentTarget.transform.position))
                    currentTarget = target;
            }
        }
        return currentTarget;
    }

}
