using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Bat : Enemy
{
    public override void InitSetting(int Difficulty)  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Stage = 3;
        Enemy_Mod = 3;  // ����
        Bump_Power = 30 * stats[Difficulty]; // �浹 ���ݷ�
        Enemy_HP = 300f * stats[Difficulty];  // ���� ü��
        Enemy_Speed = 3f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        nextDirX = 1;  // X�� ������ ���ڷ� ǥ��
        nextDirY = 0;   // Y�� ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 1.3f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Sensing_X = 15f; // �÷��̾� ���� X��
        Enemy_Sensing_Y = 15f;  // �÷��̾� ���� Y��
        Enemy_Range_X = 2f; //���� X�� ���� ��Ÿ�
        Enemy_Range_Y = 1.5f; //���� Y�� ���� ��Ÿ�
        atkDelay = 1f; // ���� ������
        atkTime = 0.5f; // ���� ��� �ð�
        atkX = 0.8f;    // ���� �ڽ� �ݶ��̴��� x��
        atkY = -1.2f;   // ���� �ڽ� �ݶ��̴��� y��
        Attacker = false;   // �浹�� ����
        bleedLevel = 0; // ���� ������
        AmIBoss = false;    //  ������ �ƴ�
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }
}
