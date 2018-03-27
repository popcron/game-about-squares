using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    //round x position
    public int X
    {
        get
        {
            return Mathf.RoundToInt(transform.position.x);
        }
    }

    //rounded y position
    public int Y
    {
        get
        {
            return Mathf.RoundToInt(transform.position.y);
        }
    }

    public virtual void Initialize()
    {

    }

    public virtual void SetOrigin()
    {

    }

    public virtual void Clicked()
    {

    }

    //checks if a position in world space enters this shape in a 1, 1 square size
    public bool Contains(Vector2 position)
    {
        return position.x > X - 0.5f && position.x < X + 0.5f && position.y > Y - 0.5f && position.y < Y + 0.5f;
    }
}
