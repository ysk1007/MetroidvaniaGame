using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump ���� ���� ����
    public float maxSpeed; //Move �ְ�ӵ� ���� ����
    public float curTime,coolTime = 2;  // ���Ӱ����� ������ �ð�
    public bool isdelay = false;    //���� ������ üũ
    public bool isSlide = false;     //�����̵� üũ
    public bool isGround = true;    //Player�� ������ �ƴ��� üũ
    public float delayTime = 1f;    //���� ������ �⺻ �ð�
    public int WeaponChage = 1;     //���� ���� ���� ����
    public int JumpCnt, JumpCount = 2;  //2�������� ���� ī���� ���ִ� ����
    public int SwdCnt, AxeCnt;  //���ݸ���� ����
    public float h; // ���Ⱚ

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
        jumpPower = 17; //�⺻ ��������
    }
    void Update()
    {
        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�
    }

    void Player_Move()
    {
        //Move
        h = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if(!isdelay && h != 0 && gameObject.layer != 6)    //���� ���������Ͻ� �̵� �Ұ���
        {
            transform.Translate(new Vector2(h, 0) * maxSpeed * Time.deltaTime);
            anim.SetBool("Player_Walk", true);

            if (h < 0) //���� �ٶ󺸱�
                spriteRenderer.flipX = false;
            else if (h > 0) //������ �ٶ󺸱�
                spriteRenderer.flipX = true;    
        }
        else
            anim.SetBool("Player_Walk", false);

        //Jump
        if (Input.GetKeyDown(KeyCode.Space) && JumpCnt > 0 && !anim.GetBool("Sliding") && !anim.GetBool("Wall_slide"))
        {
            rigid.velocity = Vector2.up * jumpPower;
            anim.SetBool("Player_Jump", true);
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
                }
            }
            //Debug.Log(rayHitDown.collider);
        }

        //Sliding �̿ϼ�
        
           
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
        if (Input.GetKeyDown(KeyCode.A))
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
                        SwdCnt = 0;

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
                        delayTime += 0.2f;
                    if (AxeCnt == 3)
                        delayTime += 0.55f;

                    if (AxeCnt > 2)     //���Ӱ����� ������ �ٽ� ù��° ���ݰ����� ����
                        AxeCnt = 0;

                    curTime = coolTime+0.5f;
                }
                if(WeaponChage == 3)    //Arrow ����
                {
                    isdelay = true;
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
        if (collision.gameObject.tag == "Wall" && spriteRenderer.flipX == false) //���ʺ��� �پ ��������
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }
        else if (collision.gameObject.tag == "Wall" && spriteRenderer.flipX == true) //�����ʺ��� �پ ��������
        {
            anim.SetBool("Wall_slide", false);
            rigid.drag = 0;
        }

        if(collision.gameObject.tag == "Wall" && Input.GetKey(KeyCode.Space)) //������ ������ �ݴ�� �ðܰ�
        {
            if(spriteRenderer.flipX == false)
                rigid.velocity = new Vector2(-1, 1) * 8f;
            if(spriteRenderer.flipX == true)
            rigid.velocity = new Vector2(1, 1) * 8f;
        }
            
    }

    IEnumerator attack_delay() //���Ӱ��� ������
    {
        //Debug.Log(delayTime);
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
    }

    IEnumerator Sliding() //�����̵� ����
    {
        yield return null;
        isSlide = true;
        anim.SetBool("Sliding", true);
        transform.Translate(new Vector2(-1, 0) * maxSpeed * Time.deltaTime);
        gameObject.layer = 6;
        maxSpeed = 6;
    }

    IEnumerator slide_delay() //�����̵� ������
    {
        yield return new WaitForSeconds(0.2f);
        maxSpeed = 4;
        anim.SetBool("Sliding", false);
        gameObject.layer = 7;

        yield return new WaitForSeconds(2f);
        isSlide = false;
    }
}
