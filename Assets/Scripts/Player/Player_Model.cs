using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动画、武器层、刀光效果
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
    //更新移动相关参数
    public void UpdateMovePar(float x, float y)//前后和左右
    {
        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
        //animator

    }

    public void StartAttack()
    {
        animator.SetBool("攻击", true);
    }


    #region 动画事件

    //开启技能伤害
    public void StartSkillHit()
    {
        //开启刀光的拖尾
        //开启伤害检测的触发器
        WeaponConllider.StartSkillHit();
    }

    //停止技能伤害
    public void StopSkillHit()
    {
        //关闭刀光的拖尾
        //关闭伤害检测的触发器
        WeaponConllider.StopSkillHit();
    }


    public void SkillOver()
    {
        animator.SetBool("攻击", false);
        player.ChangeState<Player_Move>(PlayerState.Player_Move);
    }
    

    #endregion

}
