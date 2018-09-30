using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CochonSlime : MonoBehaviour {

    [SerializeField]
    private Transform[] verifSols;

    [SerializeField]
    private LayerMask verifSol;

    [SerializeField]
    private float vieMax;


    private Rigidbody2D slimeRigidbody;
    private Transform Shinibuta;
    private Animator slimeAnim;
    private Collider2D slimeCollider;
    private Shinibuta perso;

    private bool pauseSaut;
    private bool parTerre;
    private float solRadiant = 0.03f;
    private float tempsSaut;
    private float vitY;
    public float vie;
    
    


    void Start ()
    {
        vie = vieMax;
        tempsSaut = 3;
        vitY = 500 * Time.deltaTime;
        Shinibuta = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        perso = GameObject.FindGameObjectWithTag("Player").GetComponent<Shinibuta>();
        slimeAnim = GetComponent<Animator>();
        slimeRigidbody = GetComponent<Rigidbody2D>();
        slimeCollider = GetComponent<Collider2D>();
        slimeRigidbody.angularVelocity = 0;
    }	
	
	void FixedUpdate ()
    {
        if (vie > 0)
        {
            parTerre = EstParTerre();
            Saut();
            SlimeAnim();
        }
        else
        {
            StartCoroutine(Mort());
        }
        
	}

    private bool EstParTerre()
    {
        if (slimeRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in verifSols)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, solRadiant, verifSol);
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)
                    {

                        return true; // si les verificateurs on une collision autre que le joueur, retourne vrai

                    }
                }
            }
        }
        return false;
    }

        private void Saut()
        {
            if (Vector2.Distance(transform.position, Shinibuta.position) < 20)
            {
                tempsSaut -= Time.deltaTime;
                if (tempsSaut <= 0)
                {
                    pauseSaut = true;
                }

           

                if (Shinibuta.position.x > slimeRigidbody.position.x)
                {
                    if (parTerre == true && tempsSaut < 1)
                    {
                        Vector3 lTemp = transform.localScale;
                        lTemp.x = -1;
                        transform.localScale = lTemp;
                        slimeRigidbody.AddForce(Vector2.right * 16000 * Time.deltaTime);
                        slimeRigidbody.velocity = Vector2.up * vitY;


                    }

                }

                if (Shinibuta.position.x < slimeRigidbody.position.x)
                {
                    if (parTerre == true && tempsSaut < 1)
                    {
                        Vector3 lTemp = transform.localScale;
                        lTemp.x = 1;
                        transform.localScale = lTemp;
                        slimeRigidbody.AddForce(Vector2.left * 16000 * Time.deltaTime);
                        slimeRigidbody.velocity = Vector2.up * vitY;


                    }

                }

                if (pauseSaut == true)
                {
                    tempsSaut = 3;
                    pauseSaut = false;
                }


            }
        }

            private void SlimeAnim()
            {
                if (slimeRigidbody.velocity.y > 0 && !parTerre)
                {
                    slimeAnim.SetBool("SlimeCochonSautant", true);
                    slimeAnim.SetBool("SlimeCochonAterrisage", false);
                }
                else if (parTerre)
                {
                    slimeAnim.SetBool("SlimeCochonSautant", false);
                    slimeAnim.SetBool("SlimeCochonAterrisage", true);
                }
            }

                private void OnTriggerEnter2D(Collider2D collision)
                {
                    if (collision.CompareTag("Player"))
                    {
                        StartCoroutine(perso.Degats(1));

                    }
                }

            public void RecevoirDegat(int degatsRecu)
            {
                Debug.Log("je recule");
                vie -= degatsRecu;
                gameObject.GetComponent<Animation>().Play("Douleur");
                if (Shinibuta.position.x > slimeRigidbody.position.x)
                {
                    slimeRigidbody.AddForce(Vector2.left * 9000 * Time.deltaTime);

                    slimeRigidbody.velocity = Vector2.up * vitY/2.5f;

                }
                else if (Shinibuta.position.x < slimeRigidbody.position.x)
                {
                    slimeRigidbody.AddForce(Vector2.right * 9000 * Time.deltaTime);

                    slimeRigidbody.velocity = Vector2.up * vitY/2.5f;

                }
            }

        private IEnumerator Mort()
        {
            gameObject.GetComponent<Animation>().Play("Douleur");
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
}
