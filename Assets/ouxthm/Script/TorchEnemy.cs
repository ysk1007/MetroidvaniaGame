using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchEnemy : Enemy
{
    public override void InitSetting(int Difficulty)  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 3;
        Enemy_Mod = 7;  // ���Ÿ� ����ü 
        Enemy_Power = 50f * stats[Difficulty]; //���� ���ݷ�
        Enemy_HP = 500f * stats[Difficulty];  // ���� ü��
        Enemy_Speed = 1.5f;    // ���� �̵��ӵ�
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
        endTime = 1f;   //����ü ������� �ð�
        bleedLevel = 0; // ���� ������
        AmIBoss = false;    // ������ �ƴ�
        Attacking = false;
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
