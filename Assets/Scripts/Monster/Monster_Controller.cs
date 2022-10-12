using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{
    private Monster_Model model;
    private CharacterController characterController;
    private int hp = 100;

    //�Ƿ����
    private bool isHurt;
    //�ܻ�����
    private Vector3 hurtVelocity;
    //�ܻ�����ʱ��
    private float hurtTime;
    //����ʱ��
    private float currentHurtTime;

    public void Start()
    {
        model = transform.Find("Model").GetComponent<Monster_Model>();
        characterController = transform.GetComponent<CharacterController>();
        model.Init();
    }

    private void Update()
    {
        if (isHurt)
        {
            currentHurtTime += Time.deltaTime;
            //��hurtTime��ʱ���ƶ�hurtVelocity�ľ���
            characterController.Move(hurtVelocity * Time.deltaTime/hurtTime);
            if (currentHurtTime >= hurtTime) isHurt = false;
        }
        else
        {
            characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
        }
    }

    public void Hurt()
    {
        //Ӳ���벥�Ŷ���
        model.PlayHurtAnimation();
        CancelInvoke("HurtOver");
        Invoke("HurtOver", 1);

        //���˻���
        isHurt = true;
        hurtVelocity = new Vector3(0, 1, 1);
        hurtTime = 0.2f;
        currentHurtTime = 0.0f;

        //����ֵ����
        hp -= 10;
    }

    public void HurtOver()
    {
        model.StopHurtAnimation();
    }
}
    