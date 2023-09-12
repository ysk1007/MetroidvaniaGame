using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Boss : Enemy
{
    public override void InitSetting(int Difficulty)  // 적의 기본 정보를 설정하는 함수
    {
        Stage = 3;
        Enemy_Name = "깨어난 고대의 피조물"; //윤성권 추가함
        AmIBoss = true; //윤성권 추가함
        BossHpLine = 5; //윤성권 추가함
        Enemy_Mod = 2;  // 보스
        Enemy_Power = 12f; //적의 공격력
        Bump_Power = 10f;    // 충돌 공격력
        Enemy_HP = 8000f * stats[Difficulty];  // 적의 체력
        Enemy_Speed = 1f;    // 적의 이동속도
        Gap_Distance_X = 99f;  // Enemy와 Player의 X 거리차이
        Gap_Distance_Y = 99f;  // Enemy와 Player의 Y 거리차이
        nextDirX = 1;  // 방향을 숫자로 표현
        Enemy_Dying_anim_Time = 4.2f;   // 죽는 애니메이션 실행 시간
        Enemy_Sensing_X = 10f; // 플레이어 인지 X값
        Enemy_Sensing_Y = 5f;  // 플레이어 인지 Y값
        Enemy_Range_X = 2f; //적의 X축 공격 사거리
        Enemy_Range_Y = 1f; //적의 Y축 공격 사거리
        atkDelay = 1f; // 공격 딜레이
        atkTime = 0.6f; // 공격 모션 시간
        endTime = 0.8f; // 투사체 사라지는 시간
        bleedLevel = 0; // 받은 출혈량
        turning = true; // 돌기 가능
        Attacking = false;
        GameManager.Instance.GetComponent<BossHpController>().BossSpawn(this); //윤성권 추가함
    }

    public override void Boss(Transform target)
    {
        base.Boss(target);   // 부모 스크립트에서 상속받아옴.
    }
}
