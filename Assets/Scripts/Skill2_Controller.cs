using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Controller : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitAudioClip;
    public AudioClip efAudioClip;

    //Key:怪物
    //Value:怪物上一次经历的攻击波数
    private Dictionary<GameObject, int> monsters = new Dictionary<GameObject, int>();

    //
    private int currNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        //计算波数
        StartCoroutine("CalNum");
        Invoke("Destroy", 3);
    }


    IEnumerator CalNum()
    {
        audioSource.PlayOneShot(efAudioClip);
        while (currNum < 3)
        {
            yield return new WaitForSeconds(0.1f);
            currNum++;
            audioSource.PlayOneShot(efAudioClip);
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            //怪物第一次收到攻击
            if (monsters.ContainsKey(other.gameObject) == false)
            {
                //进行伤害
                monsters.Add(other.gameObject,currNum);
                other.GetComponent<Monster_Controller>().Hurt(0.3f, transform, new Vector3(0, 0.3f, 1), 0.2f, 10);
                audioSource.PlayOneShot(hitAudioClip);
            }
            else
            {
                //波数一样，无需伤害
                if (currNum == monsters[other.gameObject])
                {
                    return;
                }
                else//更新波数
                {
                    monsters[other.gameObject] = currNum;
                    other.GetComponent<Monster_Controller>().Hurt(0.3f, transform, new Vector3(0, 0.3f, 1), 0.2f, 10);
                    audioSource.PlayOneShot(hitAudioClip);
                }
            }
        }
    }


    private void Destroy()
    {
        Destroy(gameObject);
        StopAllCoroutines();
    }


}
