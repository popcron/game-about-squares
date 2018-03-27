using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleChanger : Circle
{
    public Direction direction = Direction.Down;

    private void OnDrawGizmos()
    {
        transform.name = "Changer";
        Transform triangle = transform.Find("Triangle");
        triangle.localEulerAngles = direction.ToEuler();
    }

    public override void Enter(Square square)
    {
        base.Enter(square);

        //when a square enters this circle, change direction
        square.Direction = direction;
    }
}
