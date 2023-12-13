using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour

{
    //Health
    public float playerHealth;
    public float playerMaxHealth;

    //Lives
    public int maxLives = 3;
    public int currentLives;
    public Image[] heartIcons;

    
    public Image healthBar;

    public Transform respawnPoint;

    public GameObject GameOverCanvas;

    void Start()
    {
        playerHealth = playerMaxHealth;
        currentLives = maxLives;

        if (GameOverCanvas != null)
        {
            GameOverCanvas.SetActive(false);
        }

        UpdateLivesUI();
    }

    void Update()
    {
        healthBar.fillAmount = Mathf.Clamp(playerHealth / playerMaxHealth, 0, 1);

        if (playerHealth <= 0)
        {
            Die();
        }
    }

        public void RegenerateHealth(int amount)
    {
        playerHealth = Mathf.Clamp(playerHealth + amount, 0, playerMaxHealth);
        // Implement any UI update or other logic related to player health
    }

    void Die() 
    {
        //deduct lives and end game
        currentLives--;
        UpdateLivesUI();

        if (currentLives > 0)
        {
            //respawn and regain health
            transform.position = respawnPoint.position;
            playerHealth = playerMaxHealth;
            UpdateLivesUI();
        }
        else
        {
            GameOver();
        }

    }

    void GameOver()
    {
        //Debug.Log("Game Over");

        if (GameOverCanvas != null)
        {
            GameOverCanvas.SetActive(true);
        }
    }

    void UpdateLivesUI()
    {
        // Update the heart icons based on the current lives count
        for (int i = 0; i < heartIcons.Length; i++)
        {
            if (i < currentLives)
            {
                // Enable the heart icon
                heartIcons[i].enabled = true;
            }
            else
            {
                // Disable the heart icon
                heartIcons[i].enabled = false;
            }
        }
    }


    
}
