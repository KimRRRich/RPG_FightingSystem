using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character_Model : MonoBehaviour
{
    public abstract void PlayAudio(AudioClip audioClip);
    public abstract void SpawnObject(Skill_SpawnObj spawn);
    public List<string> EnemyTargetNames;
}

public class Character_Model<T> : Character_Model
{
    protected Character_Controller<T> character;
    protected Animator animator;
    public WeaponCollider[] WeaponConllider;
    //��ǰ��������
    protected Conf_SkillData skillData;
    //�Ƿ�����л�
    public bool canSwitch { get; private set; } = true;


    public virtual void Init(Character_Controller<T> character)
    {
        this.character = character;
        animator = GetComponent<Animator>();
        for (int i = 0; i < WeaponConllider.Length; i++)
        {
            WeaponConllider[i].Init(this);
        }
    }

    public override void PlayAudio(AudioClip audioClip)
    {
        character.audio.PlayAudio(audioClip);
    }
    private int currHitIndex = 0;

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
    public override void SpawnObject(Skill_SpawnObj spawn)
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
        temp.transform.eulerAngles = character.transform.eulerAngles;

        temp.transform.Translate(spawn.Position, Space.Self);
        temp.transform.eulerAngles += spawn.Rotation;
        PlayAudio(spawn.AudioClip);
    }

    private int currHurtAnimationIndex = 1;

    public void PlayHurtAnimation()
    {
        animator.SetTrigger("����" + currHurtAnimationIndex);
        if (currHurtAnimationIndex == 1) currHurtAnimationIndex = 2;
        else currHurtAnimationIndex = 1;
    }

    public void StopHurtAnimation()
    {
        animator.SetTrigger("���˽���");
    }

    public void SetAnimation(string name, bool bl)
    {
        animator.SetBool(name, bl);
    }


    #region �����¼�
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
            OnSkillOver();
            Debug.Log("SkillOver!");
        }
    }

    protected virtual void OnSkillOver() { }

    //���ܿ����л�
    private void SkillCanSwitch()
    {
        canSwitch = true;
    }
    //��ɫλ�ƣ����ڵڼ���λ��Ч��
    private void CharacterMoveForAttack(int index)
    {
        CharacterMoveModel model = skillData.CharacterMoveModels[index];
        character.CharacterAttackMove(model.Target, model.Time);
    }


    //������Ϸ����
    private void SpawnSkillObj(int index)
    {
        SpawnObject(skillData.SpawnObjs[index]);
    }
    #endregion
}
