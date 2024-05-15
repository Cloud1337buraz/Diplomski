using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float health = 100f;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DamagePlayer(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Player is dead!");
        } else
        {
            Debug.Log("Player HP: " + health + "(taken " + damage + " damage)");
        }
    }
}
