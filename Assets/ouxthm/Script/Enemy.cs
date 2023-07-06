using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{
    public int Enemy_Mod;   // 1: 달팽이, 2: 근접공격 가능 몬스터, 3:비행몬스터, 4:원거리 몬스터
    public float Enemy_HP;  // 적의 체력
    public float Enemy_Power;   //적의 공격력
    public float Enemy_Speed;   // 적의 이동속도
    public float Enemy_Atk_Speed;    // 적의 공격속도
    public bool Attacking = false;  // 공격 중인이 확인하는 변수
    public bool Hit_Set;    // 피해를 입었는지 확인하는 변수
    public float Gap_Distance_X;  // 적과 Player X 거리차이
    public float Gap_Distance_Y;  // 적과 Player Y 거리차이
    public float Enemy_Sensing_X;  // 적의 X축 감지 사거리
    public float Enemy_Sensing_Y;  // 적의 Y축 감지 사거리
    public float Enemy_Range_X; //적의 X축 공격 사거리
    public float Enemy_Range_Y; //적의 Y축 공격 사거리
    public float Pdamage;   // 몬스터가 받는 데미지
    public float Bump_Power;    // 플레이어와 충돌 시 줄 데미지
    public float atkDelay;  // 공격 딜레이
    public int nextDirX;    // 비행 몬스터의 X 방향
    public int nextDirY;    // 비행 몬스터의 Y 방향  
    public bool Dying = false; // 죽는 중을 확인하는 변수
    public float Enemy_Dying_anim_Time;     // 죽는 애니메이션 시간 변수
    public float atkX;  // 공격 콜라이더의 x값
    public float atkY;  // 공격 콜라이더의 y값
    public bool enemyHit = false;   // 적이 피해입은 상태인지 확인하는 변수
    public float old_Speed;     // 속도 값 변하기 전 속도 값
    public float dTime;

    public GameObject Split_Slime;

    Transform spawn;    // 분열된 슬라임 생성될 위치 1
    Transform spawn2;   // 분열된 슬라임 생성될 위치 2
    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    BoxCollider2D Bcollider;
    BoxCollider2D Box;
    Transform posi;
    BoxCollider2D Boxs;
   
    public Transform Pos;
    public Arrow arrow;

    public abstract void InitSetting(); // 적의 기본 정보를 설정하는 함수(추상)

    public virtual void Short_Monster(Transform target) 
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x);
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y);
        Sensing(target, rayHit);
        Sensor();
    }
    public virtual void onetime()   // Awake에 적용
    {
        Pos = GetComponent<Transform>();
        StartCoroutine(Think());
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Boxs = GetComponent<BoxCollider2D>();
            Bump();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Arrow")
            {
                arrow = collision.GetComponent<Arrow>();
                if (arrow != null)
                {
                    Pdamage = arrow.Dmg;
                    StartCoroutine(Hit(Pdamage));
                }
                else
                    Debug.Log("좆버그");               // 가끔 일어남 해결해야 함 예외처리 실행하면 됨
            }
        }
    }

    void Move() // 이동
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        gameObject.transform.Translate(new Vector2(nextDirX, 0) * Time.deltaTime * Enemy_Speed);

        if(Enemy_Mod == 9)
        {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();
        }

        if (nextDirX == -1)
        {
            spriteRenderer.flipX = false;
        }
        else if (nextDirX == 1)
        {
            spriteRenderer.flipX = true;
        }
        if (Enemy_Mod == 5 && (nextDirX == 1 || nextDirX == -1))
        {
            animator.SetBool("Run", true);
        }
        else if (Enemy_Mod == 5 && (nextDirX == 0))
        {
            animator.SetBool("Run", false);
        }
    }

    IEnumerator Think() // 자동으로 다음 방향을 정하는 코루틴
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        nextDirX = Random.Range(-1, 2);     // 적의 X방향 랜덤( -1 ~ 1)
        if(Enemy_Mod == 3)
        {
        nextDirY = Random.Range(-1, 2);     // 적의 Y방향 랜덤( -1 ~ 1)
        }
        
        if (nextDirX == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true;       // nextDirX의 값이 1이면 x축을 flip함
        }
        // 재귀
        float nextThinkTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
    }

    void Turn() // 이미지를 뒤집는 코루틴
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        nextDirX *= -1;   // nextDirX에 -1을 곱해 방향전환
        if (nextDirX == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range를 추가하지 않으면 플레이어가 사거리 내에 있고 rayHit=null이라면 제자리 돌기함
        {
            spriteRenderer.flipX = true; // nextDirX 값이 1이면 x축을 flip함
        }
        StopAllCoroutines();
        StartCoroutine(Think());
    }

    void delayTime()
    {
        dTime -= Time.deltaTime;
        if (dTime > 0)
        {
            delayTime();
        }
        else if (dTime <= 0)
        {
            return;
        }
    }

    public IEnumerator Hit(float damage) // 피해 함수
    {
        posi = this.gameObject.GetComponent<Transform>();
        enemyHit = true;
        animator = this.GetComponentInChildren<Animator>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        rigid = this.GetComponent<Rigidbody2D>();

        Enemy_HP -= damage;
        Debug.Log("데미지 받음");
        if (Enemy_HP > 0) // Enemy의 체력이 0 이상일 때
        {
            if (!animator.GetBool("Hit"))
            {
                /*한 사이클 돌고 
                  0 디버그 두 번째 찍힐 때 처음 히트 애니메이션 나옴
                  애니메이션 끝나고 1.5 올드 스피드 디버그와 1.5 디버그 나옴*/

                if(Enemy_Speed > 0)
                {
                    old_Speed = Enemy_Speed;  // 이전 속도 값으로 돌리기 위해 다른 변수에 속도 값을 저장
                }
                animator.SetTrigger("Hit");
                Enemy_Speed = 0;
                yield return new WaitForSeconds(0.5f);
                Enemy_Speed = old_Speed;    // 이전 속도 값으로 복구
                enemyHit = true;
            }
        }
        else if (Enemy_HP <= 0 && Enemy_Mod != 3) // Enemy의 체력이 0과 같거나 이하일 때(죽음)
        {
            Dying = true;
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            animator.SetTrigger("Die");
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            yield return new WaitForSeconds(Enemy_Dying_anim_Time );
            enemyHit = false;
            if(Enemy_Mod == 9 && posi.localScale.y > 1f)   // 분열 몬스터일 경우
            {
                Debug.Log("분열 시작");
                StartCoroutine(Split());
                this.gameObject.SetActive(false);
            }       
            else if (Enemy_Mod == 9 && posi.localScale.y <= 1f)
            {
                Debug.Log("이제 없앨게");
                this.gameObject.SetActive(false);   // clone slime 제거
                //Destroy();
            }
            else if(Enemy_Mod != 9)
            {
                this.gameObject.SetActive(false);
            }
        }
        else if (Enemy_HP <= 0 && Enemy_Mod == 3) // 비행 몬스터 죽음)
        {
            Dying = true;
            this.gameObject.layer = LayerMask.NameToLayer("Dieenemy");
            Enemy_Speed = 0;
            old_Speed = Enemy_Speed;
            nextDirX = 0;
            for (int i = 0; i < 4; i++) 
            {
                // 스프라이트 블링크
                spriteRenderer.color = new Color(1, 1, 1, 0.4f);
                yield return new WaitForSeconds(0.1f);
                spriteRenderer.color = new Color(1, 1, 1, 1);
                yield return new WaitForSeconds(0.1f);
            }
            spriteRenderer.color = new Color(1, 1, 1, 0.4f);
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.gameObject.SetActive(false);
        }
        
    }


    void Sensing(Transform target, RaycastHit2D rayHit)  // 플레이어 추적
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        if (Gap_Distance_X <= Enemy_Sensing_X && Gap_Distance_Y <= Enemy_Sensing_Y)      // Enemy의 X축 사거리에 있을 때, Y축 사거리에 있을 때
        {
            if (transform.position.x < target.position.x)            // 오른쪽 방향
            {
                nextDirX = 1;
                if (Enemy_Mod != 3)  // 몬스터가 비행타입이 아닐 때
                {
                    if (nextDirX == 1 && rayHit.collider != null)  // nextDirX가 1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = true;
                        if(Enemy_Mod == 5)  // 자폭 몬스터가 자폭할 때 제자리에 있기 위한 코드
                        {
                            Enemy_Speed = 5f;
                            if (Attacking == true)
                            {
                                Enemy_Speed = 0f;
                            }
                        }
                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Debug.Log("dddddd");
                            Attacking = true;
                            if (Enemy_Mod == 5)
                            {
                                Attack();
                            }
                            else if (Enemy_Mod == 9)
                            {
                                Debug.Log("점프 공격 시작");
                                StartCoroutine(slimeJump());
                            }
                            else
                            {
                                Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                            }
                            
                        }
                    }
                    else if (nextDirX == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (0,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                        }
                    }
                }
                else if (nextDirX == 1 && Enemy_Mod == 3)  // 비행 몬스터의 플레이어 추적
                {
                    spriteRenderer.flipX = true;
                    Vector2 resHeight = new Vector2(-1.5f, 1f);
                    Vector2 playerPoint = (Vector2)target.transform.position + resHeight;   // 플레이어와 겹쳐서 공격하는 것을 방지하기 위해 새로운 지점을 정의함
                    transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);   // resHeight를 더해주어 플레이어의 아래에서 공격하지 않도록 했음
                    if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y)
                    {
                        Attacking = true;
                        Invoke("Attack", atkDelay); // 공격 쿨타임 적용

                    }
                }

            }
            else if (transform.position.x > target.position.x)      // 왼쪽 방향
            {
                nextDirX = -1;
                if (Enemy_Mod != 3) // 몬스터가 비행타입이 아닐 때
                {
                    if (nextDirX == -1 && rayHit.collider != null) // nextDirX가 -1일 때 그리고 레이캐스트 값이 null이 아닐 때
                    {
                        spriteRenderer.flipX = false;
                        if (Enemy_Mod == 5)
                        {
                            Enemy_Speed = 5f;
                            if (Attacking == true)
                            {
                                Enemy_Speed = 0f;
                            }
                        }
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Attacking = true;
                            if (Enemy_Mod == 5)
                            {
                                Attack();
                            }
                            else
                            {
                                Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                            }
                        }
                    }
                    else if (nextDirX == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy의 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && Enemy_Mod != 3)
                        {
                            Attacking = true;
                            Invoke("Attack", atkDelay); // 공격 쿨타임 적용
                        }
                    }
                }
                else if (nextDirX == -1 && Enemy_Mod == 3)  // 비행 몬스터의 플레이어 추적
                {
                    spriteRenderer.flipX = false;
                    Vector2 resHeight = new Vector2(1.5f, 1f);
                    Vector2 playerPoint = (Vector2)target.transform.position + resHeight;       // 플레이어와 겹쳐서 공격하는 것을 방지하기 위해 새로운 지점을 정의함(Vector2를 정의하여 +를 쓸 때 Vector2인지 3인지 모호하지 않게 함)
                    transform.position = Vector2.MoveTowards(transform.position, playerPoint, Enemy_Speed * Time.deltaTime);
                    if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && Attacking == false && target.position.y + 1 <= transform.position.y) // 타겟의 위치에 2.5f를 더해서 Bee가 플레이어의 아래쪽에서 공격하는 것을 방지
                    {
                        Attacking = true;
                        Invoke("Attack", atkDelay); // 공격 쿨타임 적용

                    }
                }
            }
        }
        else
        {
            if (Enemy_Mod != 3)
            {
                Move();     // Move 함수 실행
            }
        }
    }

    public void Sensor()    // 플랫폼 감지 함수
    {
        rigid = this.GetComponent<Rigidbody2D>();
        if (Enemy_Mod != 3)
        {
            // Enemy의 한 칸 앞의 값을 얻기 위해 자기 자신의 위치 값에 (x)에 + nextDirX값을 더하고 1.2f를 곱한다.
            Vector2 frontVec = new Vector2(rigid.position.x + nextDirX * 1.2f, rigid.position.y);

            Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0));

            // 레이저를 아래로 쏘아서 실질적인 레이저 생성(물리기반), LayMask.GetMask("")는 해당하는 레이어만 스캔함
            rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Tilemap", "Pad", "wall"));
            if (rayHit.collider == null)
            {
                Turn();
            }
        }
    }

    public void Attack() //공격 함수
    {
        Transform AtkTransform = transform.GetChild(0);
        animator = this.GetComponentInChildren<Animator>();
        Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // 본인 오브젝트의 첫번째 자식 오브젝트에 포함된 BoxCollider2D를 가져옴.
        spriteRenderer = this.gameObject.transform.GetChild(1).GetComponent<SpriteRenderer>();

        if (!Dying && Enemy_Mod == 3)
        {
            Bcollider.enabled = true;   // 공격 박스 콜라이더 생성

            if (spriteRenderer.flipX == true)   // 이미지 플립했을 때 공격 범위 x값 전환 조건문
            {
                AtkTransform.localPosition = new Vector3(atkX, atkY);   // 몬스터의 공격 콜라이더 박스의 x좌표와 y좌표
            }
            else if (spriteRenderer.flipX == false) // 왼쪽을 볼 때
            {
                AtkTransform.localPosition = new Vector3(-atkX, atkY);  // 몬스터의 공격 콜라이더 박스의 -x좌표와 y좌표
            }

            GiveDamage();       // 플레이어에게 데미지 주는 함수 실행
            animator.SetTrigger("Attack");  // 벌 공격용
            animator.SetBool("Attacking", true);    // 벌 공격 확인하는 용인 듯(너무 오래되서 기억 안 남)
            Enemy_Speed = 0;

            if (Attacking == true)
            {
                Invoke("offAttkack", 0.5f);
            }

        }
        else if(!Dying && Enemy_Mod == 5)
        {
            StartCoroutine(Boom());
        }

    }
    public void offAttkack() // 공격 종료 함수
    {
        Bcollider = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();    // 본인 오브젝트의 첫번째 자식 오브젝트에 포함된 BoxCollider2D를 가져옴. 
        Attacking = false;
        animator.SetBool("Attacking", false);
        Enemy_Speed = 3f;
        Bcollider.enabled = false;
    }
    
    public void GiveDamage()    // 플레이어에게 데미지를 주는 함수
    {
        posi = this.gameObject.transform.GetChild(0).GetComponent<Transform>();
        Box = this.gameObject.transform.GetChild(0).GetComponent<BoxCollider2D>();
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(posi.position, Box.size, 0);
        
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
            {
                collider.GetComponent<Player>().Playerhurt(Enemy_Power, Pos.position);
                Debug.Log("데미지 줌");

            }
            Debug.Log("데미지 못 줌");
        }

    }

    public void Bump()      // 충돌 데미지 함수
    {
        Collider2D[] collider2D = Physics2D.OverlapBoxAll(Pos.position, Boxs.size, 0);
        Player player = GetComponent<Player>();
        foreach (Collider2D collider in collider2D)
        {
            if (collider.tag == "Player" && collider != null)
                collider.GetComponent<Player>().Playerhurt(Bump_Power, Pos.position);
        }
    }

    public IEnumerator Boom()  // 폭발 함수
    {
        animator = this.gameObject.transform.GetChild(1).GetComponent<Animator>();

        animator.SetBool("Attacking", true);
        Attacking = true;
        yield return new WaitForSeconds(0.5f);
        GiveDamage();
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        this.gameObject.SetActive(false);
    }

    public IEnumerator Split()  // 슬라임 분열 함수
    {
        //posi = this.gameObject.GetComponent<Transform>();
        spawn = this.gameObject.transform.GetChild(2).GetComponent<Transform>();
        spawn2 = this.gameObject.transform.GetChild(3).GetComponent<Transform>();
        Debug.Log("분열 할게");
        GameObject splitSlime = Instantiate(Split_Slime, spawn.position, spawn.rotation);
        GameObject splitSlime2 = Instantiate(Split_Slime, spawn2.position, spawn2.rotation);
        yield return null;
    }

    public IEnumerator slimeJump() //슬라임 점프공격
    {
        yield return null;
        Debug.Log("점프 공격");

    }

    /*public void Destroy()
    {
        Debug.Log("아예 삭제");
        Destroy(Split_Slime); 

    }*/

}