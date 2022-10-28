using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动画、武器层、刀光效果
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider WeaponConllider;


    //当前技能数据
    private Conf_SkillData skillData;

    //是否可以切换
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

    //更新移动相关参数
    public void UpdateMovePar(float x, float y)//前后和左右
    {
        animator.SetFloat("左右", x);
        animator.SetFloat("前后", y);
        //animator

    }

    public void StartAttack(Conf_SkillData conf)
    {
        skillData = conf;
        canSwitch = false;
        animator.SetTrigger(skillData.TriggerName);
    }

    //产生物体 基于命中
    private void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            GameObject temp = GameObject.Instantiate(spawn.Prefab, null);
            temp.transform.position =transform.position + spawn.Position;
            //temp.transform.LookAt(Camera.main.transform);  //可要可不要
            temp.transform.eulerAngles =player.transform.eulerAngles+spawn.Rotation;
            PlayAudio(spawn.AudioClip);
        }
    }

    public void ScreenImpulse()
    {
        player.ScreenImpulse();
    }


    #region 动画事件

    //开启技能伤害
    public void StartSkillHit()
    {
        //开启刀光的拖尾
        //开启伤害检测的触发器
        WeaponConllider.StartSkillHit(skillData.HitModel);

        //生成释放时的游戏物体/粒子
        SpawnObject(skillData.ReleaseModel.SpawnObj);

        //音效
        PlayAudio(skillData.ReleaseModel.AudioClip);


    }

    //停止技能伤害
    public void StopSkillHit()
    {
        //关闭刀光的拖尾
        //关闭伤害检测的触发器
        WeaponConllider.StopSkillHit();
    }


    //public void SkillOver(string skillName)
    //{
    //    if (skillName == skillData.Name)
    //    { 
    //        //基于结束配置生成粒子/游戏物体
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
            // 基于结束配置生成粒子/游戏物体
            SpawnObject(skillData.EndModel.SpawnObj);
            canSwitch = true;
            animator.SetTrigger(skillData.OverTriggerName);
            player.CurrAttackIndex = 0;
            player.ChangeState<Player_Move>(PlayerState.Player_Move);
            Debug.Log("SkillOver");
        }
    }

    //技能可以切换
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }


    #endregion

}
