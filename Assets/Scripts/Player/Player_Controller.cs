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
    // 默认
    Player_None,
    // 移动
    Player_Move,
    // 攻击
    Player_Attack
}
public class Player_Controller : Character_Controller<PlayerState>
{
    public static Player_Controller Instance;
    public Player_Input input { get; private set; }
    //public Player_Model model { get; protected set; }


    //屏幕振动源
    private CinemachineImpulseSource impulseSource;


    //摄像机的目标
    private Transform cameraTarget;
    private Vector3 cameraPos;

    //UI
    public Image HPBarImg;

  
    public override int Hp { get => hp; 
        set {
            hp = value;
            if (hp < 0)
            {
                //TODO:死亡
                hp = 0;
            }
            //更新UI
            HPBarImg.fillAmount = hp / 100f;
        }
    }

    // 普通攻击配置
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

        // 默认是移动状态
        ChangeState<Player_Move>(PlayerState.Player_Move);
    }



    #region  战斗相关
    // 当前的技能

    //当前的技能编号
    protected int currSkillIndex = -1;

    // 当前是第几段攻击(普攻)
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

   
    // 检查攻击状态
    public bool CheckAttack()
    {
        //普攻检测
        if (input.GetAttackKeyDown() && model.canSwitch)
        {
            CurrSkillData = StandAttackConfs[CurrAttackIndex];
            AttackAction = StandAttack;
            currSkillIndex = -1;
            return true;
        }
        //技能检测
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

    //攻击事件，普攻或者技能
    private UnityAction AttackAction;

    //攻击
    public void Attack()
    {
        AttackAction?.Invoke();//不是null就执行
        if (currSkillIndex != -1)
        {
            SkillModels[currSkillIndex].OnRelese();
        }
    }

    //普通攻击
    private void StandAttack()
    {
        model.StartAttack(CurrSkillData);
        CurrAttackIndex++;

    }

    //技能攻击
    private void SkillAttack()
    {
        CurrAttackIndex = 0;
        model.StartAttack(CurrSkillData);
    }


    

    //屏幕震动
    public void ScreenImpulse()
    {
        impulseSource.GenerateImpulse();
    }

    //基于攻击的相机移动
    public void CameraMoveForAttack(Vector3 offset, float time, float backTime)
    {
        // 花time的时间去offset的位置
        cameraTarget.DOLocalMove(cameraPos + offset, time).onComplete = () =>
        {
            //经过backTime的时间回归到原来的位置
            cameraTarget.DOLocalMove(cameraPos, backTime);
        };

    }

    
    #endregion


}
