using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump 높이 저장 변수
    public float maxSpeed; //Move 최고속도 저장 변수
    public float curTime,coolTime = 2;  //검 연속공격이 가능한 시간
    public float curTime2, coolTime2 = 2.5f;  //도끼 연속공격이 가능한 시간
    public int JumpCnt, JumpCount = 2;  //2단점프의 수를 카운터 해주는 변수
    public int SwdCnt,AxeCnt;  //공격모션의 순서
    public bool isdelay = false;    //공격 딜레이 체크
    public bool isSlide = false;     //슬라이딩 체크
    public bool isGround = true;    //Player가 땅인지 아닌지 체크
    public float delayTime = 1f;    //공격 딜레이 기본 시간
    public int filp;

    Rigidbody2D rigid;
    CapsuleCollider2D capsuleCollider;
    SpriteRenderer spriteRenderer;
    Animator anim;
    Transform trans;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        trans = GetComponent<Transform>();
        JumpCnt = JumpCount;    //시작시 점프 가능 횟수 적용
        maxSpeed = 4;  //시작시 기본 이동속도
        jumpPower = 15; //기본 점프높이
    }
    void Update()
    {
        Player_Move();  //Player의 이동, 점프, 속도 함수
        Player_Attack();    //Player의 공격 함수
    }

    void Player_Move()
    {
        
        //Move
        float h = Input.GetAxisRaw("Horizontal");   // 방향값을 정수로 가져오기

        Vector2 target = new Vector2(trans.position.x + (maxSpeed * h), trans.position.y);

        //Player 방향전환
        if (h < 0) //왼쪽 바라보기
        {
            spriteRenderer.flipX = false;
            anim.SetBool("Player_Walk", true);
            transform.Translate(new Vector2(h, 0) * maxSpeed * Time.deltaTime);
            filp = -1;
        }
        else if (h > 0) //오른쪽 바라보기
        {
            spriteRenderer.flipX = true;
            anim.SetBool("Player_Walk", true);
            transform.Translate(new Vector2(h, 0) * maxSpeed * Time.deltaTime);
            filp = 1;
        }
        else
            anim.SetBool("Player_Walk", false);
        

        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Player_Jump") && JumpCnt > 0 )
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
        }
        if (Input.GetButtonDown("Jump") && anim.GetBool("Player_Jump") && JumpCnt > 0)
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
        }
        if(Input.GetButtonDown("Jump"))
        {
            JumpCnt--;
            isGround = false;
        }

        //Slide
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide && isGround)
        {
            
            isSlide = true;
            anim.SetBool("Sliding", true);
            gameObject.layer = 6;
            maxSpeed *= 6;
            if (h == 0)
            {
                //transform.Translate(new Vector2(filp, 0) * maxSpeed * Time.deltaTime);
                //transform.Translate(Vector2.MoveTowards(transform.position, target, maxSpeed));
            }
            else
                //transform.Translate(Vector2.MoveTowards(transform.position, target*h, maxSpeed));
            //transform.Translate(new Vector2(h, 0) * maxSpeed * Time.deltaTime);

            StartCoroutine(slide_delay());
        }

        RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        //Landing Platform, 스프라이트 밑에 Tilemap이 닿을시 Jumping값 false
        if (rigid.velocity.y < 0)
        {
            if (rayHitDown.collider != null)
            {
                isGround = true;
                if (rayHitDown.distance > 0.5)
                {
                    anim.SetBool("Player_Jump", false);
                    JumpCnt = JumpCount;
                }
            }
            Debug.Log(rayHitDown.collider);
        }
    }

    void Player_Attack() //Player 공격
    {  
        if (Input.GetKeyDown(KeyCode.J)) //임시 Sword공격
        {
           

            if (!isdelay)   //딜레이가 false일때 공격 가능
            {
                if (curTime > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
                    SwdCnt++;
                else
                    SwdCnt = 1;

                isdelay = true;
                anim.SetFloat("Sword", SwdCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
                anim.SetTrigger("sword_atk");

                if (SwdCnt > 1)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
                    SwdCnt = 0;

                StartCoroutine(sword_delay());    //공격후 다음 공격까지 딜레이
                maxSpeed = 4;
            }
            else
                

            curTime = coolTime;
            
        }
        else
            curTime -= Time.deltaTime;
           


        if (Input.GetKeyDown(KeyCode.M))    //임시 Axe공격
        {
            maxSpeed = 0;
            if (!isdelay)   //딜레이가 false일때 공격 가능
            {
                if (curTime2 > 0)    //첫번째 공격후 쿨타임 내에 공격시 강공격 발동
                    AxeCnt++;
                else
                    AxeCnt = 1;

                isdelay = true;
                anim.SetFloat("Axe", AxeCnt); //Blend를 이용해 연속공격의 애니메이션 순차적 실행
                anim.SetTrigger("axe_atk");

                if (AxeCnt > 2)     //연속공격이 끝난후 다시 첫번째 공격값으로 변경
                    AxeCnt = 0;

                StartCoroutine(axe_delay());    //공격후 다음 공격까지 딜레이
            }
            else
                maxSpeed = 4;

            curTime2 = coolTime2;
        }
        else
            curTime2 -= Time.deltaTime;
    }

    IEnumerator sword_delay() //Sword 연속공격 딜레이
    {
        yield return new WaitForSeconds(delayTime);
        isdelay = false;
    }

    IEnumerator axe_delay() //Axe 연속공격 딜레이
    {
        yield return new WaitForSeconds(delayTime+0.5f);
        isdelay = false;
    }

    IEnumerator slide_delay()
    {
        yield return new WaitForSeconds(0.2f);
        maxSpeed = 4;
        Debug.Log(maxSpeed);
        anim.SetBool("Sliding", false);
        gameObject.layer = 7;

        yield return new WaitForSeconds(2f);
        isSlide = false;
    }
}
