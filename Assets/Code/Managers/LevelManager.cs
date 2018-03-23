using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static Action<Level> onLoad;
    public Level[] levels;

    private static LevelManager instance;

    public static int Level
    {
        get
        {
            if (!instance) instance = FindObjectOfType<LevelManager>();

            for (int i = 0; i < instance.levels.Length; i++)
            {
                if (instance.levels[i].Enabled) return instance.levels[i].level;
            }

            return -1;
        }
        set
        {
            if (!instance) instance = FindObjectOfType<LevelManager>();

            for (int i = 0; i < instance.levels.Length;i++)
            {
                if(instance.levels[i].level == value)
                {
                    instance.levels[i].Enable();
                    if (onLoad != null) onLoad.Invoke(instance.levels[i]);
                }
                else
                {
                    instance.levels[i].Disable();
                }
            }
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        instance = this;
    }

    public void Refresh()
    {
        levels = GameObject.Find("Levels").GetComponentsInChildren<Level>(true);
    }
}
