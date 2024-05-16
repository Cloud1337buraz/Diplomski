using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Assignables")]
    public LayerMask layerMask;
    public LayerMask doorMask;
    public GameObject muzzleFlash;
    public Transform muzzleFlashPosition;

    public AudioClip gunShot;
    private AudioSource audioS;

    [Header("Stats")]
    public float fireRate = 0.5f;

    public float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;

        audioS = gameObject.AddComponent<AudioSource>();
        audioS.playOnAwake = false;
        audioS.clip = gunShot;
        audioS.Stop();
    }

    public void MuzzleFlash()
    {
        GameObject Flash = Instantiate(muzzleFlash, muzzleFlashPosition);
        Destroy(Flash, 0.15f);
    }
    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float nextTimeToFire = fireRate / 1000;

        RaycastHit hit;
        Vector3 dir = transform.TransformDirection(Vector3.forward);
        Vector3 dir2 = new Vector3(dir.x, dir.y, 0);

        if (Input.GetMouseButtonDown(0))
        {
            if(time >= nextTimeToFire)
            {
                MuzzleFlash();
                //audioS.Play();
                if (Physics.Raycast(transform.position, Vector3.Normalize(dir2), out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.gameObject.GetComponent<EnemyHitScript>().Damage(25);
                }
                time = 0;
            }
        }
        if (Input.GetKey(KeyCode.E))
        {
            if (Physics.Raycast(transform.position, Vector3.Normalize(dir2), out hit, Mathf.Infinity, doorMask))
            {
                hit.transform.gameObject.GetComponent<doorscript>().OpenDoor();
            }
        }
        Debug.DrawRay(transform.position, Vector3.Normalize(dir2) * 1000, Color.red);
    }
}
