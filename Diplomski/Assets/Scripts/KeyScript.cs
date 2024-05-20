using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyScript : MonoBehaviour
{
    private PlayerStats playerStatsScript;
    // Start is called before the first frame update

    private void Start()
    {
        GameObject player = GameObject.Find("Player");
        playerStatsScript = player.GetComponent<PlayerStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        playerStatsScript.hasKey = true;
        GameObject.Destroy(gameObject);
    }
}
