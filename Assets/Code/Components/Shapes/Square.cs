using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    public static Action<Square> onMove;

    public float moveDuration = 1f;
    private float moveTime;
    private Vector2 moveStart;
    private bool move;

    [SerializeField]
    private Direction direction = Direction.Up;
    private Transform triangle;

    public Direction Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
            triangle.localEulerAngles = direction.ToEuler();
        }
    }

    private void Awake()
    {
        triangle = transform.Find("Triangle");
    }

    private void OnDrawGizmos()
    {
        transform.name = "Square";
    }

    public override void Clicked()
    {
        base.Clicked();
        
        move = true;
        moveStart = transform.position;
        moveTime = 0f;
    }

    private void Update()
    {
        if(move)
        {
            float lerp = moveTime / moveDuration;

            Vector2 position = Vector2.Lerp(moveStart, moveStart + Direction.ToVector(), lerp);

            if(lerp >= 1f)
            {
                move = false;

                //round positions
                position.x = Mathf.RoundToInt(position.x);
                position.y = Mathf.RoundToInt(position.y);

                if (onMove != null) onMove.Invoke(this);
            }

            transform.position = position;
            moveTime += Time.deltaTime;
        }
    }
}
