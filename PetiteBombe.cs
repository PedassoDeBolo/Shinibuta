using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetiteBombe : MonoBehaviour {
    private Collider2D collPetiteBombe;
    public GameObject explosion;
    private Shinibuta personnage;
    private string nomAnim = "ExplosionPetite";
	void Start ()
    {
        personnage = FindObjectOfType<Shinibuta>();
        explosion = Resources.Load("Explosion/" + nomAnim, typeof(GameObject)) as GameObject;
        collPetiteBombe = GetComponent<Collider2D>();
	}
	
	
	void Update ()
    {
		
	}
    private void OnTriggerEnter2D(Collider2D collPetiteBombe)
    {
        if (collPetiteBombe.CompareTag("Player"))
        {

            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
            StartCoroutine(personnage.Degats(1));
        }
        else if (!collPetiteBombe.CompareTag("Attaque"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
       
    }

}
