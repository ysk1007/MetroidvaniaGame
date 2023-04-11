using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump ���� ���� ����
    public float Speed; //Move �ӵ� ���� ����
    public float curTime,coolTime = 2;  // ���Ӱ����� ������ �ð�
    public bool isdelay = false;    //���� ������ üũ
    public bool isSlide = false;     //�����̵� üũ
    public bool isGround = true;    //Player�� ������ �ƴ��� üũ
    public bool isjump = false;     //���������� üũ
    public float delayTime = 1f;    //���� ������ �⺻ �ð�
    public int WeaponChage = 1;     //���� ���� ���� ����
    public int JumpCnt, JumpCount = 2;  //2�������� ���� ī���� ���ִ� ����
    public int SwdCnt, AxeCnt;  //���ݸ���� ����
    public float h; //���Ⱚ
    public float attackDash = 5f; //ū ���ݽ� ������ �̵��ϴ� ��

    public float slideSpeed = 13;   //�����̵� �ӵ�
    public int slideDir;    //�����̵� ���Ⱚ

    public GameObject arrow;
    public Transform pos;
    public SpriteRenderer spriteRenderer;

    Rigidbody2D rigid;
    CapsuleCollider2D capsuleCollider;
    Animator anim;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpCnt = JumpCount;    //���۽� ���� ���� Ƚ�� ����
        Speed = 4;  //���۽� �⺻ �̵��ӵ�
        jumpPower = 17; //�⺻ ��������
    }

    void Update()
    {
        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�

    }

    void Player_Move() //Player �̵�, ����
    {
        //Move
        h = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if(!isdelay && h != 0 && gameObject.layer == 7)    //���� ���������Ͻ� �̵� �Ұ���
        {
            transform.Translate(new Vector2(1, 0) * Speed * Time.deltaTime);
            anim.SetBool("Player_Walk", true);

            if (h < 0) //���� �ٶ󺸱�
            {
                transform.eulerAngles = new Vector2(0, 180);
                slideDir = -1;
            }
            else if (h > 0) //������ �ٶ󺸱�
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
        if(Input.GetKeyDown(KeyCode.Space))
        {
            JumpCnt--;
            isGround = false;
        }
        /*if (Input.GetKeyDown(KeyCode.Space) && anim.GetBool("Player_Jump") && !anim.GetBool("Wall_slide") && JumpCnt > 0) //2������
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
        }*/

        //���� Raycast üũ
        if (rigid.velocity.y < 0)   //Player �ؿ� Tilemap�� ������ Jumping�� false
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

    void Player_Attack() //Player ����
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            WeaponChage += 1;
            if (WeaponChage > 3)
                WeaponChage = 1;

            //Debug.Log(WeaponChage);
        }
            
        //Sword ����
        if (Input.GetKeyDown(KeyCode.A) && !anim.GetBool("Sliding"))
        {
            if (!isdelay)   //�����̰� false�϶� ���� ����
            {
                if(WeaponChage == 1)    //Sword ����
                {
                    if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
                        SwdCnt++;
                    else
                        SwdCnt = 1;

                    isdelay = true;
                    anim.SetFloat("Sword", SwdCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
                    anim.SetTrigger("sword_atk");

                    if (SwdCnt > 1)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                    {
                        StartCoroutine(Dash_delay());
                        SwdCnt = 0;
                    }
                   
                    curTime = coolTime;
                }
                if(WeaponChage == 2)    //Axe ����
                {
                    if (curTime > 0)    //ù��° ������ ��Ÿ�� ���� ���ݽ� ������ �ߵ�
                        AxeCnt++;
                    else
                        AxeCnt = 1;

                    isdelay = true;
                    anim.SetFloat("Axe", AxeCnt); //Blend�� �̿��� ���Ӱ����� �ִϸ��̼� ������ ����
                    anim.SetTrigger("axe_atk");

                    if (AxeCnt == 2)    //���ۺ� ������Ÿ�� �߰�
                    {
                        delayTime += 0.2f;
                        StartCoroutine(Dash_delay());
                    }   
                    else if (AxeCnt == 3)
                    {
                        delayTime += 0.55f;
                        StartCoroutine(Dash_delay());
                    }
                   
                    if (AxeCnt > 2)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                        AxeCnt = 0;

                    curTime = coolTime+0.5f;
                }
                if(WeaponChage == 3)    //Bow ����
                {
                    isdelay = true;
                    StartCoroutine(arrow_delay());
                    anim.SetTrigger("arrow_atk");
                }
                StartCoroutine(attack_delay());    //������ ���� ���ݱ��� ������
            }
        }
        else
            curTime -= Time.deltaTime;
    }
    
    //Wall_Slide
    void OnCollisionStay2D(Collision2D collision)   // �� �ݶ������� Player�� ��� ������ ����
    {
        if (collision.gameObject.tag == "Wall" && !isGround)
        {
            anim.SetBool("Wall_slide", true);
            rigid.drag = 10;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)   //Player�� ���� ���� ������
    {
        if (collision.gameObject.tag == "Wall" && h > 0) //���ʺ��� �پ ��������
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
        else if (collision.gameObject.tag == "Wall" && h < 0) //�����ʺ��� �پ ��������
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }

        if(collision.gameObject.tag == "Wall" && Input.GetKey(KeyCode.Space)) //������ ������ �ݴ�� �ðܰ�
        {
            if(h > 0)
                rigid.velocity = new Vector2(1, 1) * 8f;
            if(h < 0)
                rigid.velocity = new Vector2(-1, 1) * 8f;
        }
            
    }

    IEnumerator attack_delay() //���Ӱ��� ������
    {
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
    }

    private IEnumerator Sliding() //�����̵� ����
    {
        yield return null;
        Speed = 0;
        isSlide = true;
        gameObject.layer = 6;
        anim.SetBool("Sliding", true);
        if(slideDir == 1) //���������� �����̵�
           rigid.velocity = new Vector2(transform.localScale.x * slideSpeed, Time.deltaTime);
        if(slideDir == -1) //�������� �����̵�
           rigid.velocity = new Vector2((transform.localScale.x * -1 * slideSpeed), Time.deltaTime);
        yield return new WaitForSeconds(0.5f); //���� �ð�
        anim.SetBool("Sliding", false);
        gameObject.layer = 7;
        Speed = 4;
        yield return new WaitForSeconds(2f); //�����̵� ��Ÿ��
        isSlide = false;

    }
    IEnumerator arrow_delay() //ȭ����ݽ� ������ �ð� ���� - �ִϸ��̼ǰ� �����ֱ� ����
    {
        yield return new WaitForSeconds(0.5f);
        if (slideDir == 1)
            rigid.velocity = new Vector2(transform.localScale.x - 5f, Time.deltaTime);
        else
            rigid.velocity = new Vector2(transform.localScale.x + 5f, Time.deltaTime);
        Instantiate(arrow, pos.position, transform.rotation);
    }

    IEnumerator Dash_delay() //ū ���ݽ� �ణ�� �������� ������ ���� �̵�
    {
        if (SwdCnt == 2)
            yield return new WaitForSeconds(0.5f);
        else if(AxeCnt == 2)
            yield return new WaitForSeconds(0.8f);
        else
            yield return new WaitForSeconds(1f);
        if (slideDir == -1)
            rigid.velocity = new Vector2(transform.localScale.x - attackDash, Time.deltaTime);
        else
            rigid.velocity = new Vector2(transform.localScale.x + attackDash, Time.deltaTime);
    }
}
