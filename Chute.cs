using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chute : MonoBehaviour {
    private Collider2D chute;
    void Start () {
        chute = GetComponent<Collider2D>();
	}

    private void OnTriggerEnter2D(Collider2D chute)
    {
        if (chute.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
