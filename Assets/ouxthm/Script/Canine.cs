using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canine : Enemy
{
    public override void InitSetting(int Difficulty)  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 1;
        Enemy_Mod = 2;  // �ٰŸ�
        Enemy_Power = 30f * stats[Difficulty]; //���� ���ݷ�
        Bump_Power = 10f * stats[Difficulty]; // �浹 �� �� ������
        Enemy_HP = 100f * stats[Difficulty];  // ���� ü��
        Enemy_Speed = 2.5f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 0.7f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 10f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 5f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 2f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1f; //���� Y�� ���� ��Ÿ�
        atkX = -1.3f;    // ���� �ڽ� �ݶ��̴��� x��
        atkFlipx =  0.45f;   // �ø� �� ���� �ڽ� �ݶ��̴��� x��
        atkY = -0.1f;   // ���� �ڽ� �ݶ��̴��� y��
        atkDelay = 1f; // ���� ������
        atkTime = 0.6f; // ���� ��� �ð�
        endTime = 7f;   
        bleedLevel = 0; // ���� ������
        Attacking = false;
        Dying = false;
        AmIBoss = false;    //  ������ �ƴ�
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
