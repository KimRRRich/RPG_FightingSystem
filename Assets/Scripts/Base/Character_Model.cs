using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Model : MonoBehaviour
{
    public abstract void PlayAudio(AudioClip audioClip);
    public abstract void SpawnObject(Skill_SpawnObj spawn);
    public List<string> EnemyTargetNames;
}

public class Character_Model<T> : Character_Model
{
    protected Character_Controller<T> character;
    protected Animator animator;
    public WeaponCollider[] WeaponConllider;
    //当前技能数据
    protected Conf_SkillData skillData;
    //是否可以切换
    public bool canSwitch { get; private set; } = true;


    public virtual void Init(Character_Controller<T> character)
    {
        this.character = character;
        animator = GetComponent<Animator>();
        for (int i = 0; i < WeaponConllider.Length; i++)
        {
            WeaponConllider[i].Init(this);
        }
    }

    public override void PlayAudio(AudioClip audioClip)
    {
        character.audio.PlayAudio(audioClip);
    }
    private int currHitIndex = 0;

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
    public override void SpawnObject(Skill_SpawnObj spawn)
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
        temp.transform.eulerAngles = character.transform.eulerAngles;

        temp.transform.Translate(spawn.Position, Space.Self);
        temp.transform.eulerAngles += spawn.Rotation;
        PlayAudio(spawn.AudioClip);
    }

    private int currHurtAnimationIndex = 1;

    public void PlayHurtAnimation()
    {
        animator.SetTrigger("受伤" + currHurtAnimationIndex);
        if (currHurtAnimationIndex == 1) currHurtAnimationIndex = 2;
        else currHurtAnimationIndex = 1;
    }

    public void StopHurtAnimation()
    {
        animator.SetTrigger("受伤结束");
    }

    public void SetAnimation(string name, bool bl)
    {
        animator.SetBool(name, bl);
    }


    #region 动画事件
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
            OnSkillOver();
            Debug.Log("SkillOver!");
        }
    }

    protected virtual void OnSkillOver() { }

    //技能可以切换
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }
    //角色位移，基于第几个位移效果
    private void CharacterMoveForAttack(int index)
    {
        CharacterMoveModel model = skillData.CharacterMoveModels[index];
        character.CharacterAttackMove(model.Target, model.Time);
    }


    //生成游戏物体
    private void SpawnSkillObj(int index)
    {
        SpawnObject(skillData.SpawnObjs[index]);
    }
    #endregion
}
