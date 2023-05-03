using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump 높이 저장 변수
    public float Speed; //Move 속도 저장 변수
    public float curTime, coolTime = 2;  // 연속공격이 가능한 시간
    public bool isdelay = false;    //공격 딜레이 체크
    public bool isSlide = false;     //슬라이딩 체크
    public bool isGround = true;    //Player가 땅인지 아닌지 체크
    public bool isjump = false;     //점프중인지 체크
    public bool isSkill = false;    //스킬 확인
    public float delayTime = 1f;    //공격 딜레이 기본 시간
    public int WeaponChage = 1;     //무기 변경 저장 변수
    public int JumpCnt, JumpCount = 2;  //2단점프의 수를 카운터 해주는 변수
    public int SwdCnt, AxeCnt;  //공격모션의 순서
    public float Direction; //방향값
    public float attackDash = 5f; //큰 공격시 앞으로 이동하는 값
    public float slideSpeed = 13;   //슬라이딩 속도
    public int slideDir;    //슬라이딩 방향값
    public float Hp;
    public bool ishurt = false;
    public bool isknockback = false;

    public Vector2 boxSize; //공격 범위
    public GameObject arrow; //화살 오브젝트
    public Transform pos;
    public SpriteRenderer spriteRenderer;
    Rigidbody2D rigid;
    Animator anim;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //시작시 점프 가능 횟수 적용
        Speed = 4;  //시작시 기본 이동속도
        jumpPower = 17; //기본 점프높이
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
        if (!isdelay && Direction != 0 && gameObject.layer == 7)    //공격 딜레이중일시 이동 불가능
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
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide && !isjump)
        {
            StartCoroutine(Sliding());
        }


        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0 && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide"))
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
            isjump = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpCnt--;
            isGround = false;
        }
        /*if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("Player_Jump") && !anim.GetBool("Wall_slide") && JumpCnt > 0) //2단점프
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
        }*/

        //점프 Raycast 체크
        if (rigid.velocity.y < 0)   //Player 밑에 Tilemap이 닿을시 Jumping값 false
        {
            RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));

            if (rayHitDown.collider != null)
            {
                isGround = true;
                if (rayHitDown.distance > 0.5)
                {
                    anim.SetBool("Player_Jump", false);
                    JumpCnt = JumpCount;
                    isjump = false;
                }
            }
            //Debug.Log(rayHitDown.collider);
        }
    }

    void Player_Attack() //Player 공격
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            WeaponChage += 1;
            if (WeaponChage > 3)
                WeaponChage = 1;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            isSkill = true;
            StartCoroutine(Skill());
        }

            if (Input.GetKeyDown(KeyCode.A) && !anim.GetBool("Sliding"))
        {
            if (!isdelay)   //딜레이가 false일때 공격 가능
            {

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
                    anim.SetTrigger("arrow_atk");
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
            isSkill = true;
            StartCoroutine(arrow_delay());
            anim.SetTrigger("arrow_atk");
            Debug.Log(isSkill);
        }
        
    }

    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // 벽 콜라이젼이 Player에 닿고 있으면 실행
    {
        if (collision.gameObject.tag == "Wall" && !isGround)
        {
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)   //Player가 벽에 닿지 않을때
    {
        if (collision.gameObject.tag == "Wall" && Direction > 0) //왼쪽벽에 붙어서 떨어질때
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
        else if (collision.gameObject.tag == "Wall" && Direction < 0) //오른쪽벽에 붙어서 떨어질때
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }

        if (collision.gameObject.tag == "Wall" && Input.GetKey(KeyCode.Space)) //벽에서 점프시 반대로 팅겨감
        {
            if (Direction > 0)
                rigid.velocity = new Vector2(1, 1) * 8f;
            if (Direction < 0)
                rigid.velocity = new Vector2(-1, 1) * 8f;
        }

    }

    private IEnumerator Sliding() //슬라이딩 실행
    {
        yield return null;
        Speed = 0;
        isSlide = true;
        gameObject.layer = 6;
        anim.SetBool("Sliding", true);
        if (slideDir == 1) //오른쪽으로 슬라이딩
            rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
        if (slideDir == -1) //왼쪽으로 슬라이딩
            rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
        yield return new WaitForSeconds(0.5f); //무적 시간
        anim.SetBool("Sliding", false);
        gameObject.layer = 7;
        Speed = 4;
        yield return new WaitForSeconds(2f); //슬라이딩 쿨타임
        isSlide = false;

    }

    void AttackDamage() //공격 대미지
    {
        Collider2D[] collider2Ds = Physics2D.OverlapBoxAll(pos.position, boxSize, 0);
        foreach (Collider2D collider in collider2Ds)
        {
            if (collider.tag == "Enemy")
            {
                collider.GetComponent<Enemy>().EnemyHurt(1, pos.position);
                Debug.Log("1");
            } 
        }
    }

    public void Playerhurt(int Damage) // Player가 공격받을 시
    {
        if (!ishurt)
        {
            ishurt = true;
            Hp = Hp - Damage;

            if (Hp <= 0)
            {
                Invoke("Die", 3f);
            }
            else
            {
                anim.SetTrigger("hurt");
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
    }

    IEnumerator arrow_delay() //화살공격시 나가는 시간 조절 - 애니메이션과 맞춰주기 위해
    {
        if(isSkill)
            yield return new WaitForSeconds(0.7f);
        else
            yield return new WaitForSeconds(0.5f);
        if (slideDir == 1)
        {
            if(!isSkill)
                rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime);
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
        Instantiate(arrow, pos.position, transform.rotation);
        isSkill = false;
    }

    IEnumerator Dash_delay() //공격시 약간의 딜레이후 앞으로 조금 이동
    {
        if (SwdCnt == 2)
        {
            yield return new WaitForSeconds(0.5f);
            boxSize.x = 2.3f;
        }
        else if (AxeCnt == 2)
        {
            yield return new WaitForSeconds(0.8f);
            boxSize.x = 2.4f;
        }
        else
        {
            yield return new WaitForSeconds(1f);
            boxSize.x = 3.2f;
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
        boxSize.x = 1.3f;
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

    void Die() //Player 사망시 스프라이트 삭제
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()//공격 범위 박스 표시
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(pos.position, boxSize);
    }
}
