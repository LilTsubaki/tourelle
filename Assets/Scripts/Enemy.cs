using UnityEngine;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class Enemy : ObjetPoolable<Enemy>
    {
        public List<Transform> wayPoints;
        public GameObject prefab;

        public Enemy()
        {
            wayPoints = new List<Transform>();
        }

        public Enemy(int l)
        {
        }

        public Enemy(GameObject pre, List<Transform> points)
        {
            prefab = pre;
            wayPoints = points;
            //pre.transform.position = wayPoints[0].position;
            //Debug.Log(wayPoints[0].position);
        }

        public void Copy(Enemy b)
        {
            prefab = GameObject.Instantiate<GameObject>(b.prefab);
            wayPoints = b.wayPoints;
            //b.prefab.transform.position = wayPoints[0].position;
            prefab.GetComponent<EnemyMovement>().wayPoints = wayPoints;
        }

        public bool isAvailable()
        {
            return !prefab.activeSelf;
        }

        public void putUnavailable()
        {
            
            prefab.transform.position = wayPoints[0].position;
            prefab.transform.rotation = Quaternion.identity;
            prefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
            prefab.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            prefab.GetComponent<EnemyMovement>().cpt = 0;
            prefab.GetComponent<EnemyMovement>().first = true;
            prefab.SetActive(true);
        }


        /**/
    }
}