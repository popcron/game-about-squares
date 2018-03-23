using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleFinish : Circle
{
    private void OnDrawGizmos()
    {
        transform.name = "Finish";    
    }

    protected override void Enter(Square square)
    {
        base.Enter(square);

        LevelManager.Level++;
    }
}