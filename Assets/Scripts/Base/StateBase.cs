using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ״̬�������
/// ֮�����ж��󶼼̳������
/// �磺Idle��Walk��Attack
/// </summary>
public abstract class StateBase <T>
{
    //״̬��������ö��״̬
    public T StateType;

    //��ǰ���ƵĽ�ɫ
    //public FSMControl controller;

    //��һ��ʵ������ʱ���ʼ��
    public  void Init(FSMControl<T> controller,T stateType)
    {
        //this.controller = controller;
        this.StateType = stateType;
        OnInit(controller, stateType);
    }
    protected virtual void OnInit(FSMControl<T> controller, T stateType) { }

    //����
    public abstract void OnEnter();
    //����
    public abstract void OnUpdate();    
    //�˳�
    public abstract void OnExit();
}
