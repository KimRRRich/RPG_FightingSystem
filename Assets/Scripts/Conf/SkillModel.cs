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
    public float CDTime;      //CDʱ��
    private float CurrTime=0; //0ΪCD��������˼
    public Image CDimage;

    public bool canRelease { get; private set; } = true;
    public void Update()
    {
        //�����ͷ�
        if (canRelease == false && CurrTime > 0)
        {
            CurrTime -= Time.deltaTime;
            //CD����
            if (CurrTime <=0)
            {
                CurrTime = 0;
                canRelease = true;
            }
            if (CDimage != null)
            {
                //����UI
                CDimage.fillAmount = CurrTime / CDTime;
            }
           
        }
    }

    //�ͷŵ�ʱ��
    public void OnRelese()
    {
        canRelease = false;
        CurrTime = CDTime;
    }

}
