using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Bombe : MonoBehaviour {

    
    private Shinibuta personnage;
    private Transform cible;
    private Transform boss;
    private Rigidbody2D rbMissile;
    public GameObject explosion;
    private Collider2D collMissile;

    private BossManager bossMan;
    public float vitesse;
    public float range;
    private float vitesseTourner;
    private string nomAnim = "Explosion";
    private bool vaVersCible;
    private bool vaVersBoss;
    
	void Start () {
        vaVersCible = true;
        explosion = Resources.Load("Explosion/" + nomAnim, typeof(GameObject)) as GameObject;
        bossMan = FindObjectOfType<BossManager>();
        vitesseTourner = 600f;
        personnage = FindObjectOfType<Shinibuta>();
        boss = cible = GameObject.FindGameObjectWithTag("Boss").transform;
        cible = GameObject.FindGameObjectWithTag("Player").transform;
        rbMissile = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (vaVersCible)
        {
            MissileGuide();
        }
        else if(vaVersBoss)
        {
            RetourAuBoss();
        }

	}
    private void OnTriggerEnter2D(Collider2D collMissile)
    {
        if (collMissile.transform.tag == "Attaque")
        {
            vaVersCible = false;
            vaVersBoss = true;
        }
        else if(collMissile.CompareTag("Player"))
        {
            
           
            StartCoroutine(personnage.Degats(2));
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);

        }
        else if(collMissile.transform.tag == "Boss")
        {
           StartCoroutine(bossMan.Degats(5));
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
       

    }
    private void MissileGuide()
    {
        Vector2 direction = (Vector2)cible.position - rbMissile.position;
        direction.Normalize();
        float tourner = Vector3.Cross(direction, (-transform.up)).z;
        rbMissile.angularVelocity = -tourner * vitesseTourner;
        rbMissile.velocity = (-transform.up * vitesse);
    }
    public void RetourAuBoss()
    {
        
        Vector2 direction = (Vector2)boss.position - rbMissile.position;
        direction.Normalize();
        float tourner = Vector3.Cross(direction, (-transform.up)).z;
        rbMissile.angularVelocity = -tourner * vitesseTourner*7;
        rbMissile.velocity = (-transform.up * vitesse);
    }
   

}
