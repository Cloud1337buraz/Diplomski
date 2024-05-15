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
        healthBar.localScale = Vector3.Lerp(healthBar.localScale, new Vector3(startScale * health / 100, healthBar.localScale.y, healthBar.localScale.z), 7 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        healthBar.LookAt(Vector3.up);
        healthBar.rotation = Quaternion.Euler(healthBar.rotation.x, 0, healthBar.rotation.z);
    }
    public void Damage(float damageTaken)
    {
        health -= damageTaken;
         
        if (health <= 0)
        {
            Destroy(transform.gameObject);
        }
    }
}
