using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level = 0;
    private new bool enabled;

    public bool Enabled
    {
        get
        {
            return enabled;
        }
    }

    public void Enable()
    {
        enabled = true;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        enabled = false;
        gameObject.SetActive(false);
    }
}
