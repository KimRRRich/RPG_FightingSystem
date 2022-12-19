using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动画、武器层、刀光效果
public class Player_Model : Character_Model<PlayerState>
{
    private Player_Controller player;
    public GameObject Weapon;

    public override void Init(Character_Controller<PlayerState> character)
    {
        base.Init(character);
        player = character as Player_Controller;
    }
    //更新移动相关参数
    public void UpdateMovePar(float x, float y)//前后和左右
    {
        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
        //animator

    }  

    public void ScreenImpulse()
    {
        player.ScreenImpulse();
    }


    #region 动画事件


    //开启技能伤害
    

    //相机移动，基于第几个位移效果
    private void CameraMoveForAttack(int index)
    {
        CameraModel model = skillData.CameraMoveModels[index];
        player.CameraMoveForAttack(model.Target, model.Time, model.BackTime);
        Debug.Log("Cemara move");
    }

    protected override void OnSkillOver()
    {
        player.CurrAttackIndex = 0;
        player.ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    public void SetActiveWeapon()
    {
        Debug.Log("SetActiveWeapon");
        Weapon.SetActive(true);
    }

    public void ChangeTimeSpeedStart(float n)
    {
        Debug.Log("ChangeTimeSpeedStart" + n);
        Time.timeScale = n;
        //Time.maximumDeltaTime = n;
        //Time.fixedDeltaTime = n * UNITY_DEFAULT_STEP;
    }
    public void ChangeTimeSpeedEnd()
    {
        Time.timeScale = 1;
        Debug.Log("ChangeTimeSpeedEnd!");
    }

    public void DisableWeapon()
    {
        Debug.Log("DisableWeapon");
        Weapon.SetActive(false);
    }


    #endregion

}
