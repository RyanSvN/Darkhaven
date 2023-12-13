using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{

    public int healthAmount = 30;
    
    public AudioClip drinkSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            //if audio not found, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = drinkSound;
        
    }

    void OnTriggerEnter(Collider other)
    {
        //if player goes over health potion collider, grab player health and add to it, and destroy health potion object
        if (other.CompareTag("MainPlayer"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.RegenerateHealth(healthAmount);
                Object.Destroy(gameObject, 1f);

                if (drinkSound != null && audioSource != null)
                {
                    audioSource.Play();
                    Debug.Log("Sound Played");
                }
            }
        }
    }
}
