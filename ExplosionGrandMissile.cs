using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionGrandMissile : MonoBehaviour {

    private Animation animExplo;
	void Start () {
        animExplo = GetComponent<Animation>();
        StartCoroutine(Explosion()); // appelle la couroutine Explosion
	}
	
	
	
    private IEnumerator Explosion()
    {
      //  FindObjectOfType<AudioManager>().Jouer("ExplosionMissile"); //joue l'animation de l'explosion
        yield return new WaitForSeconds(0.8f); //attends 0,8 secondes
        Destroy(gameObject); // détruit l'object
    }
}
