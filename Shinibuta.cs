using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shinibuta : MonoBehaviour {

    private Rigidbody2D shinibutaRigidbody;
    private Animator shinibutaAnimator;
    public BoxCollider2D zoneDegat;

    [SerializeField]
    private float vitesseMouvement;

    [SerializeField]
    private float vitesseSaut;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private LayerMask verifSol;

    [SerializeField]
    private float vie;

    private bool saut;
    private bool regardeDroite;
    private bool attaque;
    private bool parTerre;
    private float horizontal;
    private float solRadiant = 0.3f;
    private float tempsAttaque;
    
    private float vieMax = 3;

    void Start ()
    {
        vie = vieMax;
        tempsAttaque = 0;
        regardeDroite = true;
        shinibutaRigidbody = GetComponent<Rigidbody2D>();
        shinibutaAnimator = GetComponent<Animator>();
        zoneDegat.enabled = false;
    }
	
	void Update ()
    {
        Clavier();
	}

    void FixedUpdate()
    {

        if (vie > 0)
        {
            parTerre = EstParTerre();
            horizontal = Input.GetAxis("Horizontal");
            Mouvement(horizontal);
            Tourner();
            Layers();
            if (tempsAttaque > 0)
            {
                tempsAttaque -= Time.deltaTime;
            }
            else
            {
                zoneDegat.enabled = false;
                tempsAttaque = 0.9f;

            }
            Attaque();
            Rez();
        }
        else if(vie <= 0)
        {
            shinibutaRigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
            shinibutaAnimator.SetTrigger("Mort");
            StartCoroutine(Mort());
        }
        
    }

        private void Mouvement(float horizontal)
        {

            if (shinibutaRigidbody.velocity.y < 0)
            {
                shinibutaAnimator.SetBool("atteri", true);
            }

            if (parTerre)
            {
                shinibutaAnimator.SetBool("atteri", false);
                shinibutaAnimator.ResetTrigger("saut");
            }

            if (attaque == false && !this.shinibutaAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attaque"))
            {
                shinibutaRigidbody.velocity = new Vector2(horizontal * vitesseMouvement, shinibutaRigidbody.velocity.y);
                shinibutaAnimator.SetFloat("Vitesse", Mathf.Abs(shinibutaRigidbody.velocity.x));
            }
                
 
            if (saut && shinibutaRigidbody.velocity.y == 0 && parTerre)
            {
            shinibutaRigidbody.AddForce(new Vector2(0, vitesseSaut));
            shinibutaAnimator.SetTrigger("saut");
            }

        }

            private void Clavier()
            {
            
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    saut = true;
                }
                if (Input.GetKeyDown(KeyCode.U))
                {
                    attaque = true;
                }
            }

                private void Rez()
                {
                    saut = false;
                    attaque = false;
                }

                    private bool EstParTerre()
                    {
                        if (shinibutaRigidbody.velocity.y <= 0)
                        {
                            foreach (Transform point in groundPoints)
                            {
                                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, solRadiant, verifSol);
                                for (int i = 0; i < colliders.Length; i++)
                                {
                                    if (colliders[i].gameObject != gameObject)
                                    {

                                        return true; // si les 3 verificateurs on une collision autre que le joueur, retourne vrai

                                    }
                                }
                            }
                        }
                        return false;
                    }

                private void Tourner()
                {
                    if (shinibutaRigidbody.velocity.x > 0 && !regardeDroite || shinibutaRigidbody.velocity.x < 0 && regardeDroite)
                    {
                        regardeDroite = !regardeDroite;
                        Vector3 Changement = transform.localScale;
                        Changement.x *= -1;
                        transform.localScale = Changement;
                    }
                }

            private void Layers()
            {
                if (!parTerre)
                {
                    shinibutaAnimator.SetLayerWeight(1, 1);
                }
                else
                {
                    shinibutaAnimator.SetLayerWeight(1, 0);
                }
            }

        private void Attaque()
        {
            if (attaque && regardeDroite && !this.shinibutaAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attaque") && parTerre)
            {
                shinibutaAnimator.SetTrigger("attaque");
                zoneDegat.enabled = true;
                shinibutaRigidbody.velocity = Vector2.zero;
                shinibutaRigidbody.velocity = new Vector2(300*Time.deltaTime, shinibutaRigidbody.velocity.y);
            }

            if (attaque && !regardeDroite && !this.shinibutaAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attaque") && parTerre)
            {
                shinibutaAnimator.SetTrigger("attaque");
                zoneDegat.enabled = true;
                shinibutaRigidbody.velocity = Vector2.zero;
                shinibutaRigidbody.velocity = new Vector2(-300 * Time.deltaTime, shinibutaRigidbody.velocity.y);
            }
            
        }

        public IEnumerator Degats(int dgt)
        {
            int layerEnnemi = LayerMask.NameToLayer("Ennemi");
            int layerShinibuta = LayerMask.NameToLayer("Shinibuta");
            Physics2D.IgnoreLayerCollision(layerEnnemi, layerShinibuta);
            vie -= dgt;
            gameObject.GetComponent<Animation>().Play("Douleur");
            shinibutaRigidbody.velocity = (new Vector2(0,10));
            yield return new WaitForSeconds(3);
            Physics2D.IgnoreLayerCollision(layerEnnemi, layerShinibuta, false);
            yield return 0;
        }

    private IEnumerator Mort()
    {
        int layerEnnemi = LayerMask.NameToLayer("Ennemi");
        int layerPerso = LayerMask.NameToLayer("Shinibuta");
        Physics2D.IgnoreLayerCollision(layerEnnemi, layerPerso, false);
        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield return 0;
    }
}
