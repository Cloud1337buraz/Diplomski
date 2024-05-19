using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    private PowerUpManagerScript powerUpManager;

    private void Start()
    {
        powerUpManager = GameObject.Find("PowerUps").GetComponent<PowerUpManagerScript>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.tag == "JumpPowerUp")
        {
            powerUpManager.jumpPowerUp = true;
            GameObject.Destroy(gameObject);

            if (powerUpManager.jumpTime > 0)
            {
                powerUpManager.jumpTime = 0;    
            }
        }

        if (gameObject.tag == "FireRatePowerUp")
        {
            powerUpManager.fireRatePowerUp = true;
            GameObject.Destroy(gameObject);

            if (powerUpManager.fireRateTime > 0)
            {
                powerUpManager.fireRateTime = 0;
            }
        }
    }
}
