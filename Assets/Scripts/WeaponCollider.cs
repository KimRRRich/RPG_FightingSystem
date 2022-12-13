using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider BoxCollider;
    //public BoxCollider BoxCollider2;
    //public TrailRenderer TrailRenderer;
    public MeleeWeaponTrail MeleeWeaponTrail;

    private List<GameObject> enemyList;

    private Character_Model model;

    //当前技能的命中数据
    private Skill_HitModel hitModel;

    public void Init(Character_Model model)
    {
        //BoxCollider.enabled = false;
        //TrailRenderer.emitting = false;
        //monsterList = new List<GameObject>();
        //this.model= model ;
        //StopSkillHit();
        enemyList = new List<GameObject>();
        this.model = model;
        StopSkillHit();
    }
   public void StartSkillHit(Skill_HitModel hitModel)
    {
        this.hitModel = hitModel;
        BoxCollider.enabled = true;
        //BoxCollider2.enabled = true;
        MeleeWeaponTrail.Emit= true;
    }

    public void StopSkillHit()
    {
        BoxCollider.enabled = false;
        //BoxCollider2.enabled = false;
        MeleeWeaponTrail.Emit = false;
        enemyList.Clear();
    }

  

    private void OnTriggerStay(Collider other){
        //保证一段伤害对一个敌人只造成一段伤害
        Debug.Log("OnTriggerEnter");
        if (model.EnemyTargetNames.Contains(other.tag) && !enemyList.Contains(other.gameObject))
        {
            //敌人逻辑
            enemyList.Add(other.gameObject);

            CharacterEventAgent enemy = other.GetComponent<CharacterEventAgent>();
            //实际输出伤害给敌人
            enemy.Hurt(hitModel.HardTime,model.transform,hitModel.RepelVelocity,hitModel.RepelTransitionTime,hitModel.DamageValue);
            
            //命中 生成相关的逻辑
            if (hitModel.SkillHitEF != null)
            {
                //生成粒子-在命中的地方   closets。。。 传一个坐标，获取这个坐标和触发器的碰撞点
                SpawnObjectByHit(hitModel.SkillHitEF.Spawn, other.ClosestPointOnBounds(transform.position));

                //播放音效
                if (hitModel.SkillHitEF.AudioClip != null) model.PlayAudio(hitModel.SkillHitEF.AudioClip);
                
            }
            //如果是玩家持有
            if(model as Player_Model)
            {
                Player_Model player_Model = model as Player_Model;
                //命中 效果相关的逻辑
                if (hitModel.WantScreenImpulse) player_Model.ScreenImpulse();
                if (hitModel.WantChramaticAberration) PostProcessManager.Instance.ChromaticAberrationEF();
                Debug.Log("玩家击中");

                //生成单次释放时的游戏物体/粒子
                model.SpawnObject(hitModel.SpawnObj);
            }
            
        }
    }

    //产生物体 基于命中
    private void SpawnObjectByHit(Skill_SpawnObj spawn,Vector3 spawnPoint)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            GameObject temp = GameObject.Instantiate(spawn.Prefab, null);
            temp.transform.position = spawnPoint + spawn.Position;
            temp.transform.LookAt(Camera.main.transform);  //可要可不要
            temp.transform.eulerAngles += spawn.Rotation;
            model.PlayAudio(spawn.AudioClip);
        }
    }

}
