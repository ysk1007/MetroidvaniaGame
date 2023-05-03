using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public float Gap_Distance ;  // 적과 Player 사이 거리.
    public int NextMove;  // 방향을 숫자로 표현
    public float Enemy_Attack_Range = 10f;  // 적의 공격 사거리


    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수

    public virtual void Short_Monster(Transform target)
    {
        Gap_Distance = Mathf.Abs(target.transform.position.x - transform.position.x);
        StartCoroutine(Sensing(target, rayHit));
        Sensor();
    }

    public virtual void Long_Monster()
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()
    {
        Debug.Log("Fly_Monster");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        animator = this.GetComponentInChildren<Animator>();
        if (collision.gameObject.tag == "Wall")    // collicoin = 닿았다는 뜻, 닿은 오브젝트의 태그가 Enemy일 때
        {
            Debug.Log(collision.gameObject.tag);
        }
        if (collision.gameObject.tag == "Player")    // 체력 깎이는 것 추가.
        {

            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Sword")
        {
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Axe")
        {
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
    }
    void Move() // 플레이어 감지 전 move
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Enemy_Attack_Range < Gap_Distance)    // 사정거리 밖에 있을 때 
        {
            if (NextMove == -1)       // Enemy의 값이 true라면
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running 애니메이션이 실행 중이고 Fat_Left의 값이 false라면
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.           
                spriteRenderer.flipX = true;
            }
        }
    }

    IEnumerator Sensing(Transform target, RaycastHit2D rayHit)
    {
        rigid = this.GetComponent<Rigidbody2D>();
        
        if (Gap_Distance <= Enemy_Attack_Range)      // Enemy의 사정거리에 있을 때
        {
            if (transform.position.x < target.position.x)            // 오른쪽 방향
            {
                NextMove = 1;      

                if (NextMove == 1 && rayHit.collider != null)  // NextMove가 1일 때 그리고 레이캐스트 값이 null이 아닐 때
                {
                    spriteRenderer.flipX = true;   
                    
                    transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                }
                else if (NextMove == 1 && rayHit.collider == null)
                {
                    transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (0,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                }
            }
            else if (transform.position.x > target.position.x)      // 왼쪽 방향
            {
                NextMove = -1;     
                if (NextMove == -1 && rayHit.collider != null) // NextMove가 -1일 때 그리고 레이캐스트 값이 null이 아닐 때
                {
                    spriteRenderer.flipX = false;  
                    transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                }
                else if (NextMove == -1 && rayHit.collider == null)
                {
                    transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                }
            }
        }
        else if(Gap_Distance > Enemy_Attack_Range)
        {
            Move();     // Move 함수 실행
            yield return null;
        }
       // else if(Gap_Distance > Enemy_Attack_Range && )
    }

    IEnumerator Think()     // 재귀함수 
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        NextMove = Random.Range(-1, 2);     // -1 ~ 1까지의 랜덤한 수 저장
        if (NextMove != 0 && NextMove == 1 && Gap_Distance > Enemy_Attack_Range)    // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true;       // NextMove의 값이 1이면 x축을 flip함
        }
        // 재귀
        float nextThinkTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
    }

    IEnumerator Turn()
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        NextMove *= -1;   // NextMove에 -1을 곱해 방향전환
        if(NextMove == 1 && Gap_Distance > Enemy_Attack_Range)  // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true; // NextMove 값이 1이면 x축을 flip함
        }    
        StopAllCoroutines();
        StartCoroutine(Think());
        yield return null;
    }

    public void Sensor()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        
        // Enemy의 한 칸 앞의 값을 얻기 위해 자기 자신의 위치 값에 (x)에 + NextMove값을 더하고 1.2f를 곱한다.
        Vector2 frontVec = new Vector2(rigid.position.x + NextMove * 1.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0)); 
        // 레이저를 아래로 쏘아서 실질적인 레이저 생성(물리기반), LayMask.GetMask("")는 해당하는 레이어만 스캔함
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));
       // Debug.Log(rayHit.collider);
        if (rayHit.collider == null)  
        {
           StartCoroutine(Turn());     
        }
    }
}