using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {
    [SerializeField]
    public float xMax;

    [SerializeField]
    public float yMax;

    [SerializeField]
    public float xMin;

    [SerializeField]
    public float yMin;

    private Transform cible;
    
   
 

    // Use this for initialization
    void Start () {
        
        cible = GameObject.Find("Shinibuta").transform;
        
	}
	
	// Update is called once per frame
	void LateUpdate ()
    {
        transform.position = new Vector3(Mathf.Clamp(cible.position.x, xMin, xMax), Mathf.Clamp(cible.position.y, yMin, yMax),transform.position.z); // on transforme la position de la camera par rapport au joueur(cible) et qu'elle ne dépasse jamais x et y min et max(avec Mathf clamp)
	}
}
