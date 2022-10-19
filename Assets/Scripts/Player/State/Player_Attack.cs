using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : StateBase<PlayerState>
{
    public Player_Controller player;
    public override void Init(FSMControl<PlayerState> controller, PlayerState stateType)
    {
        base.Init(controller, stateType);
        player = controller as Player_Controller;
    }

    public override void OnEnter()
    {
        //����
       player.StandAttack();
    }

    public override void OnExit()  { }

    public override void OnUpdate(){ }
    
    
}
