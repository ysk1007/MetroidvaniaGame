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
        float h = Input.GetAxisRaw("Horizontal");   // �¿� ���Ⱚ�� ������ ��������
        if(!isdelay && h != 0)    //���� ���������Ͻ� �̵� �Ұ���
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
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Player_Jump") && JumpCnt > 0 && !anim.GetBool("Sliding"))
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


        //���� �� �� �����̵� Raycast üũ
        if (rigid.velocity.y < 0)   //Player �ؿ� Tilemap�� ������ Jumping�� false
        {
            Vector2 frontVec = new Vector2(rigid.position.x + h * 0.5f, rigid.position.y);
            RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Tilemap"));
            RaycastHit2D rayHitforward = Physics2D.Raycast(frontVec, Vector3.up, 0.5f, LayerMask.GetMask("Tilemap"));
            //Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            //Debug.DrawRay(frontVec, Vector3.up, new Color(0, 1, 0));

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

            if (rayHitforward.collider != null && rayHitDown.collider == null)  //���� �ƴϰ� Player �տ� Tilemap�� ������ wall_slide ����
            {
                anim.SetBool("Wall_slide", true);
                rigid.velocity = Vector2.down * 4f;
            }
            else
                anim.SetBool("Wall_slide", false);
        }

        //Sliding �̿ϼ�
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isSlide && isGround)
        {
            isSlide = true;
            anim.SetBool("Sliding", true);
            gameObject.layer = 6;
            maxSpeed *= 6;
            if (h == 0)
            {
                //transform.Translate(new Vector2(flip, 0) * maxSpeed * Time.deltaTime);
            }
            else
            {
                //transform.Translate(new Vector2(h, 0) * maxSpeed * Time.deltaTime);
            }
            StartCoroutine(slide_delay());
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

    IEnumerator attack_delay() //���Ӱ��� ������
    {
        //Debug.Log(delayTime);
        yield return new WaitForSeconds(delayTime);
        delayTime = 1f;
        isdelay = false;
    }

    IEnumerator slide_delay()
    {
        yield return new WaitForSeconds(0.2f);
        maxSpeed = 4;
        //Debug.Log(maxSpeed);
        anim.SetBool("Sliding", false);
        gameObject.layer = 7;

        yield return new WaitForSeconds(2f);
        isSlide = false;
    }
}
