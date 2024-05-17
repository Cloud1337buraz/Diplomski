using System.Collections;
using System.Collections.Generic;
using System.Data;
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

    void Start()
    {
        player = GameObject.Find("Player").transform;
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
                    OpenDoor();
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
