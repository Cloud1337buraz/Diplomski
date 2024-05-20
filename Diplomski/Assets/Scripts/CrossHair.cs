using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public Texture2D cursorTex;
    private Vector2 cursorSpot;
    void Start()
    {
        cursorSpot = new Vector2(cursorTex.height / 2, cursorTex.width / 2);
        Cursor.SetCursor(cursorTex, cursorSpot, CursorMode.Auto);
    }

}
