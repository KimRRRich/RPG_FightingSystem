using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Controller<T> : FSMControl<T>
{
    public new AudioController audio { get; protected set; }

    public Player_Model model { get; protected set; }

    public CharacterController characterController { get; protected set; }

    //生命值
    protected int hp = 100;
    public abstract int Hp { get; set; }

    //技能的包装数据cd、按键
    public SkillModel[] SkillModels;

    protected virtual void Start()
    {
       
        audio = new AudioController(GetComponent<AudioSource>());

        model = transform.Find("Model").GetComponent<Player_Model>();
        //TODO:模型层重构
        //model.Init(this);
        characterController = GetComponent<CharacterController>();
    }
    protected override void Update()
    {
        base.Update();
        UpdateSkillCD();

    }

    //更新技能CD
    private void UpdateSkillCD()
    {
        for (int i = 0; i < SkillModels.Length; i++)
        {
            SkillModels[i].Update();
        }
    }

    #region 战斗相关
    public Conf_SkillData CurrSkillData { get; protected set; }

    //当前的技能编号
    protected int currSkillIndex = -1;

    public void PlayAudio(AudioClip audioClip)
    {
        audio.PlayAudio(audioClip);
    }

    /// <summary>
    /// 角色攻击移动
    /// </summary>
    public void CharacterAttackMove(Vector3 target, float time)
    {
        StartCoroutine(DoCharacterAttackMove(transform.TransformDirection(target), time));
    }

    IEnumerator DoCharacterAttackMove(Vector3 target, float time)
    {
        float currTime = 0;
        while (currTime < time)
        {
            Vector3 moveDir = target * Time.deltaTime / time;
            characterController.Move(moveDir);
            currTime += Time.deltaTime;

        }
        //暂停一帧
        yield return null;
    }

    #endregion
}
