using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump 높이 저장 변수
    public float Speed; //Move 속도 저장 변수
    public float curTime, coolTime = 2;  // 연속공격이 가능한 시간
    public float skcurTime, skcoolTime = 5;  // 스킬 쿨타임
    public bool isdelay = false;    //공격 딜레이 체크
    public bool isSlide = false;     //슬라이딩 체크
    public bool isGround = true;    //Player가 땅인지 아닌지 체크
    public bool isjump = false;     //점프중인지 체크
    public bool isSkill = false;    //스킬 확인
    public bool isAttacking = false; //공격상태 확인
    public float delayTime = 1f;    //공격 딜레이 기본 시간
    public int WeaponChage = 1;     //무기 변경 저장 변수
    public int JumpCnt, JumpCount = 2;  //2단점프의 수를 카운터 해주는 변수
    public int SwdCnt, AxeCnt;  //공격모션의 순서
    public float Direction; //방향값
    public float attackDash = 4f; //큰 공격시 앞으로 이동하는 값
    public float slideSpeed = 13;   //슬라이딩 속도
    public int slideDir = 1;    //슬라이딩 방향값
    public float MaxHp;    //플레이어 최대 HP
    public float CurrentHp;    //플레이어 현재 HP
    public bool ishurt = false; //피격 확인
    public bool isknockback = false;    //넉백 확인
    public float Dmg = 7;
    public GameObject GameManager;
    public GameObject attackRange;

    private Enemy enemyarrow;
    private Arrow arrow;

    public BoxCollider2D box; //공격 범위
    public Transform pos;   //공격박스 위치
    public GameObject Arrow; //화살 오브젝트
    public Transform Arrowpos;
    public SpriteRenderer spriteRenderer;
    private SpriteRenderer AttackRennderer;
    Rigidbody2D rigid;
    Animator anim;
    public Enemy enemy;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //시작시 점프 가능 횟수 적용
        Speed = 4;  //시작시 기본 이동속도
        jumpPower = 17; //기본 점프높이
        pos = transform.GetChild(1).GetComponentInChildren<Transform>(); //attackRange의 위치값을 pos에 저장
        Arrowpos = transform.GetChild(0).GetComponentInChildren<Transform>(); //attackRange의 위치값을 pos에 저장
        arrow = GetComponent<Arrow>();
    }
    void Update()
    {
        Player_Move();  //Player의 이동, 점프, 속도 함수
        Player_Attack();    //Player의 공격 함수

    }

    void Player_Move() //Player 이동, 점프
    {
        //Move
        Direction = Input.GetAxisRaw("Horizontal");   // 좌우 방향값을 정수로 가져오기
        if (!isdelay && Direction != 0 && gameObject.CompareTag("Player") && !isSkill)    //공격 딜레이중일시 이동 불가능
        {
            Transform AtkRangeTransform = transform.GetChild(1);   // AttackRange 위치값 변경을 위해 자식오브젝트 위치값 불러옴
            anim.SetBool("Player_Walk", true);
            if (Direction < 0) //왼쪽 바라보기
            {
                spriteRenderer.flipX = false;
                transform.Translate(new Vector2(-1, 0) * Speed * Time.deltaTime);
                slideDir = -1;
                AtkRangeTransform.localPosition = new Vector3(-3, 0); // AttackRange 위치값 변경
            }
            else if (Direction > 0) //오른쪽 바라보기
            {
                spriteRenderer.flipX = true;
                transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
                slideDir = 1;
                AtkRangeTransform.localPosition = new Vector3(0, 0);
            }
        }
        else
            anim.SetBool("Player_Walk", false);

        //Sliding
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide && !isjump && !isdelay && !isSkill)
        {
            StartCoroutine(Sliding());
        }

        //Jump
        if (Input.GetKey(KeyCode.DownArrow) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide")) //발판에서 밑으로 점프시 내려가기
        {
            if(Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(DownJump());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
            //gameObject.layer = LayerMask.NameToLayer("Jump");
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            JumpCnt--;
            isGround = false;
        }

        /*if (transform.position.y < -10)
         {
            PlayerReposition();
         }*/

    }

    public void Player_Attack() //Player 공격모음
    {
        if (Input.GetKeyDown(KeyCode.Tab) && GameManager.GetComponent<WeaponSwap>().swaping != true)
        {
            WeaponChage += 1;      
            // Sword Dmg = 7, Axe Dmg = 10, Arrow Dmg = 5
            if(WeaponChage == 2)
            {
                attackRange.tag = "Axe";
                Dmg = 10;
            }
            else if(WeaponChage == 3)
            {
                attackRange.tag = "Arrow";
                this.transform.GetChild(1).gameObject.SetActive(false);
            }
            else
            {
                Dmg = 7;
                attackRange.tag = "Sword";
                WeaponChage = 1;
                this.transform.GetChild(1).gameObject.SetActive(true);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && skcurTime <= 0 && !anim.GetBool("Sliding"))
        {
            skcurTime = skcoolTime;
            isSkill = true;
            StartCoroutine(Skill());
        }
        else
            skcurTime -= Time.deltaTime;

        if (Input.GetKey(KeyCode.A) && !anim.GetBool("Sliding") && !isSkill)
        {
            if (!isdelay)   //딜레이가 false일때 공격 가능
            {
                if (WeaponChage == 1)    //Sword 공격
                {
                    Sword_attack();
                }
                if (WeaponChage == 2)    //Axe 공격
                {
                    Axe_attack();
                }
                if (WeaponChage == 3)    //Bow 공격
                {
                    isdelay = true;
                    anim.SetTrigger("arrow_atk");
                }

                StartCoroutine(Attack_delay());    //공격후 다음 공격까지 딜레이
            }
        }
        else
            curTime -= Time.deltaTime;
    }

    public void AttackDamage()// Player 공격시 적에게 대미지값 넘겨주기
    {
        box = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();
        if (box != null)    //공격 범위 안에 null값이 아닐때만
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, box.size, 0); //공격 범위 안에 콜라이더를 
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && collider.tag == "Enemy")
                {
                    enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        //StopCoroutine(enemy.Hit(Dmg));
                        StartCoroutine(enemy.Hit(Dmg));
                        //enemy.Hit(Dmg);
                        Debug.Log(Dmg + "Player");
                    }
                }
            }
        }
    } 
    IEnumerator Skill()//스킬 작동시 실행(아직 수정중)
    {
        yield return null;
        if (WeaponChage == 1) //sword 스킬
        {

        }
        if (WeaponChage == 2) //Axe 스킬
        {

        }
        if (WeaponChage == 3) //Arrow 스킬
        {
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillTime());
        }

    }    

    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // 벽 콜라이젼이 Player에 닿고 있으면 실행, 점프착지 시 콜라이젼 닿을 시 점프 해제
    {
        RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Tilemap", "Pad"));
        //Debug.DrawRay(rigid.position, Vector3.down * 1f, Color.red);

        if (collision.gameObject.tag == "Wall" && !isGround)
        {
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
        }
        if(collision.gameObject.tag == "Pad" || collision.gameObject.tag == "Tilemap" && !isGround)
        {
            anim.SetBool("Player_Jump", false);
            isjump = false;
            isGround = true;
        }
        if (rayHitDown.collider != null && !anim.GetBool("Sliding"))
        {
            JumpCnt = JumpCount;
            //gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)   //Player가 벽에 닿지 않을때
    {
        if (collision.gameObject.tag == "Wall") //왼쪽벽에 붙어서 떨어질때
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
       

        if (collision.gameObject.tag == "Wall" && Input.GetKey(KeyCode.Space)) //벽에서 점프시 반대로 팅겨감
        {
            if (Direction > 0)
                rigid.velocity = new Vector2(1, 1) * 10f;
            if (Direction < 0)
                rigid.velocity = new Vector2(-1, 1) * 10f;
        }

    }

    private IEnumerator Sliding() //슬라이딩 실행
    {
        GameManager.GetComponent<Ui_Controller>().Sliding();
        Speed = 0;
        isSlide = true;
        gameObject.tag = "Sliding";
        gameObject.layer = LayerMask.NameToLayer("Sliding");
        anim.SetBool("Sliding", true);
        if (slideDir == 1) //오른쪽으로 슬라이딩
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
        if (slideDir == -1) //왼쪽으로 슬라이딩
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
        yield return new WaitForSeconds(0.5f); //무적 시간
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
        Speed = 4;
        yield return new WaitForSeconds(2f); //슬라이딩 쿨타임
        isSlide = false;

    }

    public void Playerhurt(float Damage, Vector2 pos) // Player가 공격받을 시
    {
        if (!ishurt)
        {
            ishurt = true;
            CurrentHp = CurrentHp - Damage;

            if (CurrentHp <= 0)
            {
                Invoke("Die", 3f);
            }
            else
            {
                anim.SetTrigger("hurt");
                GameManager.GetComponent<Ui_Controller>().Damage(Damage);
                float x = transform.position.x - pos.x;
                if (transform.position.x > pos.x)
                    x = 1;
                else
                    x = -1;
                StartCoroutine(Knockback(x));
                StartCoroutine(Routine());
                StartCoroutine(Blink());
            }
        }

    }

    IEnumerator Attack_delay() //연속공격 딜레이
    {
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
    }

    void Sword_attack() //Sword 공격 관련 정보
    {
        if (curTime > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
        {
            SwdCnt++;
            Dmg = 10;
        }
        else
        {
            SwdCnt = 1;
            Dmg = 7;
        }

        if (SwdCnt == 1)
        {

            box.size = new Vector2(3.5f, 2.5f);
            box.offset = new Vector2(1.5f, 0);
        }
        else
        {
            box.size = new Vector2(4f, 2.5f);
            if (slideDir == 1)  //공격 방향별 box.offset값을 다르게 적용
                box.offset = new Vector2(2, 0);
            else
                box.offset = new Vector2(1, 0);

        }

        //첫번째 공격 대미지 함수 실행은 애니메이션 부분에 들어있음
        isdelay = true;
        anim.SetFloat("Sword", SwdCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
        anim.SetTrigger("sword_atk");

        if (SwdCnt > 1)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
            SwdCnt = 0;
        curTime = coolTime; // 콤보 공격 제한시간
    }   

    void Axe_attack()   //Axe 공격 관련 정보
    {
        if (curTime > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
            AxeCnt++;
        else
            AxeCnt = 1;


        if(AxeCnt == 1) // 동작별 대미지 변경
        {
            Dmg = 10;
            box.size = new Vector2(5f, 2.5f);
            if(slideDir == 1)   //공격 방향별 box.offset값을 다르게 적용
                box.offset = new Vector2(2, 0);
            else
                box.offset = new Vector2(1, 0);
        }
        else if (AxeCnt == 2)    
        {
            Dmg = 15;
            box.size = new Vector2(4.5f, 2.5f);
            if (slideDir == 1)
                box.offset = new Vector2(2.5f, 0);
            else
                box.offset = new Vector2(0.5f, 0);
            
        }
        else if (AxeCnt == 3)
        {
            Dmg = 20;
            attackDash = 6;
            box.size = new Vector2(5.5f, 2.5f);
            if (slideDir == 1)
                box.offset = new Vector2(3.5f, 0);
            else
                box.offset = new Vector2(-0.5f, 0); 
        }
            

        isdelay = true;
        anim.SetFloat("Axe", AxeCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
        anim.SetTrigger("axe_atk");

        if (AxeCnt > 2)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
            AxeCnt = 0;

        curTime = coolTime + 0.5f;  // 콤보 공격 제한시간
    }       

    IEnumerator Arrow_attack() //화살 일반공격 및 스킬 - 애니메이션 특정 부분에서 실행되게 유니티에서 설정함
    {
        yield return null;
        Transform ArrowposTransform = transform.GetChild(0);
        if (slideDir == 1)   //공격 방향별 Arrowpos 위치값 변경
            ArrowposTransform.localPosition = new Vector3(1, 0.2f); 
        else
            ArrowposTransform.localPosition = new Vector3(-1, 0.2f); 

        Instantiate(Arrow, Arrowpos.position, transform.rotation);
        if (slideDir == 1)  //플레이어가 바라보는 방향 왼쪽
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime); // 활 공격시 약간의 뒤로 밀림
            else
                rigid.velocity = new Vector2(transform.localScale.x - 10f, Time.deltaTime);
        }
        else  //플레이어가 바라보는 방향 오른쪽
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
            else
                rigid.velocity = new Vector2(transform.localScale.x + 10f, Time.deltaTime);
        }
    }

    IEnumerator SkillTime() //스킬 종료 시간
    {
        yield return new WaitForSeconds(1.3f);
        isSkill = false;
    }

    IEnumerator Dash() //일부 공격시 앞으로 대쉬 이동 - 애니메이션 특정 부분에서 실행되게 유니티에서 설정함
    {
        yield return null;
        if (slideDir == -1)
        {
            rigid.velocity = new Vector2(transform.localScale.x - attackDash, Time.deltaTime);
        }
        else
        {
            rigid.velocity = new Vector2(transform.localScale.x + attackDash, Time.deltaTime);
        }
    }

    IEnumerator Knockback(float dir) //피해입을시 넉백
    {
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.4f) //넉백 지속시간
        {
            Vector2 vector2 = new Vector2(dir, 1);
            transform.Translate(vector2.normalized * Speed * 3 * Time.deltaTime);
            ctime += Time.deltaTime;
            yield return null;
        }
        isknockback = false;
    }

    IEnumerator Routine() // 피해입을시 잠깐동안 무적
    {
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        yield return new WaitForSeconds(2f);
        ishurt = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    IEnumerator Blink() // 무적시간동안 투명 효과
    {
        while (ishurt)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(2f);
        }
        spriteRenderer.color = new Color(1, 1, 1, 1f);
    }

    IEnumerator DownJump()
    {
        anim.SetBool("Player_Jump", true);
        gameObject.layer = LayerMask.NameToLayer("Jump");
        yield return new WaitForSeconds(0.3f);
        gameObject.layer = LayerMask.NameToLayer("Player");
    } //블록 아래로 내려가기

    void Die() //Player 사망시 스프라이트 삭제
    {
        Destroy(gameObject);
    }

    void PlayerReposition()
    {
        transform.position = new Vector3(-30, -7.5f, 0);
    }
}