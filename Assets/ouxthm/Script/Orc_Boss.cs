using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Boss : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_Mod = 6;  // ����
        Enemy_Power = 8f; //���� ���ݷ�
        Bump_Power = 10f;    // �浹 ���ݷ�
        Enemy_HP = 35f;  // ���� ü��
        Enemy_Speed = 1f;    // ���� �̵��ӵ�
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 4.2f;   // �״� �ִϸ��̼� ���� �ð�
        atkDelay = 1f; // ���� ������
        atkTime = 0.6f; // ���� ��� �ð�
        endTime = 5f; // ����ü ������� �ð�
        bleedLevel = 0; // ���� ������
        Attacking = false;
        iamBoss = true;    //  ������
        Dying = false;
    }

    public override void Boss(Transform target)
    {
        base.OrcBoss(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
