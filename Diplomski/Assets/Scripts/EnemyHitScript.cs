using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitScript : MonoBehaviour
{
    public float health = 100f;
    public Transform healthBar;
    private float startScale;

    // Start is called before the first frame update
    void Start()
    {
        startScale = healthBar.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damageTaken)
    {
        health -= damageTaken;
        healthBar.localScale = new Vector3(startScale * health / 100, healthBar.localScale.y, healthBar.localScale.z); 
        if (health <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
