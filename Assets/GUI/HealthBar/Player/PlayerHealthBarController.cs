using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarController : MonoBehaviour
{
    private float max_health;
    private float current_health;

    public GameObject player;

    public Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        max_health = player.GetComponent<PlayerController>().getMaxHealth();
        current_health = player.GetComponent<PlayerController>().getHealth();
        healthBar.maxValue = max_health;
        healthBar.value = current_health;
    }

    // Update is called once per frame
    void Update()
    {
        current_health = player.GetComponent<PlayerController>().getHealth();
        healthBar.value = current_health;
    }
}
