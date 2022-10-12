using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{

    //Ĭ��
    Player_None,
    //�ƶ�
    Player_Move,
    //����
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

    //��ͨ��������
    public Conf_SkillData StandAttackConf;

   


    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        characterController = GetComponent<CharacterController>();
        model = transform.Find("Model").GetComponent<Player_Model>();
        model.Init(this);
        Debug.Log(input.Horizontal);

        //Ĭ�����ƶ�״̬
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    //��鹥��״̬
    public bool CheckAttck()
    {
        return input.GetAttackKeyDown();
    }
}
