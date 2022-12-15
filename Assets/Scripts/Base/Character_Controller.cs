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

    //����ֵ
    protected int hp = 100;
    public bool isDead { get; protected set; } = false;
    public abstract int Hp { get; set; }

    //���ܵİ�װ����cd������
    public SkillModel[] SkillModels;

    protected virtual void Start()
    {
       
        audio = new AudioController(GetComponent<AudioSource>());
        model = transform.Find("Model").GetComponent<Character_Model<T>>();
        //TODO:ģ�Ͳ��ع�
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

    //���¼���CD
    private void UpdateSkillCD()
    {
        for (int i = 0; i < SkillModels.Length; i++)
        {
            SkillModels[i].Update();
        }
    }

    #region ս�����
    public Conf_SkillData CurrSkillData { get; protected set; }

    

    public void PlayAudio(AudioClip audioClip)
    {
        audio.PlayAudio(audioClip);
    }

    /// <summary>
    /// ��ɫ�����ƶ�
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
            //��ͣһ֡
            yield return null;


        }
        
    }

    public void Hurt(float hardTime, Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime, int damageValue)
    {
        if (isDead) return;
        //����ֵ����
        Hp -= damageValue;
        if (Hp <= 0)
        {
            //CancelInvoke("HurtOver");
            CancelInvoke("DoCharacterAttackMove");
            Dead();
        }
        else
        {
            //Ӳֱ�벥�Ŷ���
            model.PlayHurtAnimation(repelVelocity.y > 0.5f);
            //ȡ��֮ǰ���ܻ���ִ���е�Ӳֱ
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
