using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 状态对象基类
/// 之后所有对象都继承这个类
/// 如：Idle，Walk，Attack
/// </summary>
public abstract class StateBase <T>
{
    //状态对象代表的枚举状态
    public T StateType;

    //当前控制的角色
    //public FSMControl controller;

    //第一次实例化的时候初始化
    public  void Init(FSMControl<T> controller,T stateType)
    {
        //this.controller = controller;
        this.StateType = stateType;
        OnInit(controller, stateType);
    }
    protected virtual void OnInit(FSMControl<T> controller, T stateType) { }

    //进入
    public abstract void OnEnter();
    //更新
    public abstract void OnUpdate();    
    //退出
    public abstract void OnExit();
}
