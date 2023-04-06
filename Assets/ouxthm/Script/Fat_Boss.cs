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

    public float Fat_speed = 2f; // Fat_Boss의 속도
    public int Fat_pattern;   // Fat_Boss의 패턴을 저장할 변수
    public float Gap_Distance = 99;  // Fat_Boss와 Player 사이 거리
    public float AtkDistance = 5f;  // Fat_Boss의 공격 사거리
    public float Fat_HP;    // Fat_Boss의 체력

    public bool Attacking;

    void Awake()
    {

        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();      

        target = GameObject.Find("Player").transform;   // 오브젝트 이름이 Player의 transform.
        Fat_HP = 100f;
        AtkDistance = 5f;   // Fat_Boss의 공격 사거리

        Attacking = false;
        StartCoroutine(Think());
    }

    void Update()
    {
        Gap_Distance = Mathf.Abs(target.position.x - transform.position.x); // 플레이어와 Fat_Boss의 사이거리

        Move();
    }

    void Move()
    {
        if (target.transform.position.x < transform.position.x)  // 플레이어가 왼쪽에 있을 때
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = true;    // x축 플립
            }
            animator.SetBool("Walking", true);
            transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
        }
        else if(target.transform.position.x > transform.position.x)     // 플레이어가 오른쪽에 있을 때
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = false;   // x축 플립하지 않음
            }
            animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
        }
    }

    IEnumerator Think()
    {
        if (Gap_Distance < AtkDistance)  // 사거리 안이면
        {
            animator.SetBool("Walking", false);
            Fat_speed = 0;
            Debug.Log("사거리 안이야");
            Attacking = false;
            StartCoroutine(Attack());   // 공격 코루틴 실행
            yield return new WaitForSeconds(0.1f);
        }
        else if (Gap_Distance > AtkDistance) // 사거리 밖이면 
        {
            yield return new WaitForSeconds(1f);
            Attacking = true;
            Debug.Log("실행되니?");
            StartCoroutine(Think());
        }
    }

    IEnumerator Attack()
    {
       yield return new WaitForSeconds(0f);

       if (Attacking == false)
        {
            Attacking = true;
            Fat_pattern = Random.Range(1, 3);     // 패턴 번호를 1 ~ 2까지 랜덤으로 뽑음
            
            //Fat_pattern = Random.Range(1, 4);     // 패턴 번호를 1 ~ 3까지 랜덤으로 뽑음
        }

        if (Fat_pattern == 1)
        {
            StartCoroutine(Left_Hooking());
        }
        if(Fat_pattern == 2)
        {
            StartCoroutine(Right_Hooking());
        }
        else if(Fat_pattern == 3)
        {
            StartCoroutine(Running());
        }
    }

    IEnumerator Left_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss의 속도를 0으로 바꿈
        animator.SetTrigger("Left_Hooking");
        Debug.Log("왼손");
        yield return new WaitForSeconds(1.5f);
        Fat_speed = 2;
        Attacking = false;
        StartCoroutine(Think());
    }

    IEnumerator Right_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss의 속도를 0으로 바꿈
        animator.SetTrigger("Right_Hooking");        
        Debug.Log("오른손");
        yield return new WaitForSeconds(1.5f);
        Fat_speed = 2;
        Attacking = false;
        StartCoroutine(Think());
    }

    IEnumerator Running()
    {
        Fat_speed = 10f;      // Fat_Boss의 속도를 5로 바꿈
        animator.SetBool("Running", true);
        yield return new WaitForSeconds(10f);
        animator.SetBool("Running", false);
        Fat_speed = 0;
    }
}
