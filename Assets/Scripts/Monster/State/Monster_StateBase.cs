using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster_StateBase : StateBase<MonsterState>
{
    protected Monster_Controller monster;
    protected Monster_Model model;
    protected override void OnInit(FSMControl<MonsterState> controller, MonsterState stateType)
    {
        base.OnInit(controller, stateType);
        monster = controller as Monster_Controller;
        model = monster.model as Monster_Model;
    }
}

   
