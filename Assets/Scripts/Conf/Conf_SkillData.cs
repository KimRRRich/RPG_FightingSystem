using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="技能配置",menuName ="配置/技能配置/技能数据")]
public class Conf_SkillData : ScriptableObject
{
    //技能名称
    public string Name;

    //技能触发的动画TriggerName
    public string TriggerName;

    //技能结束触发的动画TriggerName
    public string OverTriggerName;

    //释放数据
    public Skill_ReleaseModel ReleaseModel;

    //命中数据
    public Skill_HitModel HitModel;

    //结束数据
    public Skill_EndModel EndModel;
}

/// <summary>
/// 技能释放
/// </summary>
[Serializable]
public class Skill_ReleaseModel
{
    //播放粒子/产生游戏物体
    public Skill_SpawnObj SpawnObj;
    //播放音效
    public AudioClip AudioClip;
    //能否旋转
    public bool CanRotate;
}

/// <summary>
/// 技能命中
/// </summary>
[Serializable]
public class Skill_HitModel
{
    //伤害数值
    public int DamageValue;
    //硬质时间
    public float HardTime;
    //击飞击退
    public Vector3 RepelVelocity;
    //击飞击退的过度时间
    public float RepelTransitionTime;
    //是否需要屏幕震动
    public bool WantScreenImpulse;
    //命中效果
    public Conf_SkillHitEF SkillHitEF;

}

/// <summary>
/// 技能结束
/// </summary>
[Serializable]
public class Skill_EndModel
{
    //播放粒子/产生游戏物体
    public Skill_SpawnObj SpawnObj;
}

/// <summary>
/// 技能产生物体，粒子
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    //生成的预制体
    public GameObject Prefab;
    //生成的音效
    public AudioClip AudioClip;
    //位置
    public Vector3 Position;
    //旋转
    public Vector3 Rotation;

}