using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider BoxCollider;
    //public TrailRenderer TrailRenderer;
    public MeleeWeaponTrail MeleeWeaponTrail;

    private List<GameObject> enemyList;

    private Character_Model model;

    //��ǰ���ܵ���������
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
        MeleeWeaponTrail.Emit= true;
    }

    public void StopSkillHit()
    {
        BoxCollider.enabled = false;
        MeleeWeaponTrail.Emit = false;
        enemyList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //��֤һ���˺���һ������ֻ���һ���˺�
        if (model.EnemyTargetNames.Contains(other.tag) && !enemyList.Contains(other.gameObject))
        {
            //�����߼�
            enemyList.Add(other.gameObject);
            //ʵ������˺�������
            other.GetComponent<Monster_Controller>().Hurt(hitModel.HardTime,model.transform,hitModel.RepelVelocity,hitModel.RepelTransitionTime,hitModel.DamageValue);
            
            //���� ������ص��߼�
            if (hitModel.SkillHitEF != null)
            {
                //��������-�����еĵط�   closets������ ��һ�����꣬��ȡ�������ʹ���������ײ��
                SpawnObjectByHit(hitModel.SkillHitEF.Spawn, other.ClosestPointOnBounds(transform.position));

                //������Ч
                if (hitModel.SkillHitEF.AudioClip != null) model.PlayAudio(hitModel.SkillHitEF.AudioClip);
                
            }
            //�������ҳ���
            if(model as Player_Model)
            {
                Player_Model player_Model = model as Player_Model;
                //���� Ч����ص��߼�
                if (hitModel.WantScreenImpulse) player_Model.ScreenImpulse();
                if (hitModel.WantChramaticAberration) PostProcessManager.Instance.ChromaticAberrationEF();

                //���ɵ����ͷ�ʱ����Ϸ����/����
                model.SpawnObject(hitModel.SpawnObj);
            }
            
        }
    }

    //�������� ��������
    private void SpawnObjectByHit(Skill_SpawnObj spawn,Vector3 spawnPoint)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            GameObject temp = GameObject.Instantiate(spawn.Prefab, null);
            temp.transform.position = spawnPoint + spawn.Position;
            temp.transform.LookAt(Camera.main.transform);  //��Ҫ�ɲ�Ҫ
            temp.transform.eulerAngles += spawn.Rotation;
            model.PlayAudio(spawn.AudioClip);
        }
    }

}
