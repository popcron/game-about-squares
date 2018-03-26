using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : Shape
{
    public static Action<Square> onMove;

    public Circle circle;

    private Direction previousPositionDirection;
    private Direction previousFaceDirection;

    private Direction moveDirection;
    private float moveTime;
    private Vector2 moveStart;
    private bool move;

    [SerializeField]
    private Direction faceDirection = Direction.Up;
    [SerializeField]
    private SquareColor color = SquareColor.Red;

    private Transform triangle;
    private float triangleRotation;

    private SpriteRenderer indicator;
    private SpriteRenderer squareTop;
    private SpriteRenderer squareSide;

    private Vector2 originPosition;
    private SquareColor originColor;
    private Direction originFaceDirection;

    public Direction Direction
    {
        get
        {
            return faceDirection;
        }
        set
        {
            previousFaceDirection = faceDirection;
            faceDirection = value;
        }
    }

    public SquareColor Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
            Color colorValue = GameManager.ToColor(color);

            squareTop.color = GameManager.ToColor(color);
            squareSide.color = new Color(colorValue.r * 0.6f, colorValue.g * 0.6f, colorValue.b * 0.6f);

            indicator.color = GameManager.ToColor(SquareColor.Clear);
        }
    }

    public override void SetOrigin()
    {
        base.SetOrigin();

        originPosition = transform.position;
        originColor = color;
        originFaceDirection = faceDirection;
    }

    public override void Initialize()
    {
        base.Initialize();
        
        transform.position = originPosition;
        color = originColor;
        faceDirection = originFaceDirection;

        move = false;
    }

    private void Awake()
    {
        triangle = transform.Find("Triangle");
        squareTop = transform.Find("SquareTop").GetComponent<SpriteRenderer>();
        squareSide = transform.Find("SquareSide").GetComponent<SpriteRenderer>();
        indicator = transform.Find("Indicator").GetComponent<SpriteRenderer>();

        transform.name = "Square";
        Color = Color;
        Direction = Direction;
    }

    private void OnDrawGizmos()
    {
        Awake();
    }

    public override void Clicked()
    {
        base.Clicked();

        //push this square
        Push(Direction);

        //add to turn count
        GameManager.AddTurn();
    }

    private void PushNeighbours(Direction direction)
    {
        var squares = FindObjectsOfType<Square>();
        for (int i = 0; i < squares.Length; i++)
        {
            //this square is to the right, and our square is also moving right
            if(squares[i].X == X + 1 && squares[i].Y == Y && direction == Direction.Right)
            {
                squares[i].Push(direction);
            }
            //this square is to the left, and our square is also moving left
            if (squares[i].X == X - 1 && squares[i].Y == Y && direction == Direction.Left)
            {
                squares[i].Push(direction);
            }
            //this square is above, and our square is also moving up
            if (squares[i].X == X && squares[i].Y == Y + 1 && direction == Direction.Up)
            {
                squares[i].Push(direction);
            }
            //this square is below, and our square is also moving down
            if (squares[i].X == X && squares[i].Y == Y - 1 && direction == Direction.Down)
            {
                squares[i].Push(direction);
            }
        }
    }

    public void Move(Direction direction, bool record)
    {
        move = true;
        moveStart = transform.position;
        moveTime = 0f;
        moveDirection = direction;

        if (record)
        {
            GameManager.Record(this, moveDirection, Direction);
        }
    }

    private void Push(Direction direction)
    {
        //move to this direction
        Move(direction, true);

        //recursively push neighbours
        PushNeighbours(direction);
    }

    private void Update()
    {
        if(move)
        {
            //Vector2 position = Vector2.Lerp(moveStart, moveStart + moveDirection.ToVector(), moveTime);
            Vector2 position = Vector2.Lerp(transform.position, moveStart + moveDirection.ToVector(), Time.deltaTime * 10f);

            if (moveTime >= GameManager.TurnTime)
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

        if(circle && circle is CircleFinish)
        {
            Color color = GameManager.ToColor(((CircleFinish)circle).Color);
            indicator.color = new Color(color.r * 0.6f, color.g * 0.6f, color.b * 0.6f);
        }
        else
        {
            indicator.color = GameManager.ToColor(SquareColor.Clear);
        }
        

        //smoothly rotate the indicator direction
        triangleRotation = Mathf.Lerp(triangleRotation, faceDirection.ToEuler().z, Time.deltaTime * 8f);
        triangle.localEulerAngles = new Vector3(0, 0, triangleRotation);
    }
}
