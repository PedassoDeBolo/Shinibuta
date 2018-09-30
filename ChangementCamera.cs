using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangementCamera : MonoBehaviour {

    private Collider2D changement;
    private camera camera1;

	void Start ()
    {
        changement = GetComponent<Collider2D>();
		camera1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>();

    }
	
	void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D changement) // je baisse la camera vers le bas pour que les pics soient visibles
    {
        camera1.yMin = -4;
    }
}
