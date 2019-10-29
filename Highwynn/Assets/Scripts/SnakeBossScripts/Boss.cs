using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public int ModifyHealth;
    private float timeBtwDamage = 3.5f;
    public float damage;

    public Player Player;

    public Image healthBar;

    private Animator anim;
    public bool isDead = false;

    public GameObject projectile;

    public GameObject SnakeHealthBar;
    public Transform target;
    public Transform spawn;
    public float shootSpeed = 0.5f;
    public bool hasShot = false;
    public bool hasShot1 = false;
    public bool hasShot2 = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindObjectOfType<Player>();
    }

    private void Update()
    {

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpitFire")) {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.6f) {
                if (!hasShot) {
                    // Spit here
                    Debug.Log("Spit");
                    GameObject p = Instantiate(projectile, spawn.position, Quaternion.identity);
                    p.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((target.gameObject.transform.position - p.gameObject.transform.position)) * shootSpeed);
                    hasShot = true;
                }
            }
        }
        else {
            hasShot = false;
        }
        if (health <= 600)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpitFire"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.4f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.5f)
                {
                    if (!hasShot1)
                    {
                        // Spit here
                        Debug.Log("Spit");
                        GameObject p = Instantiate(projectile, spawn.position, Quaternion.identity);
                        p.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((target.gameObject.transform.position - p.gameObject.transform.position)) * shootSpeed);
                        hasShot1 = true;
                    }
                }
            }
            else
            {
                hasShot1 = false;
            }
        }
        if (health <= 300)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("SpitFire"))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.3f && anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.4f)
                {
                    if (!hasShot2)
                    {
                        // Spit here
                        Debug.Log("Spit");
                        GameObject p = Instantiate(projectile, spawn.position, Quaternion.identity); 
                        p.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize((target.gameObject.transform.position - p.gameObject.transform.position)) * shootSpeed);
                        hasShot2 = true;
                    }
                }
            }
            else
            {
                hasShot2 = false;
            }
        }

        if (health <= 0)
        {
            anim.SetTrigger("Dead");
        //    SnakeHealthBar.


        }
           if (isDead == true)
        {
          //  anim.enabled = false;
        }
        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0)
        {
            timeBtwDamage -= Time.deltaTime;
        }

        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                Player.Update_Health(ModifyHealth);
                timeBtwDamage = 3.5f;
            }

        }
        if (other.tag == "Fire")
        {
            health = health - damage;
            Destroy(GameObject.FindWithTag("Fire"));

                
            float calc_health = health / maxHealth;
            SetHealth(calc_health);
            
        }
    }
    void SetHealth(float myhealth)
    {
        healthBar.fillAmount = myhealth; // could be put in update mode
    } 


}
