using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private Transform[] lieuxSpawn;



    [SerializeField]
    private int nbSpawn;


    [SerializeField]
    private GameObject[] ennemiSpawn;


    [SerializeField]
    private float tempsSpawn;

    private bool trigger;

	void Start ()
    {
        trigger = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && trigger == false)
        {
            StartCoroutine(Spawn());
            trigger = true;
        }
    }
        private IEnumerator Spawn()
        {
        for (int i = nbSpawn; i > 0;)
        {
            

            foreach (Transform point in lieuxSpawn)
            {
                Instantiate(ennemiSpawn[0], lieuxSpawn[0]);
                
                if (lieuxSpawn.Length >=1)
                {
                    Instantiate(ennemiSpawn[0], lieuxSpawn[1]);
                    yield return new WaitForSeconds(tempsSpawn);
                }
                    
                 if (ennemiSpawn.Length > 2)
                 {
                    yield return new WaitForSeconds(tempsSpawn);
                    Instantiate(ennemiSpawn[1]);
                 }

                i--;
            }
            if (nbSpawn == 0)
            {
                yield return 0;
            }
            
        }
        

    }
}
