using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    bool opening = false;
    static float degreesPerUpdate = 200f;
    private Transform player;
    public LayerMask doorMask;
    public float doorOpenDistance = 2f;
    private int doorId;
    int differenceInPosition;
    float currentAngle;
    private PlayerStats playerStatsScript;
    public bool needsAKey;
    private TMP_Text doorLocked;

    void RemoveText()
    {
        doorLocked.text = "";
    }
    void Start()
    {
        doorLocked = GameObject.Find("DoorLocked").GetComponent<TMP_Text>();
        player = GameObject.Find("Player").transform;
        playerStatsScript = player.gameObject.GetComponent<PlayerStats>();
        doorId = transform.GetChild(0).gameObject.GetInstanceID();
    }

    void Update()
    {
        Debug.DrawRay(player.position, Vector3.Normalize(player.right) * doorOpenDistance, Color.green);
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(player.position, Vector3.Normalize(player.right), out RaycastHit hit, doorOpenDistance, doorMask))
            {
                if(hit.transform.gameObject.GetInstanceID() == doorId)
                {
                    if(!needsAKey)
                    {
                        OpenDoor();
                    }
                    else if(needsAKey && playerStatsScript.hasKey)
                    {
                        OpenDoor();
                    }
                    else
                    {
                        doorLocked.text = "Door is locked";
                        Invoke("RemoveText", 1f);
                    }
                }
            }
        }

        if (opening)
        {
            if (Mathf.Abs(currentAngle) <= 90)
            {
                currentAngle += degreesPerUpdate * differenceInPosition * Time.deltaTime;
                transform.rotation = Quaternion.Euler(0, currentAngle, 0);
            }
            else opening = false;
        }
    }
    public void OpenDoor()
    {
            if (player.position.x < transform.position.x)
            {
                differenceInPosition = 1;
            }
            else
            {
                differenceInPosition = -1;
            }
        opening = true;
    }
}
