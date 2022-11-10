using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Controller<T> : FSMControl<T>
{
    public new AudioController audio { get; protected set; }

    public Player_Model model { get; protected set; }

    public CharacterController characterController { get; protected set; }

    //����ֵ
    protected int hp = 100;
    public abstract int Hp { get; set; }

    //���ܵİ�װ����cd������
    public SkillModel[] SkillModels;

    protected virtual void Start()
    {
       
        audio = new AudioController(GetComponent<AudioSource>());

        model = transform.Find("Model").GetComponent<Player_Model>();
        //TODO:ģ�Ͳ��ع�
        //model.Init(this);
        characterController = GetComponent<CharacterController>();
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

    //��ǰ�ļ��ܱ��
    protected int currSkillIndex = -1;

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

    IEnumerator DoCharacterAttackMove(Vector3 target, float time)
    {
        float currTime = 0;
        while (currTime < time)
        {
            Vector3 moveDir = target * Time.deltaTime / time;
            characterController.Move(moveDir);
            currTime += Time.deltaTime;

        }
        //��ͣһ֡
        yield return null;
    }

    #endregion
}
