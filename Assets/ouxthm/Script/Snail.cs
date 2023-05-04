using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_HP = 20f;  // ���� ü��
        Enemy_Speed = 1.5f;    // ���� �̵��ӵ�
        Gap_Distance_X = 99f;  // Enemy�� Player�� X �Ÿ�����
        Gap_Distance_Y = 99f;  // Enemy�� Player�� Y �Ÿ�����
        NextMove = 1;  // ������ ���ڷ� ǥ��
        Enemy_Dying_anim_Time = 1.1f;   // �״� �ִϸ��̼� ���� �ð�
        Enemy_Attack_Range = 10f; // �÷��̾� ���� X��
        //Enemy_Attack_Range_Y = 5f;  // �÷��̾� ���� Y��
}

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }



}
