using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{

}
public class Monster_Controller : Character_Controller<MonsterState>
{
    //private Monster_Model model;
    //private CharacterController characterController;
    //private int hp = 100;

    //�Ƿ����
    private bool isRepel;
    //�ܻ�����
    private Vector3 repelVelocity;
    //�ܻ�����ʱ��
    private float repelTime;
    //����ʱ��
    private float currentRepelTime;

    public override int Hp { get => hp; set => hp = value; }

    protected override void OnHurt(Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime)
    {
        base.OnHurt(sourceTransform, repelVelocity, repelTransitionTime);
        //���˻���
        isRepel = true;
        this.repelVelocity = sourceTransform.TransformDirection(repelVelocity);
        repelTime = repelTransitionTime;
        currentRepelTime = 0.0f;
    }

    //public void Start()
    //{
    //    model = transform.Find("Model").GetComponent<Monster_Model>();
    //    characterController = transform.GetComponent<CharacterController>();
    //    model.Init();
    //}

    protected override void Update()
    {
        if (isRepel)
        {
            currentRepelTime += Time.deltaTime;
            //��hurtTime��ʱ���ƶ�hurtVelocity�ľ���
            characterController.Move(repelVelocity * Time.deltaTime/ repelTime);
            if (currentRepelTime >= repelTime) isRepel = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    #region ս���߼�
    
    #endregion

}
    