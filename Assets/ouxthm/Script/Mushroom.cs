using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : Enemy
{
    public override void InitSetting(int Difficulty)  // 적의 기본 정보를 설정하는 함수
    {
        Stage = 2;
        Enemy_Mod = 5;  // 자폭
        Enemy_Power = 60f * stats[Difficulty]; //적의 공격력
        Enemy_HP = 70f * stats[Difficulty];  // 적의 체력
        Enemy_Speed = 2f;    // 적의 이동속도
        Gap_Distance_X = 99f;  // Enemy와 Player의 X 거리차이
        Gap_Distance_Y = 99f;  // Enemy와 Player의 Y 거리차이
        nextDirX = 1;  // 방향을 숫자로 표현
        Enemy_Dying_anim_Time = 0.6f;   // 죽는 애니메이션 실행 시간
        Enemy_Sensing_X = 15f; // 플레이어 인지 X값
        Enemy_Sensing_Y = 5f;  // 플레이어 인지 Y값
        Enemy_Range_X = 1.5f; //적의 X축 공격 사거리
        Enemy_Range_Y = 1.5f; //적의 Y축 공격 사거리
        atkDelay = 1f; // 공격 딜레이
        atkTime = 0.7f; // 공격 모션 시간
        bleedLevel = 0; // 받은 출혈량
        Attacking = false;
        AmIBoss = false;    //  보스가 아님
    }

    public override void Short_Monster(Transform target)
    {
        base.Short_Monster(target);   // 부모 스크립트에서 상속받아옴.
    }
}
