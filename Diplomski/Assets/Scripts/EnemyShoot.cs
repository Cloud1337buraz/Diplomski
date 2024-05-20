using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public EnemyIk IkScript;
    private PlayerStats playerStatsScript;
    public Transform muzzlePosition;
    public GameObject muzzleFlashPrefab;

    public AudioClip gunShot;
    private AudioSource audioS;

    private static float fireRateMiliseconds = 800f;
    private float timeAfterLastShoot;

    private TMP_Text enemyMiss;

    public float gunDamage;

    void Start()
    {
        enemyMiss = GameObject.Find("EnemyMissed").GetComponent<TMP_Text>();
        enemyMiss.text = "";

        audioS = gameObject.AddComponent<AudioSource>();
        audioS.playOnAwake = false;
        audioS.clip = gunShot;
        audioS.Stop();

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
        if(IkScript.playerInSight && timeAfterLastShoot >= fireRateMiliseconds/1000f && playerStatsScript.health > 0)
        {
            FireAtPlayer();
        }
    }

    void RemoveText()
    {
        enemyMiss.text = "";
    }
    void FireAtPlayer()
    {
        timeAfterLastShoot = 0;
        float random = Random.value;
        if(random > 0.2f)
        {
            playerStatsScript.DamagePlayer(gunDamage);
        }
        else
        {
            enemyMiss.text = "Enemy missed";
            Invoke("RemoveText", 0.4f);
        }
        MuzzleFlash();
        audioS.Play();
    }

    void MuzzleFlash()
    {
        GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePosition);
        Destroy(flash, 0.15f);
    }
}
