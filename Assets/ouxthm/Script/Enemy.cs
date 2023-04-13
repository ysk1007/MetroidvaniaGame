using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{

    public float Enemy_HP;  // ���� ü��
    public float Enemy_Speed;   // ���� �̵��ӵ�
    public float Enemy_Attack_Speed;    // ���� ���ݼӵ�
    public bool Enemy_Left; // ���� ����
    public bool Attacking;
    public bool Hit_Set;    // ���͸� ����� ����
    public float Gap_Distance;  // ���� Player ���� �Ÿ�.
    public int NextMove;  // ������ ���ڷ� ǥ��
    public float Enemy_Attack_Range = 10f;  // ���� ���� ��Ÿ�


    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    public abstract void InitSetting(); // ���� �⺻ ������ �����ϴ� �Լ�

    public virtual void Short_Monster(Transform target)
    {
        Think();
        Move(target);
        Sensing(target);       
        Turn();
        Sensor();
    }

    public virtual void Long_Monster()
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()
    {
        Debug.Log("Fly_Monster");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        animator = this.GetComponentInChildren<Animator>();
        if (collision.gameObject.tag == "Wall")    // collicoin = ��Ҵٴ� ��, ���� ������Ʈ�� �±װ� Enemy�� ��
        {
            Debug.Log(collision.gameObject.tag);
        }
        if (collision.gameObject.tag == "Player")    // ü�� ���̴� �� �߰�.
        {

            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Sword")
        {
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Axe")
        {
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
    }
    void Move(Transform target)
    {
        if (Hit_Set == true)    // �÷��̾�� �¾Ҵٸ�
        {

            if (target.transform.position.x < transform.position.x)       // �ٴ� �ִϸ��̼��� ���� ���̰�, Fat_Left�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (target.transform.position.x > transform.position.x)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
    }

    public void Sensing(Transform target)
    {
        rigid = this.GetComponent<Rigidbody2D>();

        if (Mathf.Abs(transform.position.x - target.position.x) < Enemy_Attack_Range)      // Mathf.Abs�� ���밪���� �ٲ�, Enemy_Cop�� x�� - Player�� x �� �� ���� distance�� ����� �� ���� ���� ��
        {
            // Enemy_Cop�� �� ĭ ���� ���� ��� ���� �ڱ� �ڽ��� ��ġ ���� (x)�� + NextMove���� ���ϰ� 0.3f�� ���Ѵ�.
            Vector2 frontVec = new Vector2(rigid.position.x + NextMove * 1.2f, rigid.position.y);

            Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0));  //�������� �׸�

            // �������� �Ʒ��� ��Ƽ� �������� ������ ����(�������), LayMask.GetMask("")�� �ش��ϴ� ���̾ ��ĵ��
            RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));

            if (transform.position.x < target.position.x)            // Enemy_Cop�� x�� < Player�� x �� (������ ����)
            {
                NextMove = 1;      // NextMove�� ���� 1 ����

                if (NextMove == 1 && rayHit.collider != null)  // NextMove�� 1�� ��
                {
                    spriteRenderer.flipX = true;    // X�� �ø�
                    //animator.SetInteger("Speed", NextMove);
                    transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                }
                else if (NextMove == 1 && rayHit.collider == null)
                {
                    transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                }
            }
            else if (transform.position.x > target.position.x)      // Enemy_Cop�� x�� > Player�� x ��
            {
                NextMove = -1;      // NextMove�� -1�� ��
                if (NextMove == -1 && rayHit.collider != null) // NextMove�� -1�� ��
                {
                    spriteRenderer.flipX = false;   // X�� �ø����� ����
                    //animator.SetInteger("Speed", NextMove);
                    transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                }
                else if (NextMove == -1 && rayHit.collider == null)
                {
                    transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                }
            }
        }
        else
        {
            Move(target);     // Move �Լ� ����
            Invoke("Sensing", 5);
        }
    }

    public void Think()     // ����Լ� // enemy�� �ɵ��� ������
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        NextMove = Random.Range(-1, 2);     // -1 ~ 1������ ������ �� ����
        if (NextMove != 0 && NextMove == 1)
        {
            spriteRenderer.flipX = true;       // NextMove�� ���� 1�̸� x���� flip��
        }
        // ���
        float nextThinkTime = Random.Range(2f, 4f);
        Invoke("Think", nextThinkTime);     //Think()�Լ��� nextThinkTime�� ����� ���� �� �ڿ� ����
    }

    public void Turn()
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        NextMove *= -1;   // NextMove�� -1�� ���� ������ȯ
        if(NextMove == 1)
        {
            spriteRenderer.flipX = true; // NextMove ���� 1�̸� x���� flip��
        }      
        CancelInvoke();
        Invoke("Think", 2);
    }

    public void Sensor()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        
        // Enemy�� �� ĭ ���� ���� ��� ���� �ڱ� �ڽ��� ��ġ ���� (x)�� + NextMove���� ���ϰ� 1.2f�� ���Ѵ�.
        Vector2 frontVec = new Vector2(rigid.position.x + NextMove * 1.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0)); 
        // �������� �Ʒ��� ��Ƽ� �������� ������ ����(�������), LayMask.GetMask("")�� �ش��ϴ� ���̾ ��ĵ��
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)  
        {
           Turn();     
        }
    }
}