using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Idle : Monster_StateBase
{
    public override void OnEnter()
    {
        //Debug.Log("Monster_Idle");
        //model.SetAnimation("��",false);
        //monster.StopMove();
        
    }

    public override void OnExit()
    {
        //throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {
        //�����������һ�׾ͽ���
        //����6�׾�׷��
        float dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            //TODO:���빥��
        }
        if (dis < 6)
        {
            //TODO:׷�����
            monster.ChangeState<Monster_Move>(MonsterState.Monster_Move);
        }
    }
}
