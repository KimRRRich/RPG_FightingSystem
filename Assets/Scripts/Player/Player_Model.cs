using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����������㡢����Ч��
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider WeaponConllider;
    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        WeaponConllider.Init();
    }
    //�����ƶ���ز���
    public void UpdateMovePar(float x, float y)//ǰ�������
    {
        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
        //animator

    }

    public void StartAttack()
    {
        animator.SetBool("����", true);
    }


    #region �����¼�

    //���������˺�
    public void StartSkillHit()
    {
        //�����������β
        //�����˺����Ĵ�����
        WeaponConllider.StartSkillHit();
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
