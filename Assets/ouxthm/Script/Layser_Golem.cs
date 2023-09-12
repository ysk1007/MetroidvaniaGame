using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Layser_Golem : Enemy
{
    public override void InitSetting(int Difficulty)  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 3;
        Enemy_Mod = 4;  // ����ü X ���Ÿ�
        Enemy_Power = 60f * stats[Difficulty]; //���� ���ݷ�
        Enemy_HP = 700f * stats[Difficulty];  // ���� ü��
        Enemy_Speed = 1f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 1f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 15f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 5f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 7f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1f; //���� Y�� ���� ��Ÿ�
        atkDelay = 1.5f; // ���� ������
        atkTime = 1.1f; // ���� ��� �ð�
        bleedLevel = 0; // ���� ������
        Attacking = false;
        AmIBoss = false;    //  ������ �ƴ�
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
