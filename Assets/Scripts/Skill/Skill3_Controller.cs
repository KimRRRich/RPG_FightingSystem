using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill3_Controller : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip hitAudioClip;
    public AudioClip efAudioClip;

    //Key:敌人
    //Value:敌人上一次经历的攻击波数
    private List<GameObject> monsters = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        audioSource.PlayOneShot(efAudioClip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //敌人第一次收到攻击
            if (monsters.Contains(other.gameObject) == false)
            {
                //进行伤害
                monsters.Add(other.gameObject);
                other.GetComponent<Monster_Controller>().Hurt(0.3f, transform, new Vector3(0, 0.8f, 0.2f), 0.15f, 10);
                audioSource.PlayOneShot(hitAudioClip);
            }
            
        }
    }


}
