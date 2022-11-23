using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : Monster_StateBase
{
    public override void OnEnter(){ }

    public override void OnExit() { }

    public override void OnUpdate()
    {
        if (monster.isDead) return;
        //�����������һ�׾ͽ���
        //����6�׾�׷��
        float dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            monster.ChangeState<Monster_Attack>(MonsterState.Monster_Attack);
            return;
        }
        else if (dis < 6)
        {
            //TODO:׷�����
            monster.ChangeState<Monster_Move>(MonsterState.Monster_Move);
        }
    }
}
