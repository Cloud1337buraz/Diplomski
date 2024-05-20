using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Assignables")]
    public LayerMask doorMask;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;

    public AudioClip gunShot;
    private AudioSource audioS;

    private PlayerStats playerStatsScript;

    [Header("Stats")]
    public float fireRate;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerStatsScript = player.GetComponent<PlayerStats>();
        if (playerStatsScript == null)
        {
            playerStatsScript = new PlayerStats();
        }

        time = 0;

        audioS = gameObject.AddComponent<AudioSource>();
        audioS.playOnAwake = false;
        audioS.clip = gunShot;
        audioS.Stop();
    }

    public void MuzzleFlash()
    {
        GameObject Flash = Instantiate(muzzleFlash, muzzleFlashPosition);
        Destroy(Flash, fireRate * 0.0005f);
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float nextTimeToFire = fireRate / 1000;

        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        Vector3 dir2 = new Vector3(dir.x, dir.y, 0);

        if (Input.GetMouseButtonDown(0) && !playerStatsScript.isDead)
        {
            if(time >= nextTimeToFire)
            {
                MuzzleFlash();
                audioS.Play();
                if (Physics.Raycast(transform.position, Vector3.Normalize(dir2), out hit, Mathf.Infinity))
                {
                    if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        hit.transform.gameObject.GetComponent<EnemyHitScript>().Damage(25);
                    }
                }
                time = 0;
            }
        }
        Debug.DrawRay(transform.position, Vector3.Normalize(dir2) * 1000, Color.red);
    }
}
