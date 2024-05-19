using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManagerScript : MonoBehaviour
{
    public PowerUpScript powerUpScript;
    public PlayerController playerController;
    public GunScript gunScript;

    public float duration = 20f;
    public float jumpTime;
    public float fireRateTime;
    public float jumpMultiplier;
    public float fireRateMultiplier;

    private float startJumpForce = 0f;
    private float boostedJumpForce = 0f;

    private float originalFireRate = 0f;
    private float boostedFireRate = 0f;

    public bool jumpPowerUp = false;
    public bool fireRatePowerUp = false;

    private void Start()
    {
        startJumpForce = playerController.jumpForce;
        boostedJumpForce = playerController.jumpForce * jumpMultiplier;

        originalFireRate = gunScript.fireRate;
        boostedFireRate = gunScript.fireRate / fireRateMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpPowerUp)
        {
            jumpTime += Time.deltaTime;
            playerController.jumpForce = boostedJumpForce;

            if (jumpTime > duration)
            {
                jumpPowerUp = false;
                playerController.jumpForce = startJumpForce;
                jumpTime = 0;
            }
        }

        if(fireRatePowerUp)
        {
            fireRateTime += Time.deltaTime;
            gunScript.fireRate = boostedFireRate;

            if(fireRateTime > duration)
            {
                fireRatePowerUp = false;
                gunScript.fireRate = originalFireRate;
                fireRateTime = 0;
            }
        }
    }
}

