using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class Extensions
{
    public static Vector2 ToVector(this Direction direction)
    {
        if (direction == Direction.Left) return Vector2.left;
        else if (direction == Direction.Right) return Vector2.right;
        else if (direction == Direction.Up) return Vector2.up;
        else if (direction == Direction.Down) return Vector2.down;

        return Vector2.zero;
    }

    public static Vector3 ToEuler(this Direction direction)
    {
        if (direction == Direction.Left) return new Vector3(0, 0, 90);
        else if (direction == Direction.Right) return new Vector3(0, 0, -90);
        else if (direction == Direction.Up) return new Vector3(0, 0, 0);
        else if (direction == Direction.Down) return new Vector3(0, 0, 180);
        
        return Vector3.zero;
    }
}
