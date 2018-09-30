using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PorteSortie : MonoBehaviour {

    private Collider2D collPorte;
    
	void Start () {
        collPorte = GetComponent<Collider2D>();
        
        gameObject.SetActive(false);
	}
	
	
	
    private void OnTriggerEnter2D(Collider2D collPorte)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
    }
}
