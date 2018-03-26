using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFinish : Circle
{
    [SerializeField]
    private SquareColor color;

    public SquareColor Color
    {
        get
        {
            return color;
        }
        set
        {
            color = value;
            circle.color = GameManager.ToColor(color);
        }
    }

    private SpriteRenderer circle;

    private void Awake()
    {
        circle = transform.Find("Circle").GetComponent<SpriteRenderer>();
        transform.name = "Finish";
    }

    private void OnDrawGizmos()
    {
        Awake();
        Color = Color;
    }
}