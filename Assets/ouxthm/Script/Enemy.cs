using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{

    public float Enemy_HP;  // 적의 체력
    public float Enemy_Speed;   // 적의 이동속도
    public float Enemy_Attack_Speed;    // 적의 공격속도
    public bool Enemy_Left; // 적의 방향
    public bool Attacking;
    public bool Hit_Set;    // 몬스터를 깨우는 변수
    public float Gap_Distance;  // Fat_Boss와 Player 사이 거리.
    public float RushDistance;    // Fat_Boss의 돌진 사거리
    public float NextMove;  // 방향을 숫자로 표현


    Rigidbody rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수

    public virtual void Short_Monster()
    {
        // 애니메이션 구현하면 됨.
        //virtual로 썻기 때문에 하위 스크립트들은 상속만 받으면 돌아감
        Debug.Log("Short_Monster");
    }

    public virtual void Long_Monster()
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()
    {
        Debug.Log("Fly_Monster");
    }

}