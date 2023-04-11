using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void InitSetting()  // 적의 기본 정보를 설정하는 함수
    {
        Enemy_HP = 20;
        Enemy_Speed = 1;
    }

    public override void Short_Monster()
    {
        base.Short_Monster();   // 부모 스크립트에서 상속받아옴.
    }

}
