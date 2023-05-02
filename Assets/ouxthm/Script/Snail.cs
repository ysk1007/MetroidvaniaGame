using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_HP = 20f;  // ���� ü��
        Enemy_Speed = 2f;    // ���� �̵��ӵ�
        Enemy_Attack_Speed = 1f;    // ���� ���ݼӵ�
        Enemy_Left = false; // ���� ����
        Attacking = false;
        Hit_Set = false;    // ���͸� ����� ����
        Gap_Distance = 99f;  // Enemy�� Player ���� �Ÿ�.
        NextMove = 1;  // ������ ���ڷ� ǥ��
}

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }



}
