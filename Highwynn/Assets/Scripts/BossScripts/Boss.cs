using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{

    public int health;
    public int ModifyHealth;
    private float timeBtwDamage = 1.5f;
    public int damage;

    public Player Player;

    public Slider healthBar;
    private Animator anim;
    public bool isDead = false;

    public GameObject projectile;
    public Transform target;
    public Transform spawn;
    public float shootSpeed = 0.5f;
    public bool hasShot = false;

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

        if (health <= 0)
        {
            anim.SetTrigger("Dead");
            
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

        healthBar.value = health;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
        if (other.CompareTag("Player") && isDead == false)
        {
            if (timeBtwDamage <= 0)
            {
                Player.Update_Health(ModifyHealth);               
            }

        }
        if (other.tag == "Fire")
        {
            health = health - damage;
            Destroy(GameObject.FindWithTag("Fire"));
        }
    }

}
