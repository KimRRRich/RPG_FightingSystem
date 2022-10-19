using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "命中效果配置", menuName = "配置/技能配置/命中效果")]

public class Conf_SkillHitEF : ScriptableObject
{
    //产生的粒子物体
    public Skill_SpawnObj Spawn;

    //命中时的音效
    public AudioClip AudioClip;


}
