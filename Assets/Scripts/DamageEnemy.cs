using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemy : MonoBehaviour
{

    public int damageAmount = 10;
    public int currentDamage;

    public float knockbackForce = 5f;

    //disable object after start of game variable
    public float disableTime = 1f;

    void Start()
    {
        currentDamage = damageAmount;

        //invoke disable object method using variable
        Invoke("DisableObject", disableTime);

    }
    
    public void IncreaseDamage(int amount)
    {
        currentDamage += amount;
    }

    //damage box collider on enter, damage enemy when enemy is inside
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy currentEnemyHealth = other.GetComponent<Enemy>();
            if (currentEnemyHealth != null)
            {
                currentEnemyHealth.TakeDamage(currentDamage);
                Debug.Log("Dealt damage to the enemy");
            }

            Boss currentBossHealth = other.GetComponent<Boss>();
            if (currentBossHealth != null)
            {
                currentBossHealth.TakeDamage(currentDamage);
                Debug.Log("Dealt damage to the Boss");
            }
        }
    }

    void DisableObject()
    {
        //disable the game object
        gameObject.SetActive(false);
    }
}
