using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����������㡢����Ч��
public class Player_Model : MonoBehaviour
{
    private Player_Controller player;
    private Animator animator;
    public WeaponCollider[] WeaponConllider;


    //��ǰ��������
    private Conf_SkillData skillData;

    //�Ƿ�����л�
    public bool canSwitch { get; private set; } = true;

    public void Init(Player_Controller player)
    {
        this.player = player;
        animator = GetComponent<Animator>();
        for(int i = 0; i < WeaponConllider.Length; i++)
        {
            WeaponConllider[i].Init(this);
        }
        
    }

    public void PlayAudio(AudioClip audioClip)
    {
        player.audio.PlayAudio(audioClip);
    }

    //�����ƶ���ز���
    public void UpdateMovePar(float x, float y)//ǰ�������
    {
        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
        //animator

    }

    public void StartAttack(Conf_SkillData conf)
    {
        currHitIndex = 0;
        skillData = conf;
        canSwitch = false;
        animator.SetTrigger(skillData.TriggerName);

        //���ɵ��ε�����
        SpawnObject(skillData.ReleaseModel.SpawnObj);

        //��Ч
        PlayAudio(skillData.ReleaseModel.AudioClip);
    }

    //�������� ��������
    public void SpawnObject(Skill_SpawnObj spawn)
    {
        if (spawn != null && spawn.Prefab != null)
        {
            StartCoroutine(DoSpawnObject(spawn));
        }
    }

    private IEnumerator DoSpawnObject(Skill_SpawnObj spawn)
    {
        yield return new WaitForSeconds(spawn.Time);
        GameObject temp = GameObject.Instantiate(spawn.Prefab, null);
        //�����ҵĵ�ǰ���곯����ƫ��
        temp.transform.position = transform.position;
        temp.transform.eulerAngles = player.transform.eulerAngles;

        temp.transform.Translate(spawn.Position, Space.Self);
        temp.transform.eulerAngles += spawn.Rotation;
        PlayAudio(spawn.AudioClip);
    }

    public void ScreenImpulse()
    {
        player.ScreenImpulse();
    }


    #region �����¼�

    private int currHitIndex = 0;
    //���������˺�
    public void StartSkillHit(int weaponIndex)
    {
        //�����������β
        //�����˺����Ĵ�����
        WeaponConllider[weaponIndex].StartSkillHit(skillData.HitModels[currHitIndex]);
        

        //���ɵ��ι����ͷ�ʱ����Ϸ����/����
        //SpawnObject(skillData.HitModels[currHitIndex].SpawnObj);

        //��Ч
        PlayAudio(skillData.HitModels[currHitIndex].AudioClip);
        currHitIndex++;

    }

    //ֹͣ�����˺�
    public void StopSkillHit(int weaponIndex)
    {
        //�رյ������β
        //�ر��˺����Ĵ�����
        WeaponConllider[weaponIndex].StopSkillHit();
    }


    private void SkillOver(string skillName)
    {
        Debug.Log("SkillOver?");
        if (skillName == skillData.Name)
        {
            // ���ڽ���������������/��Ϸ����
            SpawnObject(skillData.EndModel.SpawnObj);
            canSwitch = true;
            animator.SetTrigger(skillData.OverTriggerName);
            player.CurrAttackIndex = 0;
            player.ChangeState<Player_Move>(PlayerState.Player_Move);
            Debug.Log("SkillOver!");
        }
    }

    //���ܿ����л�
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }

    //����ƶ������ڵڼ���λ��Ч��
    private void CameraMoveForAttack(int index)
    {
        CameraModel model = skillData.CameraMoveModels[index];
        player.CameraMoveForAttack(model.Target, model.Time, model.BackTime);
    }

    //��ɫλ�ƣ����ڵڼ���λ��Ч��
    private void CharacterMoveForAttack(int index)
    {
        CharacterMoveModel model = skillData.CharacterMoveModels[index];
        player.CharacterAttackMove(model.Target, model.Time);
    }


    //������Ϸ����
    private void SpawnSkillObj(int index)
    {
        SpawnObject(skillData.SpawnObjs[index]);
    }
    #endregion

}
