using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider BoxCollider;
    //public TrailRenderer TrailRenderer;
    public MeleeWeaponTrail MeleeWeaponTrail;

    private List<GameObject> monsterList;

    private Player_Model model;

    //当前技能的命中数据
    private Skill_HitModel hitModel;

    public void Init(Player_Model model)
    {
        //BoxCollider.enabled = false;
        //TrailRenderer.emitting = false;
        //monsterList = new List<GameObject>();
        //this.model= model ;
        //StopSkillHit();
        monsterList = new List<GameObject>();
        this.model = model;
        StopSkillHit();
    }
   public void StartSkillHit(Skill_HitModel hitModel)
    {
        this.hitModel = hitModel;
        BoxCollider.enabled = true;
        MeleeWeaponTrail.Emit= true;
    }

    public void StopSkillHit()
    {
        BoxCollider.enabled = false;
        MeleeWeaponTrail.Emit = false;
        monsterList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //保证一段伤害对一个怪物只造成一段伤害
        if (other.tag == "Monster" && !monsterList.Contains(other.gameObject))
        {
            //怪物逻辑
            monsterList.Add(other.gameObject);
            //实际输出伤害给敌人
            other.GetComponent<Monster_Controller>().Hurt(hitModel.HardTime,model.transform,hitModel.RepelVelocity,hitModel.RepelTransitionTime,hitModel.DamageValue);
            
            //命中 生成相关的逻辑
            if (hitModel.SkillHitEF != null)
            {
                //生成粒子-在命中的地方   closets。。。 传一个坐标，获取这个坐标和触发器的碰撞点
                SpawnObjectByHit(hitModel.SkillHitEF.Spawn, other.ClosestPointOnBounds(transform.position));

                //播放音效
                if (hitModel.SkillHitEF.AudioClip != null) model.PlayAudio(hitModel.SkillHitEF.AudioClip);
                
            }
            

            //命中 效果相关的逻辑
            if (hitModel.WantScreenImpulse) model.ScreenImpulse();
            if (hitModel.WantChramaticAberration) PostProcessManager.Instance.ChromaticAberrationEF();

            //生成单次释放时的游戏物体/粒子
            model.SpawnObject(hitModel.SpawnObj);
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
