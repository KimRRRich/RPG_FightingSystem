using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


//有限状态机控制器
//玩家，怪物等
public abstract class FSMControl<T> : MonoBehaviour
{
    //当前的状态
    public T CurrentState;

    //当前的状态对象
    protected StateBase<T> CurrStateObj;

    //存放全部状态对象—对象池
    private Dictionary<T, StateBase<T>> stateDic = new Dictionary<T, StateBase<T>>();
    //private List<StateBase<T>> stateList = new List<StateBase<T>>();


    /// <summary>
    /// 修改状态
    /// </summary>
    public void ChangeState<K>(T newState,bool reCurrState = false) where K : StateBase<T>, new()
    {
        //如果新状态和当前状态一致 同时 并不需要改变状态
        if (newState.Equals(CurrentState) && !reCurrState) return;

        //如果当前状态存在，应该执行其退出
        if (CurrStateObj != null) CurrStateObj.OnExit();

        //基于新的状态 获得一个 新的状态对象
        //StateBase tempStateObj = GetStateObj(newState);
        CurrStateObj=GetStateObj<K>(newState);
        CurrStateObj.OnEnter();

    }

    /// <summary>
    /// 获取状态对象
    /// 给一个枚举，返回一个和这个枚举同名的类型的对象
    /// </summary>
    private StateBase<T> GetStateObj<K>(T stateType) where K: StateBase<T>,new()
    {
        if (stateDic.ContainsKey(stateType))return stateDic[stateType];

        //到这里说明库里没有
        //实例化一个并返回
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

 