using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;
    private CharacterController characterController;
    private int hp = 100;

    //是否击飞
    private bool isHurt;
    //受击向量
    private Vector3 hurtVelocity;
    //受击过渡时间
    private float hurtTime;
    //现在时间
    private float currentHurtTime;

    public void Start()
    {
        model = transform.Find("Model").GetComponent<Monster_Model>();
        characterController = transform.GetComponent<CharacterController>();
        model.Init();
    }

    private void Update()
    {
        if (isHurt)
        {
            currentHurtTime += Time.deltaTime;
            //用hurtTime的时间移动hurtVelocity的距离
            characterController.Move(hurtVelocity * Time.deltaTime/hurtTime);
            if (currentHurtTime >= hurtTime) isHurt = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    public void Hurt()
    {
        //硬质与播放动画
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver");
        Invoke("HurtOver", 1);

        //击退击飞
        isHurt = true;
        hurtVelocity = new Vector3(0, 1, 1);
        hurtTime = 0.2f;
        currentHurtTime = 0.0f;

        //生命值减少
        hp -= 10;
    }

    public void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
    