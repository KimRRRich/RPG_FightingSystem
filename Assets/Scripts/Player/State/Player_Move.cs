using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Move : Player_StateBase
{
    //public Player_Controller player;

    private float runTransition = 0;
    private float moveSpeed = 400f;
    private float rotateSpeed = 90f;

    //private bool isRun;
    private bool isRun
    {
        get
        {
            bool temp= player.input.GetRunKey() && player.input.Vertical > 0;
            if (temp) moveSpeed = 800f;
            else moveSpeed = 400f;
            return temp;
        }
    }


    //public override void Init(FSMControl<PlayerState> controller, PlayerState stateType)
    //{
    //    base.Init(controller, stateType);
    //    player = controller as Player_Controller;
    //}

   

    public override void OnUpdate()
    {
        
        float h = player.input.Horizontal;
        float v = player.input.Vertical;

        if (v >= 0)
        {
            if (isRun && runTransition < 1)  runTransition += Time.deltaTime / 2;
            else if(!isRun&&runTransition>0) runTransition -= Time.deltaTime / 2;
        }
        else if(runTransition > 0) runTransition -= Time.deltaTime / 2;


        Move(h, v+runTransition);

        //检测攻击 如果玩家按键攻击则切换到攻击状态
        //还需要考虑CD
        if (player.CheckAttack()) player.ChangeState<Player_Attack>(PlayerState.Player_Attack);


    }

    private void Move(float h, float v)
    {
        //移动
        Vector3 dir = new Vector3(0, 0, v);
        dir = player.transform.TransformDirection(dir);
        player.characterController.SimpleMove(dir * Time.deltaTime * moveSpeed);

        //旋转
        Vector3 rot = new Vector3(0, h, 0);
        player.transform.Rotate(rot * Time.deltaTime * rotateSpeed);

        //同步模型动画
        model.UpdateMovePar(h,v);
    }

    public override void OnEnter()
    {

    }
        
    public override void OnExit()
    {
       
    }

   

   
}
