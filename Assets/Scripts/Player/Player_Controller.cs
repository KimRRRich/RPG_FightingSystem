using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public enum PlayerState
{
    // Ĭ��
    Player_None,
    // �ƶ�
    Player_Move,
    // ����
    Player_Attack
}
public class Player_Controller : FSMControl<PlayerState>
{
    public Player_Input input { get; private set; }
    public new Player_Audio audio { get; private set; }
    public Player_Model model { get; private set; }
    public CharacterController characterController { get; private set; }


    //��Ļ��Դ
    private CinemachineImpulseSource impulseSource;


    //�������Ŀ��
    private Transform cameraTarget;
    private Vector3 cameraPos;


    // ��ͨ��������
    public Conf_SkillData[] StandAttackConfs;
    // ��ǰ�ļ���
    public Conf_SkillData CurrSkillData { get; private set; }
    // ��ǰ�ǵڼ��ι���
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

        cameraTarget=transform.Find("CameraTarget");
        cameraPos = cameraTarget.localPosition;

        // Ĭ�����ƶ�״̬
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    // ��鹥��״̬
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

    //��Ļ��
    public void ScreenImpulse()
    {
        impulseSource.GenerateImpulse();
    }

    //���ڹ���������ƶ�
    public void CameraMoveForAttack(Vector3 offset,float time,float backTime)
    {
        // ��time��ʱ��ȥoffset��λ��
        cameraTarget.DOLocalMove(cameraPos + offset, time).onComplete=()=>
        {
            //����backTime��ʱ��ع鵽ԭ����λ��
            cameraTarget.DOLocalMove(cameraPos, backTime);
        };
        
    }
}
