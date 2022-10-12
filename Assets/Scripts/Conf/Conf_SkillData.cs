using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="技能配置",menuName ="配置/技能配置/技能数据")]
public class Conf_SkillData : ScriptableObject
{
    //技能名称
    public string Name;

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

}

/// <summary>
/// 技能命中
/// </summary>
[Serializable]
public class Skill_HitModel
{
    //伤害数值
    public int DamageValue;
}

/// <summary>
/// 技能结束
/// </summary>
[Serializable]
public class Skill_EndModel
{

}