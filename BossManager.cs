using System.Collections;
using UnityEngine;

public class BossManager : MonoBehaviour {
    private Transform transformBoss;
    
    
    

    [HideInInspector]
    public Collider2D colliderBoss;


    [HideInInspector]
   public bool regardeGauche;

    [SerializeField]
    private float tempsInitalDeBombardement;

    [SerializeField]
    private int compteur;

    private int vieMax;
    public int vie;
    public float tempsInitial;
    private float temps;
    public float vitesse;
    public float tempsEntreePhaseDeux;
    public float tempsAttente;
    private float tempsAttenteInitial;
    private float tempsBorbardement;
    private int choisir;
    private bool debutDeLaPhase;
    private bool MissileSpawn;
    private bool Mort;


    public GameObject porteSortie;
    private GameObject missileTeleguide;
    private GameObject petiteBombe;
    private GameObject explosionDegat;
    private Animator animBoss;
    public Transform[] capteurs;


    
	void Start () {
        //gameObject.SetActive(false);
        Mort = false;      
        tempsEntreePhaseDeux = 3;
        tempsAttenteInitial = 5;
        tempsAttente = tempsAttenteInitial;
        MissileSpawn = false;
        tempsBorbardement = tempsInitalDeBombardement;
        animBoss = GetComponent<Animator>();
        missileTeleguide =  Resources.Load("Explosion/" + "MissileTeleguide", typeof(GameObject)) as GameObject;
        petiteBombe = Resources.Load("Explosion/" + "PetiteBombe", typeof(GameObject)) as GameObject;
        explosionDegat = Resources.Load("Explosion/" + "Explosion", typeof(GameObject)) as GameObject;
        vieMax = 20;
        compteur = 2;
        vie = vieMax;
        temps = tempsInitial;
        transformBoss = GetComponent<Transform>();
        colliderBoss = GetComponent<Collider2D>();      
        debutDeLaPhase = true;
        regardeGauche = true;
        choisir = 0;

	}
	
	void Update () {
        if (vie <= 0)
        {
            MortBoss();
        }
        if (vie <= 10)
        {
            if (tempsEntreePhaseDeux <= 0)
            {
                
                PhaseDeux();
            }
            else
            {
                animBoss.SetTrigger("PhaseDeux");
                Instantiate(explosionDegat, transform.position - new Vector3(1, 0, 0), transform.rotation);
                Instantiate(explosionDegat, transform.position - new Vector3(-0.5f, 0, 0), transform.rotation);
                Instantiate(explosionDegat, transform.position - new Vector3(0.5f, 1, 0), transform.rotation);
                Instantiate(explosionDegat, transform.position - new Vector3(1, -1, 0), transform.rotation);
                
                tempsEntreePhaseDeux -= Time.deltaTime;
            }
        }
        if(vie > 10)
        {
            PhaseUne();
        }
        
        
    }

     private void PhaseUne()
    {

        TirMissile(2,1);

            if (compteur > 0)
            {
                TirMouvement(0.3f,20);
                ChangementCote(2);

            }
    }

     private void PhaseDeux()
    {
        TirMissile(3, 2);

        if (compteur > 0 && !Mort)
        {
            TirMouvement(0.16f, 32);    
            ChangementCote(7);
        }
    }

        public IEnumerator Degats(int degat)
        {
           // FindObjectOfType<AudioManager>().Arreter("BossDouleur");
            vie -= degat;
            animBoss.SetTrigger("Degat");
            yield return 0;
        }

            private void MortBoss()
            {
                if (tempsAttente <= 0)
                {
                    porteSortie.SetActive(true);
                    Destroy(this.gameObject);
                }
                else if (tempsAttente > 0)
                {
                    Mort = true;
                    transform.position = Vector2.MoveTowards(transform.position, capteurs[2].position, vitesse * Time.deltaTime);
                    Instantiate(explosionDegat, transform.position - new Vector3(1, 0, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(-0.5f, 0, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(0.5f, 1, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(1, -1, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(0, 0.5f, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(-0.5f, 1, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(0.5f, -1, 0), transform.rotation);
                    Instantiate(explosionDegat, transform.position - new Vector3(0.5f, -0.5f, 0), transform.rotation);
                    tempsAttente -= Time.deltaTime;
                }
            }

                private void TirMouvement(float tempsInitialBomb,float vitesse)
                {
                    if (debutDeLaPhase)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, capteurs[0].position, vitesse * Time.deltaTime);
                        if (tempsBorbardement <= 0)
                        {
                            tempsBorbardement = tempsInitialBomb;

                            Instantiate(petiteBombe, transform.position - new Vector3(0, 5, 0), transform.rotation);
                        }
                        else
                        {
                            tempsBorbardement -= Time.deltaTime;
                        }

                    }

                    if (!debutDeLaPhase)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, capteurs[choisir].position, vitesse * Time.deltaTime);
                        if (tempsBorbardement <= 0)
                        {
                            tempsBorbardement = tempsInitialBomb;

                            Instantiate(petiteBombe, transform.position - new Vector3(0, 5, 0), transform.rotation);
                        }
                        else
                        {
                            tempsBorbardement -= Time.deltaTime;
                        }
                    }


                }

            private void TirMissile(int verifCompteur, int nbMissile)
            {
                if (compteur <= 0 && MissileSpawn == false)
                {
                   
                        Instantiate(missileTeleguide, transform.position - new Vector3(0, 5, 0), transform.rotation);
                    
            
                    MissileSpawn = true;

                }

                if (tempsAttente <= 0 && MissileSpawn == true)
                {


                    tempsAttente = tempsAttenteInitial;
                    compteur = verifCompteur;
                    MissileSpawn = false;
                }
                else if (compteur == 0)
                {
                    tempsAttente -= Time.deltaTime;
                }
            }

        private void ChangementCote(float tempsInitial)
        {
            if (Vector2.Distance(transform.position, capteurs[0].position) < 0.2f)
            {
                if (temps <= 0)
                {
                    choisir = 1;
                    temps = tempsInitial;
                    debutDeLaPhase = false;
                }
                else
                {
                    temps -= Time.deltaTime;
                }
            }
            if (Vector2.Distance(transform.position, capteurs[1].position) < 0.2f)
            {
                if (temps <= 0)
                {
                    choisir = 0;
                    temps = tempsInitial;
                    debutDeLaPhase = false;
                    compteur -= 1;
                }
                else
                {
                    temps -= Time.deltaTime;
                }

            }
        }

}

