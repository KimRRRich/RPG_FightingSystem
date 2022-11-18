using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : Monster_StateBase
{
    public override void OnEnter()
    {
        //Debug.Log("Monster_Idle");
        //model.SetAnimation("跑",false);
        //monster.StopMove();
        
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //距离玩家少于一米就进攻
        //少于6米就追击
        float dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            //TODO:进入攻击
        }
        if (dis < 6)
        {
            //TODO:追踪玩家
            monster.ChangeState<Monster_Move>(MonsterState.Monster_Move);
        }
    }
}
