using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEventAgent : MonoBehaviour
{
    public Action<float,Transform, Vector3, float,int> hurtAction;
    public bool isDead;
    public void Hurt(float hardTime, Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime, int damageValue)
    {
        hurtAction(hardTime, sourceTransform, repelVelocity, repelTransitionTime, damageValue);
    }


}
