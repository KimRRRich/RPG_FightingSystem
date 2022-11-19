using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
using DG.Tweening;
using UnityEngine.UI;

public enum PlayerState
{
    // Ĭ��
    Player_None,
    // �ƶ�
    Player_Move,
    // ����
    Player_Attack
}
public class Player_Controller : Character_Controller<PlayerState>
{
    public static Player_Controller Instance;
    public Player_Input input { get; private set; }
    //public Player_Model model { get; protected set; }


    //��Ļ��Դ
    private CinemachineImpulseSource impulseSource;


    //�������Ŀ��
    private Transform cameraTarget;
    private Vector3 cameraPos;

    //UI
    public Image HPBarImg;

  
    public override int Hp { get => hp; 
        set {
            hp = value;
            if (hp < 0)
            {
                //TODO:����
                hp = 0;
            }
            //����UI
            HPBarImg.fillAmount = hp / 100f;
        }
    }

    // ��ͨ��������
    public Conf_SkillData[] StandAttackConfs;

    private void Awake()
    {
        Instance = this;
    }

    protected override void Start()
    {
        base.Start();
        //model = transform.Find("Model").GetComponent<Player_Model>();
        //model.Init(this);
        input = new Player_Input();

        impulseSource = GetComponent<CinemachineImpulseSource>();

        cameraTarget=transform.Find("CameraTarget");
        cameraPos = cameraTarget.localPosition;

        // Ĭ�����ƶ�״̬
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }



    #region  ս�����
    // ��ǰ�ļ���

    //��ǰ�ļ��ܱ��
    protected int currSkillIndex = -1;

    // ��ǰ�ǵڼ��ι���(�չ�)
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

   
    // ��鹥��״̬
    public bool CheckAttack()
    {
        //�չ����
        if (input.GetAttackKeyDown() && model.canSwitch)
        {
            CurrSkillData = StandAttackConfs[CurrAttackIndex];
            AttackAction = StandAttack;
            currSkillIndex = -1;
            return true;
        }
        //���ܼ��
        for (int i = 0; i < SkillModels.Length; i++)
        {
            if (input.GetKeyDown(SkillModels[i].KeyCode) &&SkillModels[i].canRelease &&model.canSwitch)
            {
                CurrSkillData = SkillModels[i].SkillData;
                AttackAction = SkillAttack;
                currSkillIndex = i;
                return true;
            }
        }
        return false;

    }

    //�����¼����չ����߼���
    private UnityAction AttackAction;

    //����
    public void Attack()
    {
        AttackAction?.Invoke();//����null��ִ��
        if (currSkillIndex != -1)
        {
            SkillModels[currSkillIndex].OnRelese();
        }
    }

    //��ͨ����
    private void StandAttack()
    {
        model.StartAttack(CurrSkillData);
        CurrAttackIndex++;

    }

    //���ܹ���
    private void SkillAttack()
    {
        CurrAttackIndex = 0;
        model.StartAttack(CurrSkillData);
    }


    

    //��Ļ��
    public void ScreenImpulse()
    {
        impulseSource.GenerateImpulse();
    }

    //���ڹ���������ƶ�
    public void CameraMoveForAttack(Vector3 offset, float time, float backTime)
    {
        // ��time��ʱ��ȥoffset��λ��
        cameraTarget.DOLocalMove(cameraPos + offset, time).onComplete = () =>
        {
            //����backTime��ʱ��ع鵽ԭ����λ��
            cameraTarget.DOLocalMove(cameraPos, backTime);
        };

    }

    
    #endregion


}
