using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : Shape
{
    public Square square;

    public virtual void Enter(Square square)
    {
        //occurs when a square enters this circle

        this.square = square;
        if(this.square)
        {
            this.square.circle = this;
        }

        //Debug.Log(square + " entered " + this);
    }

    public virtual void Exit(Square square)
    {
        //occurs when a square exits this circle

        if(this.square)
        {
            this.square.circle = null;
        }

        this.square = null;
        //Debug.Log(square + " exited " + this);
    }
}
