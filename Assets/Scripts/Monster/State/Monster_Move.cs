using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Move : Monster_StateBase
{
    public override void OnEnter()
    {
        monster.StartMove();
        model.SetAnimation("Εά", true);
    }

    public override void OnExit()
    {
        monster.StopMove();
        model.SetAnimation("Εά", false);

    }

    public override void OnUpdate()
    {
        monster.SetNavigationTarget(player.transform.position);
        float dis = Vector3.Distance(player.transform.position, monster.transform.position);
        if (dis < 1)
        {
            monster.ChangeState<Monster_Attack>(MonsterState.Monster_Attack);
            return;
        }
        if (dis > 6)
        {
            monster.ChangeState<Monster_Idle>(MonsterState.Monster_Idle);
        }
    }
}
