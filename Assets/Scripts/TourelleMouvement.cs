using UnityEngine;
using System.Collections.Generic;

using Assets.Scripts;

    public class TourelleMouvement : MonoBehaviour
{
    public Camera cam;
    public GameObject canon;
    public GameObject baseCanon;
    public GameObject baseTourelle;
    public GameObject balle;
    public ParticleSystem effetTire;
    public int puissanceBalle;

    private float vitesseRotate = 0;
    private int nbFrameParBalle = 0;
    private Pool<Bullet> lesBalles;
    private bool auto = false;
    private bool end = false;
    private bool bestScoreSet = false;
    private float beginTime;
    private float currentTime;
    private GameObject closestTarget;

    void Awake()
    {
        lesBalles = new Pool<Bullet>(new Bullet(balle));
        beginTime = Time.time;
    }

    // Use this for initialization
    void Start()
    {
 
    }

    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            if (auto == true)
            {
                auto = false;
            }
            else
            {
                auto = true;
            }
        }
        if (Input.GetKeyDown("p") && !end)
        {
            end = true;
            currentTime = Time.time;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(baseCanon.transform.position, baseCanon.transform.position+35*baseCanon.transform.forward);
    }

    void FixedUpdate()
    {
        
        if (!end)
        {
            /**********************mode manuel****************************/
            if (!auto)
            {
                if (Input.GetKey("space"))
                {
                    if (vitesseRotate < 35)
                    {
                        vitesseRotate += 0.1f;
                    }
                    canon.transform.Rotate(canon.transform.up, vitesseRotate, Space.World);
                }
                else
                {
                    if (vitesseRotate > 0)
                    {
                        canon.transform.Rotate(canon.transform.up, vitesseRotate, Space.World);
                        vitesseRotate -= 0.2f;
                    }
                }


                if (Input.GetKey(KeyCode.UpArrow))
                {
                    if (((baseCanon.transform.rotation.eulerAngles.z + 90) % 360) - Input.GetAxis("Vertical") * Time.fixedDeltaTime * 50 > 35)
                    {
                        baseCanon.transform.Rotate(-baseCanon.transform.right, Input.GetAxis("Vertical") * Time.fixedDeltaTime * 50, Space.World);
                    }
                }

                if (Input.GetKey(KeyCode.DownArrow))
                {
                    if (((baseCanon.transform.rotation.eulerAngles.z + 90) % 360) - Input.GetAxis("Vertical") * Time.fixedDeltaTime * 50 < 120)
                    {
                        baseCanon.transform.Rotate(-baseCanon.transform.right, Input.GetAxis("Vertical") * Time.fixedDeltaTime * 50, Space.World);
                    }
                }
                baseCanon.transform.Rotate(baseTourelle.transform.up, Input.GetAxis("Horizontal") * Time.fixedDeltaTime * 50, Space.World);
            }
            /**********************mode auto****************************/
            else
            {

                closestTarget = getClosestTarget();
                if (closestTarget != null && Vector3.Distance(baseCanon.transform.position, closestTarget.transform.position) < 20)
                {
                    Vector3 direction = closestTarget.transform.position - baseCanon.transform.position;

                    Debug.DrawLine(baseCanon.transform.position, closestTarget.transform.position, Color.red);

                    Vector3 newDirection = Vector3.RotateTowards(baseCanon.transform.forward, direction, 2 * Time.deltaTime, 0.0f);

                    baseCanon.transform.rotation = Quaternion.LookRotation(newDirection);
                }


                if (vitesseRotate < 35)
                {
                    vitesseRotate += 0.1f;
                }
                canon.transform.Rotate(canon.transform.up, vitesseRotate, Space.World);
            }

            if (vitesseRotate > 15)
            {
                if (nbFrameParBalle >= 20)
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
        else
        {
            if(!bestScoreSet && currentTime > PlayerPrefs.GetFloat("bestScore"))
            {
                PlayerPrefs.SetFloat("bestScore", Time.time);
                bestScoreSet = true;
                
            }
            Debug.Log("Current time is : " + currentTime);
            Debug.Log("Best time is :" + PlayerPrefs.GetFloat("bestScore"));
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
