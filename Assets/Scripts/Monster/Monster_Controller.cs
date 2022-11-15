using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{

}
public class Monster_Controller : Character_Controller<MonsterState>
{
    //private Monster_Model model;
    //private CharacterController characterController;
    //private int hp = 100;

    //是否击飞
    private bool isRepel;
    //受击向量
    private Vector3 repelVelocity;
    //受击过渡时间
    private float repelTime;
    //现在时间
    private float currentRepelTime;

    public override int Hp { get => hp; set => hp = value; }

    protected override void OnHurt(Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime)
    {
        base.OnHurt(sourceTransform, repelVelocity, repelTransitionTime);
        //击退击飞
        isRepel = true;
        this.repelVelocity = sourceTransform.TransformDirection(repelVelocity);
        repelTime = repelTransitionTime;
        currentRepelTime = 0.0f;
    }

    //public void Start()
    //{
    //    model = transform.Find("Model").GetComponent<Monster_Model>();
    //    characterController = transform.GetComponent<CharacterController>();
    //    model.Init();
    //}

    protected override void Update()
    {
        if (isRepel)
        {
            currentRepelTime += Time.deltaTime;
            //用hurtTime的时间移动hurtVelocity的距离
            characterController.Move(repelVelocity * Time.deltaTime/ repelTime);
            if (currentRepelTime >= repelTime) isRepel = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    #region 战斗逻辑
    
    #endregion

}
    