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
        //¹¥»÷
       player.StandAttack();
    }

    public override void OnExit()  { }

    public override void OnUpdate()
    {
        //×ªÏò
        if (player.CurrSkillData.ReleaseModel.CanRotate)
        {
            //Ðý×ª
            Vector3 rot = new Vector3(0, player.input.Horizontal, 0);
            player.transform.Rotate(rot * Time.deltaTime * 60);
        }
    
    }
    
    
}
