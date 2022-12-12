using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : Monster_StateBase
{
    public override void OnEnter()
    {
        monster.StartMove();
        model.PlayAnimation("Εά");
    }

    public override void OnExit()
    {
        monster.StopMove();
        //model.SetAnimation("Εά", false);

    }

    public override void OnUpdate()
    {
        if (player.isDead)
        {
            monster.ChangeState<Monster_Idle>(MonsterState.Monster_Idle);
            return;
        }
        monster.SetNavigationTarget(player.transform.position);
        float dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            monster.transform.LookAt(new Vector3(player.transform.position.x, monster.transform.position.y, player.transform.position.z));
            monster.ChangeState<Monster_Attack>(MonsterState.Monster_Attack);
            return;
        }
        if (dis > 6)
        {
            monster.ChangeState<Monster_Idle>(MonsterState.Monster_Idle);
        }
    }
}
