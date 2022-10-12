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
        //��֤һ���˺���һ������ֻ���һ���˺�
        if (other.tag == "Monster" && !monsterList.Contains(other.gameObject))
        {
            monsterList.Add(other.gameObject);
            //ʵ������˺�������
            other.GetComponent<Monster_Controller>().Hurt();
        }
    }

}
