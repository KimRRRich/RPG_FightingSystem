using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName ="��������",menuName ="����/��������/��������")]
public class Conf_SkillData : ScriptableObject
{
    //��������
    public string Name;

    //�ͷ�����
    public Skill_ReleaseModel ReleaseModel;

    //��������
    public Skill_HitModel HitModel;

    //��������
    public Skill_EndModel EndModel;
}

/// <summary>
/// �����ͷ�
/// </summary>
[Serializable]
public class Skill_ReleaseModel
{

}

/// <summary>
/// ��������
/// </summary>
[Serializable]
public class Skill_HitModel
{
    //�˺���ֵ
    public int DamageValue;
}

/// <summary>
/// ���ܽ���
/// </summary>
[Serializable]
public class Skill_EndModel
{

}