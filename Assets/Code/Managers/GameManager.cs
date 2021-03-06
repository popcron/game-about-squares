﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SquareColor
{
    Red,
    Green,
    Blue,
    Clear
}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}

//stores turn information
[Serializable]
public class GameSnapshot
{
    public int turn;
    public Square square;
    public Direction moveDirection;
    public Direction faceDirection;
}

public class GameManager : MonoBehaviour
{
    public const float TurnTime = 0.35f;

    private static GameManager instance;

    private Shape[] shapes;
    public List<GameSnapshot> snapshots = new List<GameSnapshot>();
    public int turn = 0;
    public Color red = Color.red;
    public Color blue = Color.blue;
    public Color green = Color.green;

    //this is true when a square is lerping
    private bool waiting;

    private void Awake()
    {
        instance = this;

        //load level 1
        LevelManager.Level = 1;
        shapes = FindObjectsOfType<Shape>();
    }

    private void Refresh(Level level)
    {
        //clear the snapshot history when a new level loads
        snapshots.Clear();

        //get akk shapes after a level loads
        shapes = FindObjectsOfType<Shape>();
    }

    private void OnEnable()
    {
        instance = this;
        LevelManager.onLoad += Refresh;
    }

    private void OnDisable()
    {
        LevelManager.onLoad -= Refresh;
    }

    private void Update()
    {
        CheckForMouse();
        CheckForWin();

        //undo if this isnt the first turn, and if a turn isnt currently taking action
        if(Input.GetKeyDown(KeyCode.Space) && turn > 0 && !waiting)
        {
            Undo();
        }
    }

    private static void OverlapCheck()
    {
        if (!instance) instance = FindObjectOfType<GameManager>();

        //process circle overlaps for every square
        //after a turn is made
        for (int i = 0; i < instance.shapes.Length; i++)
        {
            if (instance.shapes[i] is Circle)
            {
                Circle circle = instance.shapes[i] as Circle;
                Square oldSquare = circle.square;
                Square newSquare = null;

                for (int s = 0; s < instance.shapes.Length; s++)
                {
                    if (instance.shapes[s] is Square)
                    {
                        if(instance.shapes[s].X == circle.X && instance.shapes[s].Y == circle.Y)
                        {
                            newSquare = instance.shapes[s] as Square;
                            //exit out of this for loop
                            break;
                        }
                    }
                }

                //overlap information changed
                if(oldSquare != newSquare)
                {
                    if (oldSquare) circle.Exit(oldSquare);
                    if (newSquare) circle.Enter(newSquare);
                }
            }
        }
    }

    public static void Undo()
    {
        if (!instance) instance = FindObjectOfType<GameManager>();

        List<GameSnapshot> snapshots = new List<GameSnapshot>();
        //loop through all snapshots, and perform the opposite move
        for (int i = 0; i < instance.snapshots.Count; i++)
        {
            if(instance.snapshots[i].turn == instance.turn - 1)
            {
                //previous snapshot
                //move this square, in the opposite direction
                if (instance.snapshots[i].moveDirection == Direction.Left) instance.snapshots[i].square.Move(Direction.Right, false);
                else if (instance.snapshots[i].moveDirection == Direction.Right) instance.snapshots[i].square.Move(Direction.Left, false);
                else if (instance.snapshots[i].moveDirection == Direction.Up) instance.snapshots[i].square.Move(Direction.Down, false);
                else if (instance.snapshots[i].moveDirection == Direction.Down) instance.snapshots[i].square.Move(Direction.Up, false);

                //set the face direction
                instance.snapshots[i].square.Direction = instance.snapshots[i].faceDirection;

                snapshots.Add(instance.snapshots[i]);
            }
        }

        //remove the snapshots that were previously executed
        for (int i = 0; i < snapshots.Count; i++)
        {
            instance.snapshots.Remove(snapshots[i]);
        }

        //go back a turn
        instance.turn--;

        //overlap check
        instance.StartCoroutine(instance.WaitForOverlaps());
    }

    public static void AddTurn()
    {
        if (!instance) instance = FindObjectOfType<GameManager>();

        instance.turn++;

        //overlap check
        instance.StartCoroutine(instance.WaitForOverlaps());
    }

    private IEnumerator WaitForOverlaps()
    {
        //wait for turn time, and then do overlap checks then

        waiting = true;
        yield return new WaitForSeconds(TurnTime);

        OverlapCheck();
        waiting = false;
    }

    public static void Record(Square square, Direction moveDirection, Direction faceDirection)
    {
        if (!instance) instance = FindObjectOfType<GameManager>();

        //if a square isnt already recorded, then add a new item to the history
        GameSnapshot snapshot = new GameSnapshot
        {
            square = square,
            turn = instance.turn,
            moveDirection = moveDirection,
            faceDirection = faceDirection
        };

        //add this information
        instance.snapshots.Add(snapshot);
    }

    private void CheckForWin()
    {
        //check if all circles in the level, have a square of the same color overlapping them
        //if there is a mismatch in any of the circles
        //then it means that the win state hasnt reached, therefore, exit this method

        bool win = true;
        for (int i = 0; i < shapes.Length; i++)
        {
            if(shapes[i] is CircleFinish && shapes[i].transform.parent.gameObject.activeSelf)
            {
                CircleFinish circle = shapes[i] as CircleFinish;
                if(!circle.square)
                {
                    win = false;
                    break;
                }
                else if (circle.square && circle.square.Color != circle.Color)
                {
                    //mismatch
                    win = false;
                    break;
                }
            }
        }

        //no color mismatches found, proceed to next level
        if(win)
        {
            LevelManager.Level++;
        }
    }

    private void CheckForMouse()
    {
        //dont check for mouse if waiting for turn
        if (waiting) return;

        //loop through every shape,
        //and check if a mouse overlaps with the shape
        //if so, then check for mouse down events

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

    public static Color ToColor(SquareColor value)
    {
        //converts enum SquareColor to a game manager color value

        if (!instance) instance = FindObjectOfType<GameManager>();

        if (value == SquareColor.Red) return instance.red;
        if (value == SquareColor.Blue) return instance.blue;
        if (value == SquareColor.Green) return instance.green;

        return Color.clear;
    }
}
