using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rat : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_Mod = 1;
        Enemy_HP = 10f;  // ���� ü��
        Enemy_Speed = 1.5f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 0.4f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 10f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 5f;  // �÷��̾� ���� Y��
        Bump_Power = 2; // �浹 �� �� ������
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }



}