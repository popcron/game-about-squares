using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class EditorLevels : EditorWindow
{
    [MenuItem("Custom/Levels")]

    public static void Initialize()
    {
        GetWindow(typeof(EditorLevels));
    }

    private void OnGUI()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        if(levelManager)
        {
            levelManager.Refresh();

            for(int i = 0; i < levelManager.levels.Length;i++)
            {
                if (GUILayout.Button("Level " + levelManager.levels[i].level))
                {
                    LevelManager.Level = levelManager.levels[i].level;
                }
            }
        }
    }
}
