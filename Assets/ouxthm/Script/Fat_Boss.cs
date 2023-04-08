using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool Fat_Left = true;    // Fat_Boss의 방향.
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

        StartCoroutine(Move());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")    // collicoin = 닿았다는 뜻, 닿은 오브젝트의 태그가 Enemy일 때
        {
            StartCoroutine(StopRunning());   
        }
    }


    IEnumerator Move()
    {
        //  러닝 애니메이션이 아닐 때 움직이는 트랜슬레이트랑 뛸 때 발동하는 트랜슬레이트 따로 적용 시키면 뛸 때 플레이어를 무시하고 벽까지 뛰지 않을까?
        if (target.transform.position.x < transform.position.x && !animator.GetBool("Running") && !animator.GetBool("Collision"))  // 플레이어가 왼쪽에 있을 때와 뛰는 애니메이션이 아니라면.
        {
            //animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.

            if (Attacking == false)     // 공격 중이 아니면.
            {
                spriteRenderer.flipX = true;    // x축 플립.
            }
         }
        else if (target.transform.position.x > transform.position.x && !animator.GetBool("Running") && !animator.GetBool("Collision"))     // 플레이어가 오른쪽에 있을 때와 뛰는 애니메이션이 아니라면.
        {
            //animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
            if (Attacking == false)
            {
                spriteRenderer.flipX = false;   // x축 플립하지 않음.
            }
        }
        else if (animator.GetBool("Running") && Fat_Left == true)       // 뛰는 애니메이션이 실행 중이고, Fat_Left의 값이 true라면
        {
            gameObject.transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed );   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
        }
        else if (animator.GetBool("Running") && Fat_Left == false)  // Running 애니메이션이 실행 중이고 Fat_Left의 값이 false라면
        {
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.           
        }
        
        yield return null;
    }

    IEnumerator Think()
    {
        if (Gap_Distance < AtkDistance && Gap_Distance < RushDistance)  // 사거리 안이면, 거리 차이가 러쉬 거리보다 짧으면.
        {
            //animator.SetBool("Walking", false);
            Fat_speed = 0;  // 속도 0.
            Debug.Log("사거리 안이야");
            StartCoroutine(Attack());   // 공격 코루틴 실행.
            yield return null;
        }
        else if (Gap_Distance < RushDistance && Gap_Distance > AtkDistance && Fat_HP <= 50)     // 거리 차이가 러쉬 거리보다 짧고, 거리차이가 사거리보다 길면
        {
            //animator.SetBool("Walking", false);
            Attacking = true;
            Debug.Log("뛸 시간");
            StartCoroutine(Running());
        }
        else if (Gap_Distance > AtkDistance) // 사거리 밖이면 .
        {
            yield return null;
            Debug.Log("실행되니?");
            StartCoroutine(Think());    // Think 코루틴 실행.
        }
    }

    IEnumerator Attack()
    {
        yield return null;

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

    IEnumerator Left_Hooking()  // 왼손 공격 코루틴.
    {
        Fat_speed = 0;      // Fat_Boss의 속도를 0으로 바꿈.
        animator.SetTrigger("Left_Hooking");
        Debug.Log("왼손");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // 속도 2
        Attacking = false;  // 공격중 끄기
        StartCoroutine(Think());    // Think 코루틴 실행.
    }

    IEnumerator Right_Hooking() // 오른손 공격 코루틴.
    {
        Fat_speed = 0;      // Fat_Boss의 속도를 0으로 바꿈.
        animator.SetTrigger("Right_Hooking");        
        Debug.Log("오른손");
        yield return new WaitForSeconds(1.2f);
        Fat_speed = 2;  // 속도 2.
        Attacking = false;  // 공격 중 끄기.
        StartCoroutine(Think());    // Think 코루틴 실행.
    }

    IEnumerator Running()   // 돌진 코루틴.
    {
        yield return null;
        if (target.transform.position.x < transform.position.x) // 플레이어가 Fat_Boss보다 왼쪽에 있다면.
        {
            Fat_Left = true;    
        }
        else if (target.transform.position.x > transform.position.x)    // 플레이어가 Fat_Boss보다 오른쪽에 있다면.
        {
            Fat_Left = false;
        }
        Fat_speed = 15f;        // Fat_Boss 속도 10 설정.
        animator.SetBool("Running", true);
        yield return new WaitForSeconds(2f);
        Debug.Log("뛰는 중");
        
    }

    IEnumerator StopRunning()
    {
        StopCoroutine(Move());
        Debug.Log("충돌");
        animator.SetBool("Collision", true);
        Attacking = false;  // 공격 중 끄기
        Fat_speed = 0;      // 속도 0으로 해서 러쉬에 대한 반동 주기.
        yield return new WaitForSeconds(0.5f);
        if(Fat_Left == true)    
        {
            Fat_Left = false;   
        }
        else if(Fat_Left == false)  
        {
            Fat_Left = true;    
        }
        Debug.Log("2초 더 기다리고 속도 2로 바꾸기");
        animator.SetBool("Collision", false);
        animator.SetBool("Running", false);
        yield return new WaitForSeconds(0.7f);
        Fat_speed = 2;  // 걷기 위해 속도 2.
        StartCoroutine(Think());    // Think 코루틴 실행.
    }
}
