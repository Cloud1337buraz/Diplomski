using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public EnemyIk IkScript;
    private PlayerStats playerStatsScript;
    public Transform muzzlePosition;
    public GameObject muzzleFlashPrefab;
    public float gunDamage;

    public float fireRateMiliseconds;
    private float timeAfterLastShoot;

    void Start()
    {
        timeAfterLastShoot = fireRateMiliseconds;

        GameObject player = GameObject.Find("Player");
        playerStatsScript = player.GetComponent<PlayerStats>();
        if(playerStatsScript == null)
        {
            playerStatsScript = new PlayerStats();
        }
    }
    void Update()
    {
        timeAfterLastShoot += Time.deltaTime;
        if(IkScript.playerInSight && timeAfterLastShoot >= fireRateMiliseconds/1000f)
        {
            FireAtPlayer();
        }
    }

    void FireAtPlayer()
    {
        timeAfterLastShoot = 0;
        playerStatsScript.DamagePlayer(gunDamage);
        MuzzleFlash();
    }

    void MuzzleFlash()
    {
        GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePosition);
        Destroy(flash, 0.15f);
    }
}
