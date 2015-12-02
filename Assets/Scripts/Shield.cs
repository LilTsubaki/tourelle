using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{

    public int life = 1;
    public int currentLife = 1;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void lifeDown(int nb)
    {
        currentLife -= nb;
        if (currentLife <= 0)
        {
            gameObject.SetActive(false);
            currentLife = life;
        }  
    }
}
