using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input
{
    private KeyCode runKeyCode = KeyCode.LeftShift;
    private KeyCode attackKeyCode = KeyCode.J;
    public float Horizontal { get => Input.GetAxis("Horizontal"); }
    public float Vertical { get => Input.GetAxis("Vertical"); }

    //按键持续按下状态
    public bool Getkey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    //按键按下瞬间
    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    //获取当前Run按键有没有持续按下中
    public bool GetRunKey()
    { 
        return Getkey(runKeyCode);
    }

    //获取当前Attack按键有没有持续按下中
    public bool GetAttackKeyDown()
    {
        return GetKeyDown(attackKeyCode);
    }


}
