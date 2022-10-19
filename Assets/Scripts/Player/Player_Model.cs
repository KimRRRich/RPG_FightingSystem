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
        animator.SetBool("����", true);
    }


    #region �����¼�

    //���������˺�
    public void StartSkillHit()
    {
        //�����������β
        //�����˺����Ĵ�����
        WeaponConllider.StartSkillHit(skillData.HitModel);
    }

    //ֹͣ�����˺�
    public void StopSkillHit()
    {
        //�رյ������β
        //�ر��˺����Ĵ�����
        WeaponConllider.StopSkillHit();
    }


    public void SkillOver()
    {
        animator.SetBool("����", false);
        player.ChangeState<Player_Move>(PlayerState.Player_Move);
    }
    

    #endregion

}
