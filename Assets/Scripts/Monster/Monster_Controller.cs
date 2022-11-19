using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
    Monster_None,
    Monster_Idle,
    Monster_Move,
    Monster_Attack,
    Monster_Hurt,
    Monster_Dead,

}
public class Monster_Controller : Character_Controller<MonsterState>
{
    //private Monster_Model model;
    //private CharacterController characterController;
    //private int hp = 100;
    private NavMeshAgent navMeshAgent;

    //�Ƿ����
    private bool isRepel;
    //�ܻ�����
    private Vector3 repelVelocity;
    //�ܻ�����ʱ��
    private float repelTime;
    //����ʱ��
    private float currentRepelTime;

    public override int Hp { get => hp; set => hp = value; }

    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //Ĭ�����ƶ�״̬
        ChangeState<Monster_Idle>(MonsterState.Monster_Idle);

    }

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
        base.Update();
       
        if (navMeshAgent.enabled == false)
        {
            if (isRepel)
            {
                currentRepelTime += Time.deltaTime;
                //��hurtTime��ʱ���ƶ�hurtVelocity�ľ���
                characterController.Move(repelVelocity * Time.deltaTime / repelTime);
                if (currentRepelTime >= repelTime) isRepel = false;
            }
            else
            {
                characterController.Move(new Vector3(0, -9f, 0) * Time.deltaTime);
            }
        }
    }

    #region ����
    public void StartMove()
    {
        navMeshAgent.enabled = true;
    }
    public void StopMove()
    {
        navMeshAgent.enabled = false;
    }

    //���õ���Ŀ��
    public void SetNavigationTarget(Vector3 targer)
    {
        navMeshAgent.SetDestination(targer);
    }

    #endregion

    #region ս���߼�
    public bool Attack()
    {
        if (model.canSwitch == false) return false;

        //��ʲô���� ��ʲô����
        for(int i = 0; i < SkillModels.Length; i++)
        {
            if (SkillModels[i].canRelease)
            {
                CurrSkillData = SkillModels[i].SkillData;
                model.StartAttack(CurrSkillData);
                SkillModels[i].OnRelese();
                return true;
            }
        }
        return false; 
    }

    #endregion

}
