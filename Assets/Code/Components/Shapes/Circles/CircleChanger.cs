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

    protected override void Enter(Square square)
    {
        base.Enter(square);

        square.Direction = direction;
    }
}
