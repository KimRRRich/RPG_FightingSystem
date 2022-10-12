using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{

    //Ä¬ÈÏ
    Player_None,
    //ÒÆ¶¯
    Player_Move,
    //¹¥»÷
    Player_Attack
}

public class Player_Controller : FSMControl<PlayerState>
{
   
    //public override Enum CurrentState { get =>playerState; set =>playerState=(PlayerState)value; }

    //private PlayerState playerState;

    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }
    public Player_Model model { get; private set; }
    public CharacterController characterController  { get; private set; }

    //ÆÕÍ¨¹¥»÷ÅäÖÃ
    public Conf_SkillData StandAttackConf;

   


    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        characterController = GetComponent<CharacterController>();
        model = transform.Find("Model").GetComponent<Player_Model>();
        model.Init(this);
        Debug.Log(input.Horizontal);

        //Ä¬ÈÏÊÇÒÆ¶¯×´Ì¬
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    //¼ì²é¹¥»÷×´Ì¬
    public bool CheckAttck()
    {
        return input.GetAttackKeyDown();
    }
}
