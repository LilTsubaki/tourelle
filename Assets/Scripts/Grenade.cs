using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    public class Grenade : ObjetPoolable<Grenade>
    {

        public GameObject prefab;

        public Grenade()
        {
        }

        public Grenade(GameObject pre)
        {
            prefab = pre;
        }

        public void Copy(Grenade b)
        {
            prefab = GameObject.Instantiate<GameObject>(b.prefab);

            if (GameObject.Find("grenadePool") == null)
                new GameObject("grenadePool");

            prefab.transform.parent = GameObject.Find("grenadePool").transform;
        
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
