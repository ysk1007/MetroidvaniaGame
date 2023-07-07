using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_Mod = 5;  // ����
        Enemy_Power = 30f; //���� ���ݷ�
        Enemy_HP = 30f;  // ���� ü��
        Enemy_Speed = 2f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 0.6f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 10f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 5f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 1.5f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1.5f; //���� Y�� ���� ��Ÿ�
        atkDelay = 1f; // ���� ������
        Attacking = false;
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
