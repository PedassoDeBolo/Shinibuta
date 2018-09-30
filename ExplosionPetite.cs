using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPetite : MonoBehaviour {
    private Animation animExplo;
    


    void Start () {
        animExplo = GetComponent<Animation>();
        StartCoroutine(Explosion()); // Appelle la couroutine Explosion
    }
	
	// Update is called once per frame
	
    private IEnumerator Explosion()
    {
        //FindObjectOfType<AudioManager>().Jouer("ExplosionBombe"); joue le son de l'explosion
        yield return new WaitForSeconds(0.8f); //attends 0,8 secondes
        Destroy(gameObject); // detruis l'object
    }
}
