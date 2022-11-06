using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//动画、武器层、刀光效果
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider[] WeaponConllider;


    //当前技能数据
    private Conf_SkillData skillData;

    //是否可以切换
    public bool canSwitch { get; private set; } = true;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        for(int i = 0; i < WeaponConllider.Length; i++)
        {
            WeaponConllider[i].Init(this);
        }
        
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
        currHitIndex = 0;
        skillData = conf;
        canSwitch = false;
        animator.SetTrigger(skillData.TriggerName);

        //生成单次的粒子
        SpawnObject(skillData.ReleaseModel.SpawnObj);

        //音效
        PlayAudio(skillData.ReleaseModel.AudioClip);
    }

    //产生物体 基于命中
    public void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            StartCoroutine(DoSpawnObject(spawn));
        }
    }

    private IEnumerator DoSpawnObject(Skill_SpawnObj spawn)
    {
        yield return new WaitForSeconds(spawn.Time);
        GameObject temp = GameObject.Instantiate(spawn.Prefab, null);
        //相对玩家的当前坐标朝向来偏移
        temp.transform.position = transform.position;
        temp.transform.eulerAngles = player.transform.eulerAngles;

        temp.transform.Translate(spawn.Position, Space.Self);
        temp.transform.eulerAngles += spawn.Rotation;
        PlayAudio(spawn.AudioClip);
    }

    public void ScreenImpulse()
    {
        player.ScreenImpulse();
    }


    #region 动画事件

    private int currHitIndex = 0;
    //开启技能伤害
    public void StartSkillHit(int weaponIndex)
    {
        //开启刀光的拖尾
        //开启伤害检测的触发器
        WeaponConllider[weaponIndex].StartSkillHit(skillData.HitModels[currHitIndex]);
        

        //生成单次攻击释放时的游戏物体/粒子
        //SpawnObject(skillData.HitModels[currHitIndex].SpawnObj);

        //音效
        PlayAudio(skillData.HitModels[currHitIndex].AudioClip);
        currHitIndex++;

    }

    //停止技能伤害
    public void StopSkillHit(int weaponIndex)
    {
        //关闭刀光的拖尾
        //关闭伤害检测的触发器
        WeaponConllider[weaponIndex].StopSkillHit();
    }


    private void SkillOver(string skillName)
    {
        Debug.Log("SkillOver?");
        if (skillName == skillData.Name)
        {
            // 基于结束配置生成粒子/游戏物体
            SpawnObject(skillData.EndModel.SpawnObj);
            canSwitch = true;
            animator.SetTrigger(skillData.OverTriggerName);
            player.CurrAttackIndex = 0;
            player.ChangeState<Player_Move>(PlayerState.Player_Move);
            Debug.Log("SkillOver!");
        }
    }

    //技能可以切换
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }

    //相机移动，基于第几个位移效果
    private void CameraMoveForAttack(int index)
    {
        CameraModel model = skillData.CameraMoveModels[index];
        player.CameraMoveForAttack(model.Target, model.Time, model.BackTime);
    }

    //角色位移，基于第几个位移效果
    private void CharacterMoveForAttack(int index)
    {
        CharacterMoveModel model = skillData.CharacterMoveModels[index];
        player.CharacterAttackMove(model.Target, model.Time);
    }


    //生成游戏物体
    private void SpawnSkillObj(int index)
    {
        SpawnObject(skillData.SpawnObjs[index]);
    }
    #endregion

}
