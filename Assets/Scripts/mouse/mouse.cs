using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouse : MonoBehaviour
{
    public Texture2D mira;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(mira, Vector2.zero, CursorMode.ForceSoftware);
        transform.position = Input.mousePosition;
    }
}
