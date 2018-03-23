using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Shape[] shapes;
    private static GameManager instance;

    private void Awake()
    {
        instance = this;
        LevelManager.onLoad += Refresh;
        LevelManager.Level = 1;
    }

    private void Refresh(Level level)
    {
        shapes = FindObjectsOfType<Shape>();
    }

    private void OnEnable()
    {
        instance = this;
    }

    private void Update()
    {
        CheckForMouse();
    }

    private void CheckForMouse()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        for (int i = 0; i < shapes.Length; i++)
        {
            if (shapes[i].Contains(mouse))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    shapes[i].Clicked();
                }
            }
        }
    }
}
