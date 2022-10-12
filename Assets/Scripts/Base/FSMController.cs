using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//����״̬��������
//��ң������
public abstract class FSMControl<T> : MonoBehaviour
{
    //��ǰ��״̬
    public T CurrentState;

    //��ǰ��״̬����
    protected StateBase<T> CurrStateObj;

    //���ȫ��״̬���󡪶����
    private Dictionary<T, StateBase<T>> stateDic = new Dictionary<T, StateBase<T>>();
    //private List<StateBase<T>> stateList = new List<StateBase<T>>();


    /// <summary>
    /// �޸�״̬
    /// </summary>
    public void ChangeState<K>(T newState,bool reCurrState = false) where K : StateBase<T>, new()
    {
        //�����״̬�͵�ǰ״̬һ�� ͬʱ ������Ҫ�ı�״̬
        if (newState.Equals(CurrentState) && !reCurrState) return;

        //�����ǰ״̬���ڣ�Ӧ��ִ�����˳�
        if (CurrStateObj != null) CurrStateObj.OnExit();

        //�����µ�״̬ ���һ�� �µ�״̬����
        //StateBase tempStateObj = GetStateObj(newState);
        CurrStateObj=GetStateObj<K>(newState);
        CurrStateObj.OnEnter();

    }

    /// <summary>
    /// ��ȡ״̬����
    /// ��һ��ö�٣�����һ�������ö��ͬ�������͵Ķ���
    /// </summary>
    private StateBase<T> GetStateObj<K>(T stateType) where K: StateBase<T>,new()
    {
        if (stateDic.ContainsKey(stateType))return stateDic[stateType];

        //������˵������û��
        //ʵ����һ��������
        StateBase<T> state = new K();
        state.Init(this,stateType);
        stateDic.Add(stateType,state);
        return state;
    }

    protected virtual void Update()
    {
        if (CurrStateObj != null) CurrStateObj.OnUpdate();
    }
}

 