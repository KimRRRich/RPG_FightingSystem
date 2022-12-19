using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����������㡢����Ч��
public class Player_Model : Character_Model<PlayerState>
{
    private Player_Controller player;
    public GameObject Weapon;

    public override void Init(Character_Controller<PlayerState> character)
    {
        base.Init(character);
        player = character as Player_Controller;
    }
    //�����ƶ���ز���
    public void UpdateMovePar(float x, float y)//ǰ�������
    {
        animator.SetFloat("����", x);
        animator.SetFloat("ǰ��", y);
        //animator

    }  

    public void ScreenImpulse()
    {
        player.ScreenImpulse();
    }


    #region �����¼�


    //���������˺�
    

    //����ƶ������ڵڼ���λ��Ч��
    private void CameraMoveForAttack(int index)
    {
        CameraModel model = skillData.CameraMoveModels[index];
        player.CameraMoveForAttack(model.Target, model.Time, model.BackTime);
        Debug.Log("Cemara move");
    }

    protected override void OnSkillOver()
    {
        player.CurrAttackIndex = 0;
        player.ChangeState<Player_Move>(PlayerState.Player_Move);
    }

    public void SetActiveWeapon()
    {
        Debug.Log("SetActiveWeapon");
        Weapon.SetActive(true);
    }

    public void ChangeTimeSpeedStart(float n)
    {
        Debug.Log("ChangeTimeSpeedStart" + n);
        Time.timeScale = n;
        //Time.maximumDeltaTime = n;
        //Time.fixedDeltaTime = n * UNITY_DEFAULT_STEP;
    }
    public void ChangeTimeSpeedEnd()
    {
        Time.timeScale = 1;
        Debug.Log("ChangeTimeSpeedEnd!");
    }

    public void DisableWeapon()
    {
        Debug.Log("DisableWeapon");
        Weapon.SetActive(false);
    }


    #endregion

}
