using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill2_Controller : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip hitAudioClip;
    public AudioClip efAudioClip;

    //Key:����
    //Value:������һ�ξ����Ĺ�������
    private Dictionary<GameObject, int> monsters = new Dictionary<GameObject, int>();

    //
    private int currNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        //���㲨��
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
            //�����һ���յ�����
            if (monsters.ContainsKey(other.gameObject) == false)
            {
                //�����˺�
                monsters.Add(other.gameObject,currNum);
                other.GetComponent<Monster_Controller>().Hurt(0.3f, transform, new Vector3(0, 0.3f, 1), 0.2f, 10);
                audioSource.PlayOneShot(hitAudioClip);
            }
            else
            {
                //����һ���������˺�
                if (currNum == monsters[other.gameObject])
                {
                    return;
                }
                else//���²���
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
