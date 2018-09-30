using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DegatSlimeCochon : MonoBehaviour {
    public int vieMax;
    public int vie;
    private Rigidbody2D SlimeCochon1;
    private Transform Personnage;
    private float VitY;
    private SlimeCochon slimeCochon;
   
    void Start ()

    {
        slimeCochon = GameObject.FindGameObjectWithTag("Ennemi").GetComponent<SlimeCochon>();
        VitY = 75 * Time.deltaTime;
        vie = vieMax;
        SlimeCochon1 = GetComponent<Rigidbody2D>();
        Personnage = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
   

    public void RecevoirDegat(int degatsRecu)
    {
        Debug.Log("je recule");
        vie -= degatsRecu;
        gameObject.GetComponent<Animation>().Play("Douleur");
        
        if (Personnage.position.x > SlimeCochon1.position.x)
        {
            SlimeCochon1.AddForce(Vector2.left * 8000 * Time.deltaTime);

            SlimeCochon1.velocity = Vector2.up * VitY * 3;

        }
        else if (Personnage.position.x < SlimeCochon1.position.x)
        {
            SlimeCochon1.AddForce(Vector2.right * 8000 * Time.deltaTime);

            SlimeCochon1.velocity = Vector2.up * VitY;

        }
        if(vie <=0)
        {
            StartCoroutine(slimeCochon.Mort());
        }
    }
}
