using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Attack : Monster_StateBase
{
    //ÊÇ·ñ¹¥»÷
    private bool isAttack;
    public override void OnEnter()
    {
        monster.StopMove();
        isAttack = false;
    }

    public override void OnExit()
    {
        
    }

    public override void OnUpdate()
    {
        if (isAttack == false)
        {
            isAttack = monster.Attack();
        }
    }
}
