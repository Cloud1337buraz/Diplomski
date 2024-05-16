using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 angle;
    bool open = false;
    bool opening = false;
    bool didOpen = false;
    float x = 3f;
    float opened = 0f;
    public Transform pivotPoint;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (opening)
        {  
            angle = new Vector3(0, opened - x, 0);

            pivotPoint.localEulerAngles = angle;
            opened -= x;
            if (opened <= -90)
            {
                opening = false;
                didOpen = true;
            }
        }
    }
    public void OpenDoor()
    {
        if (!open && !didOpen)
        {
            opening = true;
        }
    }
}
