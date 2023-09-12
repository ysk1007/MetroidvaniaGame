using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    public override void InitSetting(int Difficulty)  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 1;
        Enemy_Mod = 3;  // ���� ����
        Enemy_Power = 25 * stats[Difficulty]; //���� ���ݷ�
        Enemy_HP = 40f * stats[Difficulty];  // ���� ü��
        Enemy_Speed = 3f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // X�� ������ ���ڷ� ǥ��
        nextDirY = 0;   // Y�� ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 0.1f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 15f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 15f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 2f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1.2f; //���� Y�� ���� ��Ÿ�
        atkDelay = 1f; // ���� ������
        atkTime = 0.5f; // ���� ��� �ð�
        atkX = 0.8f;    // ���� �ڽ� �ݶ��̴��� x��
        atkY = -0.9f;   // ���� �ڽ� �ݶ��̴��� y��
        bleedLevel = 0; // ���� ������
        Attacker = true;
        AmIBoss = false;    //  ������ �ƴ�
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
