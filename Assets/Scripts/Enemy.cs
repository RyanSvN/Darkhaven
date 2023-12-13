using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    private PlayerController playerController;

    public PlayerHealth pHealth;
    public float damage;

    public Animator Animator;

    public float cooldown = 2f; //seconds
    private float lastAttackedAt = -9999f;  

    BoxCollider boxCollider;
    
    //enemy Health
    public int maxEnemyHealth = 100;
    public int currentEnemyHealth;

    private float dist;
    public float moveSpeed;
    public float howClose;

    private bool isDead = false;

    private Patroller patrollerScript;

    private Rigidbody rb;
    
    public float knockbackForce = 5f;

    //audio
    public AudioClip zombieDieSound;
    private AudioSource zombieDieSource;

    public AudioClip zombieHitSound;
    private AudioSource zombieHitSource;

    public AudioClip zombieAttackSound;
    private AudioSource zombieAttackSource;
    
    void Start()
    {
        currentEnemyHealth = maxEnemyHealth;

        player = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        playerController = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<PlayerController>();

        boxCollider = GetComponentInChildren<BoxCollider>();
        //boxCollider.enabled = true;
        patrollerScript = GetComponent<Patroller>();
        rb = GetComponent<Rigidbody>();


        //audio
        zombieDieSource = GetComponent<AudioSource>();
        if (zombieDieSource == null)
        {
            zombieDieSource = gameObject.AddComponent<AudioSource>();
        }
        zombieDieSource.clip = zombieDieSound;


        zombieHitSource = GetComponent<AudioSource>();
        if (zombieHitSource == null)
        {
            zombieHitSource = gameObject.AddComponent<AudioSource>();
        }
        zombieHitSource.clip = zombieHitSound;
        

        zombieAttackSource = GetComponent<AudioSource>();
        if (zombieAttackSource == null)
        {
            zombieAttackSource = gameObject.AddComponent<AudioSource>();
        }
        zombieAttackSource.clip = zombieAttackSound;
    }

    void Update()
    {
        if (isDead)
        {
            return;
        }
        
        //tracking the distance from the enemy to the player
        dist = Vector3.Distance(player.position, transform.position);
        //Debug.Log("Distance to player: " + dist);

        //if player is howClose to enemy, enemy aggros
        if(dist <= howClose)
        {
            Debug.Log("Player Found!");
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
            //rb.velocity = (player.position - transform.position).normalized * moveSpeed;
        }

        //if player and enemy are right next to each other, enemy attacks, checks for player block
        if(dist <= 1f)
        {
            if (Time.time > lastAttackedAt + cooldown) 
            {
                GetComponent<Animator>().SetTrigger("Attack");
                ZombieAttackSound();
                lastAttackedAt = Time.time;

                if (!playerController.isBlocking)
                {
                    pHealth.playerHealth -= damage;
                    Debug.Log("Player Hit!");
                }

                else if (playerController.isBlocking)
                {
                    Debug.Log("Player Blocking!");
                    return;
                }
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentEnemyHealth -= amount;
        Animator.SetTrigger("Hit");
        Debug.Log("HIT");
        
        if (currentEnemyHealth <= 0)
        {
            Die();
            ZombieDieSound();
        }
        //zombie hit and knockback
        else
        {
            ZombieHitSound();
            Debug.Log("Knockback!");
            Vector3 knockbackDirection = (transform.position - player.position).normalized;
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    void Die()
    {
        if (!isDead) 
        {
            isDead = true;
            Animator.SetBool("IsDead", true);
            
            //disable RB
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            if (patrollerScript != null)
            {
                patrollerScript.enabled = false;
            }

            return;
        }
        
    }

    //audio

    private void ZombieDieSound()
    {
        if (zombieDieSound != null && zombieDieSource != null)
        {
            zombieDieSource.PlayOneShot(zombieDieSound);
        }
    }

    private void ZombieHitSound()
    {
        if (zombieHitSound != null && zombieHitSource != null)
        {
            zombieHitSource.PlayOneShot(zombieHitSound);
        }
    }

    private void ZombieAttackSound()
    {
        if (zombieAttackSound != null && zombieAttackSource != null)
        {
            zombieAttackSource.PlayOneShot(zombieAttackSound);
        }
    }
}
