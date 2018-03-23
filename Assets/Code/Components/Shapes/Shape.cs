using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

public class Shape : MonoBehaviour
{
    public int X
    {
        get
        {
            return Mathf.RoundToInt(transform.position.x);
        }
    }

    public int Y
    {
        get
        {
            return Mathf.RoundToInt(transform.position.y);
        }
    }

    public virtual void Clicked()
    {

    }

    public bool Contains(Vector2 mouse)
    {
        return mouse.x > X - 0.5f && mouse.x < X + 0.5f && mouse.y > Y - 0.5f && mouse.y < Y + 0.5f;
    }
}
