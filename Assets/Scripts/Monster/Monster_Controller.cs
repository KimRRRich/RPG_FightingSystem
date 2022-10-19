using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;
    private CharacterController characterController;
    private int hp = 100;

    //是否击飞
    private bool isRepel;
    //受击向量
    private Vector3 repelVelocity;
    //受击过渡时间
    private float repelTime;
    //现在时间
    private float currentRepelTime;

    public void Start()
    {
        model = transform.Find("Model").GetComponent<Monster_Model>();
        characterController = transform.GetComponent<CharacterController>();
        model.Init();
    }

    private void Update()
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

    public void Hurt(float hardTime,Transform sourceTransform,Vector3 repelVelocity,float repelTransitionTime,int damageValue)
    {

        //硬质与播放动画
        model.PlayHurtAnimation();
        //取消之前可能还在执行中的硬直
        CancelInvoke("HurtOver");
        Invoke("HurtOver", hardTime);

        //击退击飞
        isRepel = true;
        this.repelVelocity = sourceTransform.TransformDirection(repelVelocity);
        repelTime = repelTransitionTime;
        currentRepelTime = 0.0f;

        //生命值减少
        hp -= damageValue;
    }

    public void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
    