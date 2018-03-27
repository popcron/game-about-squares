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
        //set the name of the level to whatever number it is
        //easier to read when editing in editor
        transform.name = "Level" + level;
    }

    public void Enable()
    {
        //enable the object
        enabled = true;
        gameObject.SetActive(true);

        if(Application.isPlaying)
        {
            //set positions of children to their defaults
            //only if playing, otherwise this gets called when switching from editor

            var shapes = GetComponentsInChildren<Shape>(true);
            for (int i = 0; i < shapes.Length; i++)
            {
                shapes[i].Initialize();
            }
        }
    }

    public void SetOrigin()
    {
        //ask children to set their defaults
        var shapes = GetComponentsInChildren<Shape>(true);
        for (int i = 0; i < shapes.Length; i++)
        {
            shapes[i].SetOrigin();
        }
    }

    public void Disable()
    {
        //disable the object
        enabled = false;
        gameObject.SetActive(false);
    }
}
