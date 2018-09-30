using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLancement : MonoBehaviour {

    // lorsque le joueur rentre dans une zone, elle déclenche plusieurs choses pour le combat contre le boss, puis s'efface
    private Collider2D changement;
    public camera camera1;
    public Camera cam;
    public Transform background;
    public float hauteur;
    public float largeur;
    public GameObject boss;
    private bool verif;


    void Start ()
    {
        gameObject.SetActive(true);
        verif = false;
        changement = GetComponent<Collider2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>(); // mets dans la variable cam le component Camera
        camera1 = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<camera>(); // mets dans la variable camera1 le script de la camera
        
    }
    private void OnTriggerEnter2D(Collider2D changement) /* quand le protagoniste rentre dans la zone,
        verifie si c'est le joueur et que la verif est fausse, change la taille de la camera et joue une musique, 
        active la verif commme ça la zone ne peut être active qu'une fois et active le boss*/
    {

        if (changement.CompareTag("Player") && verif == false)
        {
            verif = true;
            FindObjectOfType<AudioManager>().Arreter("Theme3");
            FindObjectOfType<AudioManager>().Jouer("Theme1");

            boss.SetActive(true);
            camera1.xMin = 249.23f;
            camera1.yMin = 7.8f;
            cam.orthographicSize = 12;
            background.transform.localScale = new Vector2(4.871265f, 4.871265f);
            gameObject.SetActive(false);
        }
    }

}
