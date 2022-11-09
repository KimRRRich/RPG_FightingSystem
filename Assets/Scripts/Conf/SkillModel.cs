using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillModel
{
    public Conf_SkillData SkillData;
    public KeyCode KeyCode;
    public float CDTime;
    public float CurrTime;
    public Image CDimage;

}
