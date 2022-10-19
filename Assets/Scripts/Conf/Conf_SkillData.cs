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
    //Ӳ��ʱ��
    public float HardTime;
    //���ɻ���
    public Vector3 RepelVelocity;
    //���ɻ��˵Ĺ���ʱ��
    public float RepelTransitionTime;
    //����Ч��
    public Conf_SkillHitEF SkillHitEF;

}

/// <summary>
/// ���ܽ���
/// </summary>
[Serializable]
public class Skill_EndModel
{
    
}

/// <summary>
/// ���ܲ������壬����
/// </summary>
[Serializable]
public class Skill_SpawnObj
{
    //���ɵ�Ԥ����
    public GameObject Prefab;
    //���ɵ���Ч
    public AudioClip AudioClip;
    //λ��
    public Vector3 Position;
    //��ת
    public Vector3 Rotation;

}