using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMap : MonoBehaviour
{
    public static KeyMap instance;
    
    public KeyCode forward = KeyCode.E;
    public KeyCode left = KeyCode.S;
    public KeyCode backward = KeyCode.D;
    public KeyCode right = KeyCode.F;

    // singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

}
