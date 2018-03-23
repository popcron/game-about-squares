using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    private void Awake()
    {
        Square.onMove += Refresh;   
    }

    private void Refresh(Square square)
    {
        //check if this square has entered this circle
        if (square.X == X && square.Y == Y)
        {
            Enter(square);
        }
    }

    protected virtual void Enter(Square square)
    {

    }
}
