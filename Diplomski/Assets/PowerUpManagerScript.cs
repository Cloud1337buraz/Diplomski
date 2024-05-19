using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManagerScript : MonoBehaviour
{
    public PowerUpScript powerUpScript;
    public PlayerController playerController;

    public float duration = 20f;
    public float jumpTime;
    public float jumpMultiplier;

    private float startJumpForce = 0f;
    private float boostedJumpForce = 0f;

    public bool jumpPowerUp = false;

    private void Start()
    {
        startJumpForce = playerController.jumpForce;
        boostedJumpForce = playerController.jumpForce * jumpMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
            if (jumpPowerUp)
            {
                jumpTime += Time.deltaTime;
                playerController.jumpForce = boostedJumpForce;
            }

            if (jumpTime > duration)
            {
                jumpPowerUp = false;
                playerController.jumpForce = startJumpForce;
                jumpTime = 0;
            }
        }
    }

