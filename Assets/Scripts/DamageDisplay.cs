using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class DamageDisplay : MonoBehaviour
{
    public TextMeshProUGUI damageText;
    public DamageEnemy damageEnemy; // Reference to the script handling player health

    void Start()
    {
        damageEnemy = FindObjectOfType<DamageEnemy>();
        if (damageEnemy == null)
        {
            Debug.LogError("DamageEnemy script not found in the scene.");
        }
        damageText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (damageEnemy != null) 
        {
            int currentDamage = damageEnemy.currentDamage;
            UpdateDamageText(currentDamage);
        }
    }

    void UpdateDamageText(int damageValue)
    {
        // Update the text component with the current damage value
        damageText.text = "Player Damage: " + damageValue.ToString();
    }
}