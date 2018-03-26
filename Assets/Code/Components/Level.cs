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

    private void OnDrawGizmos()
    {
        transform.name = "Level" + level;
    }

    public void Enable()
    {
        enabled = true;
        gameObject.SetActive(true);

        if(Application.isPlaying)
        {
            var shapes = GetComponentsInChildren<Shape>(true);
            for (int i = 0; i < shapes.Length; i++)
            {
                shapes[i].Initialize();
            }
        }
    }

    public void SetOrigin()
    {
        var shapes = GetComponentsInChildren<Shape>(true);
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].SetOrigin();
        }
    }

    public void Disable()
    {
        enabled = false;
        gameObject.SetActive(false);
    }
}
