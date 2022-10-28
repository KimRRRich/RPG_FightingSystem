using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����������㡢����Ч��
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider WeaponConllider;


    //��ǰ��������
    private Conf_SkillData skillData;

    //�Ƿ�����л�
    public bool canSwitch { get; private set; } = true;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        WeaponConllider.Init(this);
    }

    public void PlayAudio(AudioClip audioClip)
    {
        player.audio.PlayAudio(audioClip);
    }

    //�����ƶ���ز���
    public void UpdateMovePar(float x, float y)//ǰ�������
    {
        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
        //animator

    }

    public void StartAttack(Conf_SkillData conf)
    {
        skillData = conf;
        canSwitch = false;
        animator.SetTrigger(skillData.TriggerName);
    }

    //�������� ��������
    private void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            GameObject temp = GameObject.Instantiate(spawn.Prefab, null);
            temp.transform.position =transform.position + spawn.Position;
            //temp.transform.LookAt(Camera.main.transform);  //��Ҫ�ɲ�Ҫ
            temp.transform.eulerAngles =player.transform.eulerAngles+spawn.Rotation;
            PlayAudio(spawn.AudioClip);
        }
    }

    public void ScreenImpulse()
    {
        player.ScreenImpulse();
    }


    #region �����¼�

    //���������˺�
    public void StartSkillHit()
    {
        //�����������β
        //�����˺����Ĵ�����
        WeaponConllider.StartSkillHit(skillData.HitModel);

        //�����ͷ�ʱ����Ϸ����/����
        SpawnObject(skillData.ReleaseModel.SpawnObj);

        //��Ч
        PlayAudio(skillData.ReleaseModel.AudioClip);


    }

    //ֹͣ�����˺�
    public void StopSkillHit()
    {
        //�رյ������β
        //�ر��˺����Ĵ�����
        WeaponConllider.StopSkillHit();
    }


    //public void SkillOver(string skillName)
    //{
    //    if (skillName == skillData.Name)
    //    { 
    //        //���ڽ���������������/��Ϸ����
    //        SpawnObject(skillData.EndModel.SpawnObj);
    //        canSwitch = true;
    //        animator.SetTrigger(skillData.OverTriggerName);
    //        player.CurrAttackIndex = 0;
    //        //animator.SetTrigger(skillData.OverTriggerName);
    //        player.ChangeState<Player_Move>(PlayerState.Player_Move);

    //    }

    //}
    private void SkillOver(string skillName)
    {
        if (skillName == skillData.Name)
        {
            // ���ڽ���������������/��Ϸ����
            SpawnObject(skillData.EndModel.SpawnObj);
            canSwitch = true;
            animator.SetTrigger(skillData.OverTriggerName);
            player.CurrAttackIndex = 0;
            player.ChangeState<Player_Move>(PlayerState.Player_Move);
            Debug.Log("SkillOver");
        }
    }

    //���ܿ����л�
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }


    #endregion

}
