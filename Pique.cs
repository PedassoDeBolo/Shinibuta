using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pique : MonoBehaviour {
    private Collider2D zoneDeDegat;

    [HideInInspector]
    public Personnage personnage;

	void Start () {
        zoneDeDegat = GetComponent<Collider2D>();
        personnage = GameObject.FindGameObjectWithTag("Player").GetComponent<Personnage>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D zoneDeDegat)
    {
        if (zoneDeDegat.CompareTag("Player"))
        {
            StartCoroutine(personnage.PerdreVie(1, 9, personnage.transform.position));
        }
    }
}
