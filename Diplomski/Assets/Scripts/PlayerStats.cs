using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public float health = 100f;
    public GameObject died;
    public Slider slider;
    private Vector3 healthVec;
    public bool hasKey;
    public Animator animator;
    public bool isDead;
    public CapsuleCollider playerCollider;
    private Rigidbody rb;
    public PostProcessVolume volume;
    public AudioClip gunShot;
    private AudioSource audioS;

    void Start()
    {
        rb = GameObject.Find("Player").GetComponent<Rigidbody>();
        healthVec = new Vector3(health, 0, 0);
        slider.maxValue = health;
        slider.minValue = 0;
        slider.value = health;
        hasKey = false;
        isDead = false;

        audioS = gameObject.AddComponent<AudioSource>();
        audioS.playOnAwake = false;
        audioS.clip = gunShot;
        audioS.Stop();
    }
    void Update()
    {
        healthVec = Vector3.Lerp(healthVec, new Vector3(health, 0, 0), 7 * Time.deltaTime);
        slider.value = healthVec.x;
        if(isDead)
        {
            if(volume.profile.TryGetSettings(out ColorGrading colorGrading))
            {
                colorGrading.saturation.value -= 30 * Time.deltaTime;
                colorGrading.saturation.value = Mathf.Clamp(colorGrading.saturation.value, -100, 100);
            }
        }
    }

    public void DamagePlayer(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            audioS.Play();
            animator.applyRootMotion = true;
            rb.isKinematic = true;
            animator.SetTrigger("Dead");
            died.SetActive(true);
            isDead = true;
        } 
    }
}
