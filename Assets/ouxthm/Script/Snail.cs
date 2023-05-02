using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void InitSetting()  // 적의 기본 정보를 설정하는 함수
    {
        Enemy_HP = 20f;  // 적의 체력
        Enemy_Speed = 2f;    // 적의 이동속도
        Enemy_Attack_Speed = 1f;    // 적의 공격속도
        Enemy_Left = false; // 적의 방향
        Attacking = false;
        Hit_Set = false;    // 몬스터를 깨우는 변수
        Gap_Distance = 99f;  // Enemy와 Player 사이 거리.
        NextMove = 1;  // 방향을 숫자로 표현
}

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // 부모 스크립트에서 상속받아옴.
    }



}
