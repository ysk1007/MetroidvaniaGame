using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void InitSetting()  // ���� �⺻ ������ �����ϴ� �Լ�
    {
        Enemy_HP = 20;
        Enemy_Speed = 1;
    }

    public override void Short_Monster()
    {
        base.Short_Monster();   // �θ� ��ũ��Ʈ���� ��ӹ޾ƿ�.
    }

}
