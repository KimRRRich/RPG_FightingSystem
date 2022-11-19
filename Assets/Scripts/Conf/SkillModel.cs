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
    public float CDTime;      //CD时间
    private float CurrTime=0; //0为CD结束的意思
    public Image CDimage;

    public bool canRelease { get; private set; } = true;
    public void Update()
    {
        //不能释放
        if (canRelease == false && CurrTime > 0)
        {
            CurrTime -= Time.deltaTime;
            //CD结束
            if (CurrTime <=0)
            {
                CurrTime = 0;
                canRelease = true;
            }
            if (CDimage != null)
            {
                //更新UI
                CDimage.fillAmount = CurrTime / CDTime;
            }
           
        }
    }

    //释放的时候
    public void OnRelese()
    {
        canRelease = false;
        CurrTime = CDTime;
    }

}
