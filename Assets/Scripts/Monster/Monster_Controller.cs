using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;
    private CharacterController characterController;
    private int hp = 100;

    //�Ƿ����
    private bool isRepel;
    //�ܻ�����
    private Vector3 repelVelocity;
    //�ܻ�����ʱ��
    private float repelTime;
    //����ʱ��
    private float currentRepelTime;

    public void Start()
    {
        model = transform.Find("Model").GetComponent<Monster_Model>();
        characterController = transform.GetComponent<CharacterController>();
        model.Init();
    }

    private void Update()
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

    public void Hurt(float hardTime,Transform sourceTransform,Vector3 repelVelocity,float repelTransitionTime,int damageValue)
    {

        //Ӳ���벥�Ŷ���
        model.PlayHurtAnimation();
        //ȡ��֮ǰ���ܻ���ִ���е�Ӳֱ
        CancelInvoke("HurtOver");
        Invoke("HurtOver", hardTime);

        //���˻���
        isRepel = true;
        this.repelVelocity = sourceTransform.TransformDirection(repelVelocity);
        repelTime = repelTransitionTime;
        currentRepelTime = 0.0f;

        //����ֵ����
        hp -= damageValue;
    }

    public void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
    