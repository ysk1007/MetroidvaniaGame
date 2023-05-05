using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{
    public int Enemy_Mod;   // 1: 달팽이, 2: 근접공격 가능 몬스터, 3:비행몬스터, 4:원거리 몬스터
    public float Enemy_HP;  // 적의 체력
    public float Enemy_Speed;   // 적의 이동속도
    public float Enemy_Atk_Speed;    // 적의 공격속도
    public bool Enemy_Left; // 적의 방향
    public bool Attacking;
    public bool Hit_Set;    // 몬스터를 깨우는 변수
    public float Gap_Distance_X ;  // 적과 Player X 거리차이
    public float Gap_Distance_Y ;  // 적과 Player Y 거리차이
    public int NextMove;  // 방향을 숫자로 표현
    public float Enemy_Sensing_X;  // 적의 X축 감지 사거리
    public float Enemy_Sensing_Y;  // 적의 Y축 감지 사거리
    public float Enemy_Range_X = 2f; //적의 X축 공격 사거리
    public float Enemy_Range_Y = 2f; //적의 Y축 공격 사거리

    public float Enemy_Dying_anim_Time;     // 죽는 애니메이션 시간 변수

    public GameObject gmobj;
    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수(추상 클래스)

    public virtual void Short_Monster(Transform target) // 근거리 공격 몬스터
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x);
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y);
        //StartCoroutine(Sensing(target, rayHit));
        StartCoroutine(Attack(target, rayHit));
        Sensor();
    }
    public virtual void Long_Monster()  // 원거리 공격 몬스터
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()   // 비행 몬스터
    {
        Debug.Log("Fly_Monster");
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        yield return null;

        animator = this.GetComponentInChildren<Animator>();
        if (collision.gameObject.tag == "Wall")    
        {
            Debug.Log(collision.gameObject.tag);
        }
        if (collision.gameObject.tag == "Player")   // 임시로 무기 대신 사용 중
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Sword")
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Axe")
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            StartCoroutine(Hit());
        }
    }
    void Move() // 플레이어 감지 전 move
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Enemy_Sensing_X < Gap_Distance_X)    // 사정거리 밖에 있을 때 
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
        else if(Enemy_Sensing_X > Gap_Distance_X && Enemy_Sensing_Y < Gap_Distance_Y)
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

    IEnumerator Sensing(Transform target, RaycastHit2D rayHit)  // 플레이어 추적 
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Gap_Distance_X <= Enemy_Sensing_X)      // Enemy의 X축 사거리에 있을 때
        {
            if(Gap_Distance_Y < Enemy_Sensing_Y)    // Enemy의 Y축 사러기에 있을 때
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
            else if (Gap_Distance_Y > Enemy_Sensing_Y)
            {
                Move();
            }
            
        }
        else if(Gap_Distance_X > Enemy_Sensing_X && Gap_Distance_Y > Enemy_Sensing_Y)
        {
            Move();     // Move 함수 실행
            yield return null;
        }
    }

    IEnumerator Think()     // 재귀함수 
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        NextMove = Random.Range(-1, 2);     // -1 ~ 1까지의 랜덤한 수 저장
        if (NextMove != 0 && NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
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
        if(NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
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
        if (rayHit.collider == null)  
        {
           StartCoroutine(Turn());     
        }
    }
    
    IEnumerator Hit()
    {
        Enemy_HP -= 5;
        if (Enemy_HP > 0) // Enemy의 체력이 0 이상일 때
        {
            animator.SetTrigger("Hit");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(0.5f);
            Enemy_Speed = 1.5f;
        }
        else if (Enemy_HP <= 0) // Enemy의 체력이 0과 같거나 이하일 때
        {
            animator.SetTrigger("Die");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.SetActive(false);   // 오브젝트 사라지게 함
        }
    }


    IEnumerator Attack(Transform target, RaycastHit2D rayHit)  // 플레이어 추적   // 테스트용 제작 코루틴이며 Sensing 코루틴에 덧씌울 예정
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Gap_Distance_X <= Enemy_Sensing_X)      // Enemy의 X축 사거리에 있을 때
        {
            if (Gap_Distance_Y < Enemy_Sensing_Y)
            {
                if (transform.position.x < target.position.x)            // 오른쪽 방향
                {
                    NextMove = 1;

                    if (NextMove == 1 && rayHit.collider != null)  // NextMove가 1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = true;

                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if(Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("공격했음");
                        }
                    }
                    else if (NextMove == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (0,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("공격했음");
                        }
                    }
                }
                else if (transform.position.x > target.position.x)      // 왼쪽 방향
                {
                    NextMove = -1;
                    if (NextMove == -1 && rayHit.collider != null) // NextMove가 -1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = false;
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("공격했음");
                        }
                    }
                    else if (NextMove == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("공격했음");
                        }
                    }
                }
            }
            else if (Gap_Distance_Y > Enemy_Sensing_Y)
            {
                Move();
            }

        }
        else if (Gap_Distance_X > Enemy_Sensing_X && Gap_Distance_Y > Enemy_Sensing_Y)
        {
            Move();     // Move 함수 실행
            yield return null;
        }
    }

    IEnumerator Atking()
    {
        gmobj.SetActive(true);
        yield return new WaitForSeconds(1f);
        gmobj.SetActive(false);
        Debug.Log("공격코루틴실행");
    }

}