using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFinish : Circle
{
    [SerializeField]
    private SquareColor color;
    private SpriteRenderer circle;

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

    private void Awake()
    {
        circle = transform.Find("Circle").GetComponent<SpriteRenderer>();
        transform.name = "Finish";
        Color = Color;
    }

    private void OnDrawGizmos()
    {
        Awake();
    }
}