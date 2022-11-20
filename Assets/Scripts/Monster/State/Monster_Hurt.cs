using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Hurt : Monster_StateBase
{
    //受击向量
    private Vector3 repelVelocity;
    //受击过渡时间
    private float repelTime;
    //现在时间
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
            //用hurtTime的时间移动hurtVelocity的距离
            monster.moveMotion = repelVelocity / repelTime;
        }
        else 
        {
            monster.StopRepel();
            isStop = true;
        }

    }
}
