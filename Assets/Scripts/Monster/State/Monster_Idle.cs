using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : Monster_StateBase
{
    public override void OnEnter(){
        if (monster.isDead) return;
        model.PlayAnimation("待机");
    }

    public override void OnExit() { }

    public override void OnUpdate()
    {
        if (monster.isDead||player.isDead) return;
        //距离玩家少于一米就进攻
        //少于6米就追击
        float dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            monster.transform.LookAt(new Vector3(player.transform.position.x, monster.transform.position.y, player.transform.position.z));
            monster.ChangeState<Monster_Attack>(MonsterState.Monster_Attack);
            return;
        }
        else if (dis < 6)
        {
            //TODO:追踪玩家
            monster.ChangeState<Monster_Move>(MonsterState.Monster_Move);
        }
    }
}
