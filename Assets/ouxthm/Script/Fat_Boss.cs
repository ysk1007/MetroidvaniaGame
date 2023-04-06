using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fat_Boss : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider2D boxcollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Transform target;
    GameObject GameObject;
    
    public float Fat_speed = 2f; // Fat_Boss의 속도.
    public int Fat_pattern;   // Fat_Boss의 패턴을 저장할 변수.
    public float Gap_Distance = 99;  // Fat_Boss와 Player 사이 거리.
    public float AtkDistance = 5f;  // Fat_Boss의 공격 사거리.
    public float RushDistance = 10f;    // Fat_Boss의 돌진 사거리
    public float Fat_HP;    // Fat_Boss의 체력.

    public bool Attacking;

    void Awake()
    {

        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();      

        target = GameObject.Find("Player").transform;   // 오브젝트 이름이 Player의 transform.
        Fat_HP = 10f;
        AtkDistance = 5f;   // Fat_Boss의 공격 사거리.

        Attacking = false;
        StartCoroutine(Think());
    }

    void Update()
    {
        Gap_Distance = Mathf.Abs(target.position.x - transform.position.x); // 플레이어와 Fat_Boss의 사이거리.

        Move();
    }

    void Move()
    {
        if (target.transform.position.x < transform.position.x)  // 플레이어가 왼쪽에 있을 때.
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = true;    // x축 플립.
            }
            else if (Attacking == true && animator.GetBool("Running"))
            {
                spriteRenderer.flipX = true;    // x축 플립.
            }
            animator.SetBool("Walking", true);
            transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
        }
        else if(target.transform.position.x > transform.position.x)     // 플레이어가 오른쪽에 있을 때.
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = false;   // x축 플립하지 않음.
            }
            else if (Attacking == true && animator.GetBool("Running"))
            {
                spriteRenderer.flipX = false;    // x축 플립.
            }
            animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
        }
    }

    IEnumerator Think()
    {
        if (Gap_Distance < AtkDistance && Gap_Distance < RushDistance)  // 사거리 안이면, 거리 차이가 러쉬 거리보다 짧으면
        {
            animator.SetBool("Walking", false);
            Fat_speed = 0;  // 속도 0.
            Debug.Log("사거리 안이야");
            StartCoroutine(Attack());   // 공격 코루틴 실행.
            yield return new WaitForSeconds(0.1f);
        }
        else if (Gap_Distance < RushDistance && Gap_Distance > AtkDistance && Fat_HP <= 50)
        {
            Attacking = true;
            //animator.SetBool("Walking", false);
            Debug.Log("뛸 시간");
            StartCoroutine(Running());
        }
        else if (Gap_Distance > AtkDistance) // 사거리 밖이면 .
        {
            yield return new WaitForSeconds(0.1f);
            Debug.Log("실행되니?");
            StartCoroutine(Think());    // Think 코루틴 실행.
        }
    }

    IEnumerator Attack()
    {
       yield return new WaitForSeconds(0f);

       if (Attacking == false)
       {
            Attacking = true;   // 공격 중.
            
            Fat_pattern = Random.Range(1, 3);     // 패턴 번호를 1 ~ 2까지 랜덤으로 뽑음.

        }

        if (Fat_pattern == 1)
        {
            StartCoroutine(Left_Hooking());     // 왼손 공격 코루틴 실행.
        }
        if(Fat_pattern == 2)
        {
            StartCoroutine(Right_Hooking());    // 오른손 공격 코루틴 실행.
        }      
    }

    IEnumerator Left_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss의 속도를 0으로 바꿈.
        animator.SetTrigger("Left_Hooking");
        Debug.Log("왼손");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // 속도 2
        Attacking = false;  // 공격중 끄기
        StartCoroutine(Think());    // Think 코루틴 실행.
    }

    IEnumerator Right_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss의 속도를 0으로 바꿈.
        animator.SetTrigger("Right_Hooking");        
        Debug.Log("오른손");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // 속도 2.
        Attacking = false;  // 공격 중 끄기.
        StartCoroutine(Think());    // Think 코루틴 실행.
    }

    IEnumerator Running()
    {
        animator.SetBool("Running", true);
        Fat_speed = 10f;      // Fat_Boss의 속도를 로 바꿈.
        Debug.Log("뛰는 중");
        yield return new WaitForSeconds(5f);   
        Debug.Log("5초 기다림");
        animator.SetBool("Running", false);
        Fat_speed = 0;      // 속도 0으로 해서 러쉬에 대한 반동 주기.
        yield return new WaitForSeconds(2f);
        Debug.Log("2초 더 기다리고 속도 2로 바꾸기");
        Attacking = false;  // 공격 중 끄기
        Fat_speed = 2;  // 걷기 위해 속도 2.
        StartCoroutine(Think());    // Think 코루틴 실행.
    }
}
