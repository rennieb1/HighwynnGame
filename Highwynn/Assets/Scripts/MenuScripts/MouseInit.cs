using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInit : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture = null;
    [SerializeField]
    private CursorMode cursorMode = CursorMode.Auto;
    [SerializeField]
    private Vector2 hotspot = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
    }
}
