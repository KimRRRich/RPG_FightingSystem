using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Hurt : Monster_StateBase
{
    //�ܻ�����
    private Vector3 repelVelocity;
    //�ܻ�����ʱ��
    private float repelTime;
    //����ʱ��
    private float currentRepelTime;

    public void SetData(Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime)
    {
        this.repelVelocity = sourceTransform.TransformDirection(repelVelocity);
        this.repelTime = repelTransitionTime;
        this.currentRepelTime = 0;

    }

    public override void OnEnter()
    {
        isStop = false;
    }

    public override void OnExit()
    {
        
    }
    private bool isStop = false;
    public override void OnUpdate()
    {
        if (isStop) return;
        if (currentRepelTime < repelTime)
        {
            currentRepelTime += Time.deltaTime;
            //��hurtTime��ʱ���ƶ�hurtVelocity�ľ���
            monster.moveMotion = repelVelocity / repelTime;
        }
        else 
        {
            monster.StopRepel();
            isStop = true;
        }

    }
}
