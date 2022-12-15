using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//[ RequireComponent(HurtEnter)]
public abstract class Character_Controller<T> : FSMControl<T>
{
    public new AudioController audio { get; protected set; }

    public Character_Model<T> model { get; protected set; }

    public CharacterController characterController { get; protected set; }

    private CharacterEventAgent characterEventAgent;

    //生命值
    protected int hp = 100;
    public bool isDead { get; protected set; } = false;
    public abstract int Hp { get; set; }

    //技能的包装数据cd、按键
    public SkillModel[] SkillModels;

    protected virtual void Start()
    {
       
        audio = new AudioController(GetComponent<AudioSource>());
        model = transform.Find("Model").GetComponent<Character_Model<T>>();
        //TODO:模型层重构
        model.Init(this);
        characterController = GetComponent<CharacterController>();
        characterEventAgent = gameObject.AddComponent<CharacterEventAgent>();
        characterEventAgent.hurtAction = Hurt;
        characterEventAgent.isDead = isDead;
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

    protected IEnumerator DoCharacterAttackMove(Vector3 target, float time)
    {
        Vector3 startPos = this.transform.position;
        Vector3 endPos = startPos + target;
        float currTime = 0;
        while (currTime < time)
        {
            //Vector3 moveDir = target * Time.deltaTime / time;
            //Vector3 moveDir = new Vector3(1, 1, 1);
            //characterController.Move(moveDir);
            //currTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, endPos, currTime / time);
            currTime += Time.deltaTime;
            //暂停一帧
            yield return null;


        }
        
    }

    public void Hurt(float hardTime, Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime, int damageValue)
    {
        if (isDead) return;
        //生命值减少
        Hp -= damageValue;
        if (Hp <= 0)
        {
            //CancelInvoke("HurtOver");
            CancelInvoke("DoCharacterAttackMove");
            Dead();
        }
        else
        {
            //硬直与播放动画
            model.PlayHurtAnimation(repelVelocity.y > 0.5f);
            //取消之前可能还在执行中的硬直
            CancelInvoke("HurtOver");
            CancelInvoke("DoCharacterAttackMove");
            Invoke("HurtOver", hardTime);
            OnHurt(sourceTransform, repelVelocity, repelTransitionTime);
        }

        model.SkillCanSwitch();
        model.RestWeapon();
    }

    protected abstract void OnHurt(Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime);


    public void HurtOver()
    {
        //model.StopHurtAnimation();
        OnHurtOver();
    }

    protected abstract void OnHurtOver();
    private void Dead()
    {
        isDead = true;
        characterEventAgent.isDead = isDead;
        model.PlayDeadAnimation();
        OnDead();
    }
    protected abstract void OnDead();

    #endregion
}
