using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vroum : MonoBehaviour {

    [SerializeField]
    private int Vitesse;


    [SerializeField]
    private int Vie;

    public Transform personnage;

    [SerializeField]
    private bool regardeGauche;


    private Shinibuta perso;
    int degatsRecu;
    float PuiRecul;
    Vector3 DirecRecul;
    private Rigidbody2D corpsVroum;
    public Collider2D collisionVroum;

    private bool AutoriseMarche;

    void Start ()
    {
        Vie = 3;
        regardeGauche = true;
        AutoriseMarche = true;
        personnage = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        corpsVroum = GetComponent<Rigidbody2D>();
        collisionVroum = GetComponent<Collider2D>();
        perso = GameObject.FindGameObjectWithTag("Player").GetComponent<Shinibuta>();
    }
	
	
	void FixedUpdate ()
    {
        if (Vie > 0)
        {
            Mouvement();
        }
        else if(Vie <= 0)
        {
            StartCoroutine(Mort());
        }
	}

    private void Mouvement()
    {
        if(regardeGauche && AutoriseMarche)
        {
            corpsVroum.velocity = Vector2.left * Vitesse;
        }
        else if(!regardeGauche && AutoriseMarche)
        {
            corpsVroum.velocity = Vector2.right * Vitesse;
        }
    }

    public void Flip()
    {
        if (corpsVroum.velocity.x > 0 && !regardeGauche || corpsVroum.velocity.x < 0 && regardeGauche)
        {
            regardeGauche = !regardeGauche;
            Vector3 Changement = transform.localScale;
            Changement.x *= -1;
            transform.localScale = Changement;
        }
    }

    private void OnTriggerEnter2D(Collider2D collisionVroum)
    {
        
        if (collisionVroum.CompareTag("Player"))
        {
            StartCoroutine(perso.Degats(1));

        }
    }

    public void RecevoirDegat(int degatRecu)
    {
        StartCoroutine(RecevoirDegatIE(1));
    }

    public IEnumerator RecevoirDegatIE(int degatsRecu)
    {
        Debug.Log("j'ai mal");
        Vie -= degatsRecu;
        gameObject.GetComponent<Animation>().Play("VroumDouleur");
        if (personnage.position.x > corpsVroum.position.x)
        {
            AutoriseMarche = false;
            
            
            
            corpsVroum.AddForce(Vector2.left * 80000 * Time.deltaTime);


            yield return new WaitForSeconds(1);
            AutoriseMarche = true;
            
            
           
        }
        else if (personnage.position.x < corpsVroum.position.x)
        {
            AutoriseMarche = false;
            
            
            corpsVroum.AddForce(Vector2.right * 80000 * Time.deltaTime);

            yield return new WaitForSeconds(1);
            AutoriseMarche = true;
            Debug.Log("je recule");
            
            
        }
        
    }

    IEnumerator Mort()
    {
        gameObject.GetComponent<Animation>().Play("VroumDouleur");
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

}
