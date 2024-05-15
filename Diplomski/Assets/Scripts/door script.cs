using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorscript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 angle;
    bool closed = true;
    int x = -90;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (closed)
        {
            closed = false;   
            angle = new Vector3(x, 0, 0);

            transform.localEulerAngles = angle;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z * 2);
        }
    }
    public void OpenDoor()
    {
        if (closed)
        {
            closed = false;
        }
    }
}
