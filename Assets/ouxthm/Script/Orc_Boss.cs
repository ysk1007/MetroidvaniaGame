using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Boss : Enemy
{
    public static Orc_Boss instance;

    private void Awake()
    {
        instance = this;
    }
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 1;
        Enemy_Name = "������ ���̾�Ʈ �����"; //������ �߰���
        BossHpLine = 1; //������ �߰���
        Enemy_Mod = 6;  // ����
        Enemy_Power = 30f; //���� ���ݷ�
        Bump_Power = 20f;    // �浹 ���ݷ�
        Enemy_HP = 1500f;  // ���� ü��
        Enemy_Speed = 2f;    // ���� �̵��ӵ�
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 4.2f;   // �״� �ִϸ��̼� ���� �ð�
        atkDelay = 1f; // ���� ������
        atkTime = 0.6f; // ���� ��� �ð�
        endTime = 5f; // ����ü ������� �ð�
        bleedLevel = 0; // ���� ������
        Attacking = false;
        AmIBoss = true;    //  ������
        Dying = false;
        GameManager.Instance.GetComponent<BossHpController>().BossSpawn(this); //������ �߰���
    }

    public override void Boss(Transform target)
    {
        base.OrcBoss(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
