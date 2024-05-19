using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private float health = 100f;
    public GameObject died;
    public Slider slider;
    private Vector3 healthVec;

    void Start()
    {
        healthVec = new Vector3(health, 0, 0);
        slider.maxValue = health;
        slider.minValue = 0;
        slider.value = health;
    }
    void Update()
    {
        healthVec = Vector3.Lerp(healthVec, new Vector3(health, 0, 0), 7 * Time.deltaTime);
        slider.value = healthVec.x;
    }

    public void DamagePlayer(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            died.SetActive(true);
            Debug.Log("Player is dead!");
        } 
        
        else
        {
            Debug.Log("Player HP: " + health + "(taken " + damage + " damage)");
        }
    }
}
