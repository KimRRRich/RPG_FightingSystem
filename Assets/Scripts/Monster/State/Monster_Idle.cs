using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : StateBase<MonsterState>
{
    public override void OnEnter()
    {
        Debug.Log("Monster_Idle");
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        throw new System.NotImplementedException();
    }
}
