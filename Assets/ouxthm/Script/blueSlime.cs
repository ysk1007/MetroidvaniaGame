using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blueSlime : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_Mod = 2;  // �ٰŸ�
        Enemy_Power = 3f; //���� ���ݷ�
        Enemy_HP = 15f;  // ���� ü��
        Enemy_Speed = 1.5f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 0.3f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 10f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 5f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 2f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1f; //���� Y�� ���� ��Ÿ�
        atkX = 0.91f;    // ���� �ڽ� �ݶ��̴��� x��
        atkY = -0.21f;   // ���� �ڽ� �ݶ��̴��� y��
        atkDelay = 1.2f; // ���� ������
        atkTime = 0.4f; // ���� ��� �ð�
        Attacking = false;
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}