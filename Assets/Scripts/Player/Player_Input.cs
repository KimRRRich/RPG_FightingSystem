using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input
{
    private KeyCode runKeyCode = KeyCode.LeftShift;
    private KeyCode attackKeyCode = KeyCode.J;
    public float Horizontal { get => Input.GetAxis("Horizontal"); }
    public float Vertical { get => Input.GetAxis("Vertical"); }

    //������������״̬
    public bool Getkey(KeyCode key)
    {
        return Input.GetKey(key);
    }

    //��������˲��
    public bool GetKeyDown(KeyCode key)
    {
        return Input.GetKeyDown(key);
    }

    //��ȡ��ǰRun������û�г���������
    public bool GetRunKey()
    { 
        return Getkey(runKeyCode);
    }

    //��ȡ��ǰAttack������û�г���������
    public bool GetAttackKeyDown()
    {
        return GetKeyDown(attackKeyCode);
    }


}
