using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Capteur : MonoBehaviour {
    private Collider2D collisionCapteur;
    public Vroum vroum;
   
    void Start () {
        if (GameObject.Find("EnnemiVroum") != null)
        {
            vroum = GameObject.FindGameObjectWithTag("EnnemiVroum").GetComponent<Vroum>();
        }
        
        collisionCapteur = GetComponent<Collider2D>();
        
    }

   

   
    private void OnTriggerEnter2D(Collider2D collisionCapteur)
    {
        if(collisionCapteur.isTrigger !=true && collisionCapteur.CompareTag("EnnemiVroum"))
        {
            vroum.Flip();
        }
    }
}
