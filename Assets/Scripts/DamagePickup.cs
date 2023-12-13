using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePickup : MonoBehaviour
{
    public int damageIncrease = 10;

    //audio
    public AudioClip smashSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            //find audio if not found
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = smashSound;
    }

    void OnTriggerEnter(Collider other)
    {
        //check if the player has entered the trigger zone
        if (other.CompareTag("DamageBox"))
        {
            //increase player damage
            DamageEnemy playerDamage = other.GetComponent<DamageEnemy>();
            if (playerDamage != null)
            {
                playerDamage.IncreaseDamage(damageIncrease);
                Object.Destroy(gameObject, 1f);

                if (smashSound != null && audioSource != null)
                {
                    audioSource.Play();
                    Debug.Log("Sound Played");
                    
                }

                // Destroy the pickup after it's collected
                //Destroy(gameObject);
            }
             else
            {
                Debug.LogError("PlayerDamage script not found on the player!");
            }
        }
        else
        {
            Debug.LogError("Trigger entered, but the collider doesn't have the 'Player' tag.");
        }
    }
}
