using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    public BoxCollider BoxCollider;
    public TrailRenderer TrailRenderer;

    private List<GameObject> monsterList;

    public void Init()
    {
        BoxCollider.enabled = false;
        TrailRenderer.emitting = false;
        monsterList = new List<GameObject>();
    }
   public void StartSkillHit()
    {
        BoxCollider.enabled = true;
        TrailRenderer.emitting = true;
    }

    public void StopSkillHit()
    {
        BoxCollider.enabled = false;
        TrailRenderer.emitting = false;
        monsterList.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        //保证一段伤害对一个怪物只造成一段伤害
        if (other.tag == "Monster" && !monsterList.Contains(other.gameObject))
        {
            monsterList.Add(other.gameObject);
            //实际输出伤害给敌人
            other.GetComponent<Monster_Controller>().Hurt();
        }
    }

}
