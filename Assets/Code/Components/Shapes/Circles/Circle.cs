using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public Square square;

    private void Awake()
    {
        //Square.onMove += Refresh;   
    }

    public virtual void Enter(Square square)
    {
        this.square = square;
        if(this.square)
        {
            this.square.circle = this;
        }

        Debug.Log(square + " entered " + this);
    }

    public virtual void Exit(Square square)
    {
        if(this.square)
        {
            this.square.circle = null;
        }

        this.square = null;
        Debug.Log(square + " exited " + this);
    }
}
