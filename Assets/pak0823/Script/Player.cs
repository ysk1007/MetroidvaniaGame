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
    public float attackDash = 5f; //큰 공격시 앞으로 이동하는 값
    public float slideSpeed = 13;   //슬라이딩 속도
    public int slideDir;    //슬라이딩 방향값
    public float MaxHp;    //플레이어 최대 HP
    public float CurrentHp;    //플레이어 현재 HP
    public bool ishurt = false; //피격 확인
    public bool isknockback = false;    //넉백 확인
    public float Dmg = 7;
    public GameObject GameManager;
    public GameObject attackRange;

    public BoxCollider2D box; //공격 범위
    public GameObject Arrow; //화살 오브젝트
    public Transform pos;   //공격박스 위치
    public SpriteRenderer spriteRenderer;
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
        pos = GetComponent<Transform>(); // pos 변수에 Transform 컴포넌트 할당
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
            transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
            anim.SetBool("Player_Walk", true);

            if (Direction < 0) //왼쪽 바라보기
            {
                transform.eulerAngles = new Vector2(0, 180);
                slideDir = -1;
            }
            else if (Direction > 0) //오른쪽 바라보기
            {
                transform.eulerAngles = new Vector2(0, 0);
                slideDir = 1;
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
            if (Input.GetKeyDown(KeyCode.Space))
                StartCoroutine(DownJump());
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
            gameObject.layer = LayerMask.NameToLayer("Jump");
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
            if (WeaponChage == 2)
            {
                attackRange.tag = "Axe";
                Dmg = 10;
            }
            else if (WeaponChage == 3)
            {
                attackRange.tag = "Arrow";
            }
            else
            {
                Dmg = 7;
                attackRange.tag = "Sword";
                WeaponChage = 1;
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
                //this.attackRange.gameObject.SetActive(true);
                if (WeaponChage == 1)    //Sword 공격
                {

                    if (curTime > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
                        SwdCnt++;
                    else
                        SwdCnt = 1;

                    isdelay = true;
                    anim.SetFloat("Sword", SwdCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
                    anim.SetTrigger("sword_atk");

                    if (SwdCnt > 1)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
                    {
                        StartCoroutine(Dash_delay());
                        SwdCnt = 0;
                    }
                    else
                        AttackDamage();

                    curTime = coolTime;
                }
                if (WeaponChage == 2)    //Axe 공격
                {
                    if (curTime > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
                        AxeCnt++;
                    else
                        AxeCnt = 1;

                    if (AxeCnt == 2)    //동작별 딜레이타임 추가
                    {
                        delayTime += 0.2f;
                        StartCoroutine(Dash_delay());
                    }
                    else if (AxeCnt == 3)
                    {
                        delayTime += 0.55f;
                        StartCoroutine(Dash_delay());
                    }
                    else
                        AttackDamage();

                    isdelay = true;
                    anim.SetFloat("Axe", AxeCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
                    anim.SetTrigger("axe_atk");

                    if (AxeCnt > 2)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
                        AxeCnt = 0;

                    curTime = coolTime + 0.5f;
                }
                if (WeaponChage == 3)    //Bow 공격
                {
                    isdelay = true;
                    StartCoroutine(arrow_delay());
                }
                StartCoroutine(attack_delay());    //공격후 다음 공격까지 딜레이
            }
        }
        else
            curTime -= Time.deltaTime;
    }

    IEnumerator Skill()
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
            StartCoroutine(arrow_delay());
            anim.SetTrigger("arrow_atk");
            StartCoroutine(SkillTime());
        }

    }

    public void AttackDamage()
    {
        box = transform.GetChild(1).GetComponentInChildren<BoxCollider2D>();
        if (box != null)
        {
            Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, box.size, 0);
            foreach (Collider2D collider in collider2Ds)
            {
                if (collider != null && collider.tag == "Enemy")
                {
                    enemy = collider.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        StartCoroutine(enemy.Hit(Dmg));
                        Debug.Log(Dmg + "Player");
                    }
                }
            }
        }
    }


    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // 벽 콜라이젼이 Player에 닿고 있으면 실행, 점프착지 시 콜라이젼 닿을 시 점프 해제
    {
        RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1f, LayerMask.GetMask("Tilemap", "Pad"));
        Debug.DrawRay(rigid.position, Vector3.down * 1f, Color.red);

        if (collision.gameObject.tag == "Wall" && !isGround)
        {
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
        }
        if (collision.gameObject.tag == "Pad" || collision.gameObject.tag == "Tilemap" && !isGround)
        {
            anim.SetBool("Player_Jump", false);
            isjump = false;
            isGround = true;
        }
        if (rayHitDown.collider != null)
        {
            JumpCnt = JumpCount;
            gameObject.layer = LayerMask.NameToLayer("Player");
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
        anim.SetBool("Sliding", true);
        if (slideDir == 1) //오른쪽으로 슬라이딩
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
        if (slideDir == -1) //왼쪽으로 슬라이딩
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
        yield return new WaitForSeconds(0.5f); //무적 시간
        anim.SetBool("Sliding", false);
        gameObject.tag = "Player";
        Speed = 4;
        yield return new WaitForSeconds(2f); //슬라이딩 쿨타임
        isSlide = false;

    }


    public void Playerhurt(float Damage) // Player가 공격받을 시
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
                StartCoroutine(Knockback());
                StartCoroutine(Routine());
                StartCoroutine(Blink());
            }
        }

    }

    IEnumerator attack_delay() //연속공격 딜레이
    {
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
        //this.attackRange.gameObject.SetActive(false);
    }

    IEnumerator arrow_delay() //화살공격시 나가는 시간 조절 - 애니메이션과 맞춰주기 위해
    {
        anim.SetTrigger("arrow_atk");
        if (isSkill)
            yield return new WaitForSeconds(0.7f);
        else
            yield return new WaitForSeconds(0.5f);
        if (slideDir == 1)  //플레이어가 바라보는 방향 왼쪽
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime); // 활 공격시 약간의 뒤로 밀림
            else
                rigid.velocity = new Vector2(transform.localScale.x - 10f, Time.deltaTime);
        }
        else
        {
            if (!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
            else
                rigid.velocity = new Vector2(transform.localScale.x + 10f, Time.deltaTime);
        }
        Instantiate(Arrow, pos.position, transform.rotation);

    }

    IEnumerator SkillTime() //스킬 종료 시간
    {
        yield return new WaitForSeconds(1.3f);
        isSkill = false;
    }

    IEnumerator Dash_delay() //공격시 약간의 딜레이후 앞으로 조금 이동
    {
        if (SwdCnt == 2)
        {
            yield return new WaitForSeconds(0.5f);
            box.size = new Vector2(3, 2);
        }
        else if (AxeCnt == 2)
        {
            yield return new WaitForSeconds(0.8f);
            box.size = new Vector2(3, 2);
        }
        else
        {
            yield return new WaitForSeconds(1f);
            box.size = new Vector2(4, 2);
        }

        if (slideDir == -1)
        {
            rigid.velocity = new Vector2(transform.localScale.x - attackDash, Time.deltaTime);
        }
        else
        {
            rigid.velocity = new Vector2(transform.localScale.x + attackDash, Time.deltaTime);
        }

        AttackDamage();
        box.size = new Vector2(2, 2);
    }

    IEnumerator Knockback() //피해입을시 넉백
    {
        isknockback = true;
        float ctime = 0;

        while (ctime < 0.2f) //넉백 지속시간
        {
            if (slideDir == -1)
                transform.Translate(Vector2.left * Speed * 2 * Time.deltaTime);
            else
                transform.Translate(Vector2.right * Speed * 2 * Time.deltaTime * -1);

            ctime += Time.deltaTime;
            yield return null;
        }
        isknockback = false;
    }

    IEnumerator Routine() // 피해입을시 잠깐동안 무적
    {
        yield return new WaitForSeconds(2f);
        ishurt = false;
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