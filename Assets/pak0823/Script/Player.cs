using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump ���� ���� ����
    public float maxSpeed; //Move �ְ�ӵ� ���� ����
    public float curTime,coolTime = 2;  //�� ���Ӱ����� ������ �ð�
    public float curTime2, coolTime2 = 2.5f;  //���� ���Ӱ����� ������ �ð�
    public int JumpCnt, JumpCount = 2;  //2�������� ���� ī���� ���ִ� ����
    public int SwdCnt,AxeCnt;  //���ݸ���� ����
    public bool isdelay = false;    //���� ������ üũ
    public bool isSlide = false;     //�����̵� üũ
    public bool isGround = true;    //Player�� ������ �ƴ��� üũ
    public float delayTime = 1f;    //���� ������ �⺻ �ð�
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
        JumpCnt = JumpCount;    //���۽� ���� ���� Ƚ�� ����
        maxSpeed = 4;  //���۽� �⺻ �̵��ӵ�
        jumpPower = 15; //�⺻ ��������
    }
    void Update()
    {
        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�
    }

    void Player_Move()
    {
        
        //Move
        float h = Input.GetAxisRaw("Horizontal");   // ���Ⱚ�� ������ ��������

        Vector2 target = new Vector2(trans.position.x + (maxSpeed * h), trans.position.y);

        //Player ������ȯ
        if (h < 0) //���� �ٶ󺸱�
        {
            spriteRenderer.flipX = false;
            anim.SetBool("Player_Walk", true);
            transform.Translate(new Vector2(h, 0) * maxSpeed * Time.deltaTime);
            filp = -1;
        }
        else if (h > 0) //������ �ٶ󺸱�
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
        //Landing Platform, ��������Ʈ �ؿ� Tilemap�� ������ Jumping�� false
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

    void Player_Attack() //Player ����
    {  
        if (Input.GetKeyDown(KeyCode.J)) //�ӽ� Sword����
        {
           

            if (!isdelay)   //�����̰� false�϶� ���� ����
            {
                if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
                    SwdCnt++;
                else
                    SwdCnt = 1;

                isdelay = true;
                anim.SetFloat("Sword", SwdCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
                anim.SetTrigger("sword_atk");

                if (SwdCnt > 1)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                    SwdCnt = 0;

                StartCoroutine(sword_delay());    //������ ���� ���ݱ��� ������
                maxSpeed = 4;
            }
            else
                

            curTime = coolTime;
            
        }
        else
            curTime -= Time.deltaTime;
           


        if (Input.GetKeyDown(KeyCode.M))    //�ӽ� Axe����
        {
            maxSpeed = 0;
            if (!isdelay)   //�����̰� false�϶� ���� ����
            {
                if (curTime2 > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
                    AxeCnt++;
                else
                    AxeCnt = 1;

                isdelay = true;
                anim.SetFloat("Axe", AxeCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
                anim.SetTrigger("axe_atk");

                if (AxeCnt > 2)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                    AxeCnt = 0;

                StartCoroutine(axe_delay());    //������ ���� ���ݱ��� ������
            }
            else
                maxSpeed = 4;

            curTime2 = coolTime2;
        }
        else
            curTime2 -= Time.deltaTime;
    }

    IEnumerator sword_delay() //Sword ���Ӱ��� ������
    {
        yield return new WaitForSeconds(delayTime);
        isdelay = false;
    }

    IEnumerator axe_delay() //Axe ���Ӱ��� ������
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
