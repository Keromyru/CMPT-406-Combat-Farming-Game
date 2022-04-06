using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

// Mace

public class PlayerHealthBarController : MonoBehaviour
{
    private float max_health;
    private float current_health;

    public PlayerController player;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        max_health = player.getMaxHealth();
        current_health = player.getHealth();
        healthBar.maxValue = max_health;
        healthBar.value = current_health;
    }

    void UpdateHealth()
    {
        current_health = player.getHealth();
        healthBar.value = current_health;
    }

    void Update()
    {
        if (current_health != player.getHealth()) {
            UpdateHealth();
        }
    }
}
