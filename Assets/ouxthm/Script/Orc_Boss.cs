using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Boss : Enemy
{
    public static Orc_Boss instance;

    private void Awake()
    {
        instance = this;
    }
    public override void InitSetting()  // 적의 기본 정보를 설정하는 함수
    {
        Stage = 1;
        Enemy_Name = "흉포한 자이언트 오우거"; //윤성권 추가함
        BossHpLine = 1; //윤성권 추가함
        Enemy_Mod = 6;  // 보스
        Enemy_Power = 30f; //적의 공격력
        Bump_Power = 20f;    // 충돌 공격력
        Enemy_HP = 1500f;  // 적의 체력
        Enemy_Speed = 2f;    // 적의 이동속도
        nextDirX = 1;  // 방향을 숫자로 표현
        Enemy_Dying_anim_Time = 4.2f;   // 죽는 애니메이션 실행 시간
        atkDelay = 1f; // 공격 딜레이
        atkTime = 0.6f; // 공격 모션 시간
        endTime = 5f; // 투사체 사라지는 시간
        bleedLevel = 0; // 받은 출혈량
        Attacking = false;
        AmIBoss = true;    //  보스임
        Dying = false;
        GameManager.Instance.GetComponent<BossHpController>().BossSpawn(this); //윤성권 추가함
    }

    public override void Boss(Transform target)
    {
        base.OrcBoss(target);   // 부모 스크립트에서 상속받아옴.
    }
}
