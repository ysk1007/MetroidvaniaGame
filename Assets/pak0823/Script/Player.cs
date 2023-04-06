using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpPower; //Jump ���� ���� ����
    public float maxSpeed; //Move �ְ�ӵ� ���� ����
    public float SliedSpeed = 5f; //Slied �ӵ� ����
    public float curTime,coolTime = 2;  //�� ���Ӱ����� ������ �ð�
    public float curTime2, coolTime2 = 2.5f;  //���� ���Ӱ����� ������ �ð�
    public int JumpCnt, JumpCount = 2;  //2�������� ���� ī���� ���ִ� ����
    public int SwdCnt,AxeCnt;  //���ݸ���� ����
    public int filp;
    public bool isdelay = false;
    public bool isGround = true;
    public bool isSlide = true;
    public float delayTime = 1f;

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
    }
    void Update()
    {
        Player_Move();  //Player�� �̵�, ����, �ӵ� �Լ�
        Player_Attack();    //Player�� ���� �Լ�

    }

    private void FixedUpdate()
    {
        slidingFalse();
        TFslide();
    }

    void Player_Move()
    {
        //Move Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.velocity = new Vector2(maxSpeed * h, rigid.velocity.y);
        maxSpeed = 4;
            

        //Max Speed ��,��� ������ �ӵ�
        if (rigid.velocity.x > maxSpeed) //Right Max Speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        if (rigid.velocity.x < maxSpeed * (-1)) //Left Max Speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        

        //Player ������ȯ
        if (h < 0) //���� �ٶ󺸱�
        {
            //trans.localPosition = new Vector3(-1, 1, 1);
            spriteRenderer.flipX = false;
            filp = -1;
            //transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (h > 0) //������ �ٶ󺸱�
        {
           // trans.localPosition = new Vector3(1, 1, 1);
            spriteRenderer.flipX = true;
            filp = 1;
            //transform.eulerAngles = new Vector3(0, 0, 0);
        }
        

        //Walk Animation Stop
        if (Mathf.Abs(rigid.velocity.x) < 0.3)  //�̵��ӵ��� ���ǿ� ������ Walk�ִϸ��̼� ����
        {
            anim.SetBool("Player_Walk", false);
        } 
        else
        {
            anim.SetBool("Player_Walk", true);
        }

        //Jump
        if (Input.GetButtonDown("Jump") && !anim.GetBool("Player_Jump") && JumpCnt > 0 )
        {
            if(anim.GetBool("sliding") == false)
            {
                rigid.velocity = Vector2.up * jumpPower;
                anim.SetBool("Player_Jump", true);
            }
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

        
        //Slied
        if (Input.GetKeyDown(KeyCode.K) && isGround && isSlide)
        {
            maxSpeed = 5;
            if (filp == 1)
            {
                trans.Translate(new Vector2(1, 0) * maxSpeed);
            }
            else
                trans.Translate(new Vector2(-1, 0) * maxSpeed);
            //Vector2 pos = trans.position;
            Debug.Log(rigid.velocity);
            
            //pos.x += h * Time.deltaTime * maxSpeed;
            Debug.Log(rigid.velocity);
            //spriteRenderer.flipX = true;
            anim.SetBool("Sliding",true);
                isSlide = false;
                gameObject.layer = 6;
                Invoke("SlidingFalse", 0.5f);

                Invoke("TFslide", 1f);
        }


        //Landing Platform, ��������Ʈ �ؿ� Tilemap�� ������ Jumping�� false
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHitDown = Physics2D.Raycast(rigid.position, Vector3.down, 2, LayerMask.GetMask("Tilemap"));
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
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

    void Player_Attack()
    {  
        if (Input.GetKeyDown(KeyCode.J)) //�ӽ� Sword����
        {
            maxSpeed = 0;

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
            }
            curTime = coolTime;
        }
        else
            curTime -= Time.deltaTime;


        if (Input.GetKeyDown(KeyCode.M))    //�ӽ� Axe����
        {
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
            curTime2 = coolTime2;
        }
        else
            curTime2 -= Time.deltaTime;
    }

    void slidingFalse()
    {
        maxSpeed = 4;
        anim.SetBool("Sliding", false);
        if (gameObject.layer == 6)
        {

        }
        else
        {
            gameObject.layer = 0;          // invincible time end
        }
    }
    void TFslide()
    {
        if (!isSlide)
            isSlide = true;
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
}
