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

    public override int Hp { get => hp; set => hp = value; }
    public Vector3 moveMotion = new Vector3(0, -9, 0);

    protected override void Start()
    {
        base.Start();
        navMeshAgent = GetComponent<NavMeshAgent>();
        //默认是移动状态
        ChangeState<Monster_Idle>(MonsterState.Monster_Idle);

    }

    protected override void Update()
    {
        base.Update();
        if (isDead)
        {
            characterController.Move(moveMotion * Time.deltaTime);
            return;
        }
       
        if (navMeshAgent.enabled == false)
        {
            characterController.Move(moveMotion* Time.deltaTime);
        }
    }

    protected override void OnHurt(Transform sourceTransform, Vector3 repelVelocity, float repelTransitionTime)
    {
        ChangeState<Monster_Hurt>(MonsterState.Monster_Hurt, true);
        (CurrStateObj as Monster_Hurt).SetData(sourceTransform, repelVelocity, repelTransitionTime);
    }
    protected override void OnHurtOver()
    {
        ChangeState<Monster_Idle>(MonsterState.Monster_Idle);
    }
    protected override void OnDead()
    {
        StopRepel();
        ChangeState<Monster_Idle>(MonsterState.Monster_Idle);
        StopMove();
        Destroy(GetComponent<CapsuleCollider>());
        //Destroy(GetComponent<CharacterController>());
        Invoke("Destory", 5);
    }
    private void Destory()
    {
        Destroy(gameObject);
    }
    public void StopRepel()
    {
        moveMotion.Set(0, -9, 0);
    }

    #region 导航
    public void StartMove()
    {
        navMeshAgent.enabled = true;
    }
    public void StopMove()
    {
        navMeshAgent.enabled = false;
    }

    //设置导航目标
    public void SetNavigationTarget(Vector3 targer)
    {
        navMeshAgent.SetDestination(targer);
    }

    #endregion

    #region 战斗逻辑
    public bool Attack()
    {
        if (model.canSwitch == false) return false;

        //有什么技能 放什么技能
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
