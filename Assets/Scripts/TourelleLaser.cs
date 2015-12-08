using UnityEngine;
using System.Collections;

using Assets.Scripts;
public class TourelleLaser : MonoBehaviour
{

    public GameObject baseTourelle;
    public GameObject baseCanon;
    public Material lineMaterial;

    private GameObject closestTarget;
    private int nbFrameParBalle = 0;
    public GameObject rayon;
    private LineRenderer lineRenderer;


    //public Color c1 = Color.yellow;
    //public Color c2 = Color.red;


    // Use this for initialization
    void Start () {
        //lineRenderer = gameObject.AddComponent<LineRenderer>();
        //lineRenderer.material = new Material(Shader.Find("Assets/Graphics/Textures/Metal"));
        //lineRenderer.material = lineMaterial;
        //lineRenderer.SetColors(c1, c1);
        lineRenderer = rayon.GetComponent<LineRenderer>();
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(2);

    }

    void FixedUpdate()
    {
        closestTarget = getClosestTarget();
        if (closestTarget != null && Vector3.Distance(baseCanon.transform.position, closestTarget.transform.position) < 40)
        {
            Vector3 direction = closestTarget.transform.position - baseCanon.transform.position;
            Vector3 newDirection = Vector3.RotateTowards(baseCanon.transform.forward, direction, 8 * Time.deltaTime, 0.0f);

            baseCanon.transform.rotation = Quaternion.LookRotation(newDirection);

            if(nbFrameParBalle == 5)
            {
                rayon.SetActive(false);
            }
            if (nbFrameParBalle >= 15)
            {
                Vector3 temp = new Vector3(baseCanon.transform.position.x, baseCanon.transform.position.y, baseCanon.transform.position.z) + 2.0f * baseCanon.transform.forward;
                RaycastHit rch;

                lineRenderer.SetPosition(0, temp);
                
                rayon.SetActive(true);

                if (Physics.Raycast(temp, closestTarget.transform.position - baseCanon.transform.position, out rch))
                {

                    AudioSourcePerso asp = SoundManager.getInstance().getSound("pewpew", Camera.main.gameObject.transform.position);
                    asp.gameO.GetComponent<AudioSource>().Play();

                    lineRenderer.SetPosition(1, rch.transform.position);
                    EnemyMovement enem = rch.transform.GetComponent<EnemyMovement>();
                    Shield shield = rch.collider.transform.GetComponent<Shield>();
                    if (enem != null)
                    {
                        enem.lifeDown(1);
                    }
                    if (shield != null)
                    {
                        shield.lifeDown(1);
                    }
                }
                nbFrameParBalle = 0; 
            }
            nbFrameParBalle++;
        }
        else
        {
            rayon.SetActive(false);
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
