using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Assets.Scripts
{
    public class spawnManager : MonoBehaviour
    {
        public List<Transform> wayPoints;
        public GameObject enemy;
        private Pool<Enemy> Enemys;

        void Awake()
        {
            Enemys = new Pool<Enemy>(new Enemy(enemy, wayPoints));
        }


        // Use this for initialization
        void Start()
        {
            StartCoroutine("Spawn");
        }

        // Update is called once per frame
        void Update()
        {
        }

        IEnumerator Spawn()
        {
            while (true)
            {
                //Debug.Log("coroutine");
                Enemys.getFirst();
                yield return new WaitForSeconds(2f);
            }
        }
    }
}
