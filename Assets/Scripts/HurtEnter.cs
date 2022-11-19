using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HurtEnter : MonoBehaviour
{
    public Action<float,Transform, Vector3, float,int> action;
    public void Hurt(float hardTime, Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime, int damageValue)
    {
        action(hardTime, sourceTransform, repelVelocity, repelTransitionTime, damageValue);
    }


}
