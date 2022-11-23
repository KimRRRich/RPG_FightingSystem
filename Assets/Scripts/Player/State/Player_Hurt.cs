using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hurt : Player_StateBase
{
    public void SetData(Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime)
    {
        player.RepelMove(sourceTransform, repelVelocity, repelTransitionTime);
    }
    public override void OnEnter()
    {
       
    }

    public override void OnExit()  { }

    public override void OnUpdate()
    {
    }
    
    
}
