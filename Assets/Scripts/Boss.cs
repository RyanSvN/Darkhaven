using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    //player
    private Transform player;
    private PlayerController playerController;
    public PlayerHealth pHealth;

    //boss vars
    private float dist;
    public float moveSpeed;
    public float howClose;
    public float damage;
    public float cooldown = 5f; //seconds
    private float lastAttackedAt = -9999f; 

    public Animator Animator;

    BoxCollider boxCollider;

    private Rigidbody rb;

    //boss health
    public int maxBossHealth = 10;
    public int currentBossHealth;

    private bool isDead = false;

    public float knockbackForce = 5f;

    void Start()
    {
        currentBossHealth = maxBossHealth;

        rb = GetComponent<Rigidbody>();

        boxCollider = GetComponentInChildren<BoxCollider>();

        player = GameObject.FindGameObjectWithTag("MainPlayer").transform;
        playerController = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<PlayerController>();
        
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


        //if player is howClose to enemy, enemy aggro
        if(dist <= howClose)
        {
            GetComponent<Animator>().SetBool("Follow", true);
            Debug.Log("Player Found!");
            transform.LookAt(player);
            GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed);
            Move();
            //rb.velocity = (player.position - transform.position).normalized * moveSpeed;
        }

        //if player and enemy are right next to each other, enemy attacks, checks for player block
        if(dist <= 10f)
        {
            if (Time.time > lastAttackedAt + cooldown) 
            {
                GetComponent<Animator>().SetTrigger("Attack");
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
        currentBossHealth -= amount;
        Animator.SetTrigger("Hit");
        
        if (currentBossHealth <= 0)
        {
            Die();
        }
        else
        {
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
            return;
        }
     }

    void Move()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
}
