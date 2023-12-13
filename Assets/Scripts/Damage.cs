using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public PlayerHealth pHealth;
    public float damage;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("MainPlayer"))
        {
            pHealth.playerHealth -= damage;
        }
    }
}
