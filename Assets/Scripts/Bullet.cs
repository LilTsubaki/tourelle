using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Bullet : ObjetPoolable<Bullet>
    {
        public GameObject prefab;

        public Bullet()
        {
        }

        public Bullet(GameObject pre)
        {
            prefab = pre;
        }

        public void Copy(Bullet b)
        {
            prefab = GameObject.Instantiate<GameObject>(b.prefab);

            if (GameObject.Find("bulletPool") == null)
                new GameObject("bulletPool");

            prefab.transform.parent = GameObject.Find("bulletPool").transform;
        }

        public bool isAvailable()
        {
            return !prefab.activeSelf;
        }

        public void putUnavailable()
        {
            prefab.SetActive(true);
            prefab.GetComponent<Rigidbody>().velocity = Vector3.zero;
            prefab.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            prefab.transform.position = Vector3.zero;
            prefab.transform.rotation = Quaternion.identity;
        }
    }
}
