using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    Vector3 angle;
    bool open = false;
    bool openingRight = false;
    bool openingLeft = false;
    bool didOpen = false;
    float x = 3f;
    float opened = 0f;
    private Transform player;
    public LayerMask doorMask;
    public float doorOpenDistance = 2f;
    private int doorId;

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

        if (openingRight)
        {
            angle = new Vector3(0, opened + x, 0);

            transform.localEulerAngles = angle;
            opened += x;
            if (opened >= 90)
            {
                openingRight = false;
                didOpen = true;
            }
        }
        else if(openingLeft)
        {
            angle = new Vector3(0, opened - x, 0);

            transform.localEulerAngles = angle;
            opened -= x;
            if (opened <= -90)
            {
                openingLeft = false;
                didOpen = true;
            }
        }
    }
    public void OpenDoor()
    {
        if (!open && !didOpen)
        {
        if (player.position.x < transform.position.x)
            openingRight = true;
        else
            openingLeft = true;
        }
    }
}
