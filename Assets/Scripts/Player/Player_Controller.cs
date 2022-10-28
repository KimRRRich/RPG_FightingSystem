using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum PlayerState
{
    // 默认
    Player_None,
    // 移动
    Player_Move,
    // 攻击
    Player_Attack
}
public class Player_Controller : FSMControl<PlayerState>
{
    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }
    public Player_Model model { get; private set; }
    public CharacterController characterController { get; private set; }


    //屏幕振动源
    private CinemachineImpulseSource impulseSource;

    // 普通攻击配置
    public Conf_SkillData[] StandAttackConfs;
    // 当前的技能
    public Conf_SkillData CurrSkillData { get; private set; }
    // 当前是第几段攻击
    private int currAttackIndex = 0;
    public int CurrAttackIndex
    {
        get => currAttackIndex;
        set
        {
            if (value == StandAttackConfs.Length) currAttackIndex = 0;
            else currAttackIndex = value;
        }
    }



    private void Start()
    {
        input = new Player_Input();
        audio = new Player_Audio(GetComponent<AudioSource>());
        model = transform.Find("Model").GetComponent<Player_Model>();
        model.Init(this);
        characterController = GetComponent<CharacterController>();
        impulseSource = GetComponent<CinemachineImpulseSource>();

        // 默认是移动状态
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    // 检查攻击状态
    public bool CheckAttack()
    {
        return input.GetAttackKeyDown() && model.canSwitch;
    }

    public void StandAttack()
    {
        model.StartAttack(StandAttackConfs[CurrAttackIndex]);
        CurrSkillData = StandAttackConfs[CurrAttackIndex];
        CurrAttackIndex++;

    }

    public void PlayAudio(AudioClip audioClip)
    {
        audio.PlayAudio(audioClip);
    }

    //屏幕震动
    public void ScreenImpulse()
    {
        impulseSource.GenerateImpulse();
    }
}
