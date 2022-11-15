using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Player_StateBase : StateBase<PlayerState>
{
    protected Player_Controller player;
    protected Player_Model model;
    protected override void OnInit(FSMControl<PlayerState> controller, PlayerState stateType)
    {
        base.OnInit(controller, stateType);
        player = controller as Player_Controller;
        model = player.model as Player_Model;
        
    }

    
}
