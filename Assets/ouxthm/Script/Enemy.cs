using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Enemy : MonoBehaviour
{
    public int Enemy_Mod;   // 1: ������, 2: �������� ���� ����, 3:�������, 4:���Ÿ� ����
    public float Enemy_HP;  // ���� ü��
    public float Enemy_Speed;   // ���� �̵��ӵ�
    public float Enemy_Atk_Speed;    // ���� ���ݼӵ�
    public bool Enemy_Left; // ���� ����
    public bool Attacking;
    public bool Hit_Set;    // ���͸� ����� ����
    public float Gap_Distance_X ;  // ���� Player X �Ÿ�����
    public float Gap_Distance_Y ;  // ���� Player Y �Ÿ�����
    public int NextMove;  // ������ ���ڷ� ǥ��
    public float Enemy_Sensing_X;  // ���� X�� ���� ��Ÿ�
    public float Enemy_Sensing_Y;  // ���� Y�� ���� ��Ÿ�
    public float Enemy_Range_X = 2f; //���� X�� ���� ��Ÿ�
    public float Enemy_Range_Y = 2f; //���� Y�� ���� ��Ÿ�

    public float Enemy_Dying_anim_Time;     // �״� �ִϸ��̼� �ð� ����

    public GameObject gmobj;
    Rigidbody2D rigid;
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;
    RaycastHit2D rayHit;
    public abstract void InitSetting(); // ���� �⺻ ������ �����ϴ� �Լ�(�߻� Ŭ����)

    public virtual void Short_Monster(Transform target) // �ٰŸ� ���� ����
    {
        Gap_Distance_X = Mathf.Abs(target.transform.position.x - transform.position.x);
        Gap_Distance_Y = Mathf.Abs(target.transform.position.y - transform.position.y);
        //StartCoroutine(Sensing(target, rayHit));
        StartCoroutine(Attack(target, rayHit));
        Sensor();
    }
    public virtual void Long_Monster()  // ���Ÿ� ���� ����
    {
        Debug.Log("Long_Monster");
    }

    public virtual void Fly_Monster()   // ���� ����
    {
        Debug.Log("Fly_Monster");
    }

    IEnumerator OnCollisionEnter2D(Collision2D collision)
    {
        yield return null;

        animator = this.GetComponentInChildren<Animator>();
        if (collision.gameObject.tag == "Wall")    
        {
            Debug.Log(collision.gameObject.tag);
        }
        if (collision.gameObject.tag == "Player")   // �ӽ÷� ���� ��� ��� ��
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Sword")
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Axe")
        {
            StartCoroutine(Hit());
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            StartCoroutine(Hit());
        }
    }
    void Move() // �÷��̾� ���� �� move
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Enemy_Sensing_X < Gap_Distance_X)    // �����Ÿ� �ۿ� ���� �� 
        {
            if (NextMove == -1)       // Enemy�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
        else if(Enemy_Sensing_X > Gap_Distance_X && Enemy_Sensing_Y < Gap_Distance_Y)
        {
            if (NextMove == -1)       // Enemy�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (NextMove == 1)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
    }

    IEnumerator Sensing(Transform target, RaycastHit2D rayHit)  // �÷��̾� ���� 
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Gap_Distance_X <= Enemy_Sensing_X)      // Enemy�� X�� ��Ÿ��� ���� ��
        {
            if(Gap_Distance_Y < Enemy_Sensing_Y)    // Enemy�� Y�� �緯�⿡ ���� ��
            {
                if (transform.position.x < target.position.x)            // ������ ����
                {
                    NextMove = 1;

                    if (NextMove == 1 && rayHit.collider != null)  // NextMove�� 1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                    {
                        spriteRenderer.flipX = true;

                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                    }
                    else if (NextMove == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (0,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                    }
                }
                else if (transform.position.x > target.position.x)      // ���� ����
                {
                    NextMove = -1;
                    if (NextMove == -1 && rayHit.collider != null) // NextMove�� -1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                    {
                        spriteRenderer.flipX = false;
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                    }
                    else if (NextMove == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                    }
                }
            }
            else if (Gap_Distance_Y > Enemy_Sensing_Y)
            {
                Move();
            }
            
        }
        else if(Gap_Distance_X > Enemy_Sensing_X && Gap_Distance_Y > Enemy_Sensing_Y)
        {
            Move();     // Move �Լ� ����
            yield return null;
        }
    }

    IEnumerator Think()     // ����Լ� 
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();
        NextMove = Random.Range(-1, 2);     // -1 ~ 1������ ������ �� ����
        if (NextMove != 0 && NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)    // Gap_Distance > Enemy_Attack_Range�� �߰����� ������ �÷��̾ ��Ÿ� ���� �ְ� rayHit=null�̶�� ���ڸ� ������
        {
            spriteRenderer.flipX = true;       // NextMove�� ���� 1�̸� x���� flip��
        }
        // ���
        float nextThinkTime = Random.Range(2f, 5f);
        yield return new WaitForSeconds(nextThinkTime);
        StartCoroutine(Think());
    }

    IEnumerator Turn()
    {
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        NextMove *= -1;   // NextMove�� -1�� ���� ������ȯ
        if(NextMove == 1 && Gap_Distance_X > Enemy_Sensing_X)  // Gap_Distance > Enemy_Attack_Range�� �߰����� ������ �÷��̾ ��Ÿ� ���� �ְ� rayHit=null�̶�� ���ڸ� ������
        {
            spriteRenderer.flipX = true; // NextMove ���� 1�̸� x���� flip��
        }    
        StopAllCoroutines();
        StartCoroutine(Think());
        yield return null;
    }

    public void Sensor()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        
        // Enemy�� �� ĭ ���� ���� ��� ���� �ڱ� �ڽ��� ��ġ ���� (x)�� + NextMove���� ���ϰ� 1.2f�� ���Ѵ�.
        Vector2 frontVec = new Vector2(rigid.position.x + NextMove * 1.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down * 1.2f, new Color(0, 1, 0)); 
        // �������� �Ʒ��� ��Ƽ� �������� ������ ����(�������), LayMask.GetMask("")�� �ش��ϴ� ���̾ ��ĵ��
        rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)  
        {
           StartCoroutine(Turn());     
        }
    }
    
    IEnumerator Hit()
    {
        Enemy_HP -= 5;
        if (Enemy_HP > 0) // Enemy�� ü���� 0 �̻��� ��
        {
            animator.SetTrigger("Hit");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(0.5f);
            Enemy_Speed = 1.5f;
        }
        else if (Enemy_HP <= 0) // Enemy�� ü���� 0�� ���ų� ������ ��
        {
            animator.SetTrigger("Die");
            Enemy_Speed = 0;
            yield return new WaitForSeconds(Enemy_Dying_anim_Time);
            this.gameObject.SetActive(false);   // ������Ʈ ������� ��
        }
    }


    IEnumerator Attack(Transform target, RaycastHit2D rayHit)  // �÷��̾� ����   // �׽�Ʈ�� ���� �ڷ�ƾ�̸� Sensing �ڷ�ƾ�� ������ ����
    {
        rigid = this.GetComponent<Rigidbody2D>();
        spriteRenderer = this.GetComponentInChildren<SpriteRenderer>();

        if (Gap_Distance_X <= Enemy_Sensing_X)      // Enemy�� X�� ��Ÿ��� ���� ��
        {
            if (Gap_Distance_Y < Enemy_Sensing_Y)
            {
                if (transform.position.x < target.position.x)            // ������ ����
                {
                    NextMove = 1;

                    if (NextMove == 1 && rayHit.collider != null)  // NextMove�� 1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                    {
                        spriteRenderer.flipX = true;

                        transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                        if(Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("��������");
                        }
                    }
                    else if (NextMove == 1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (0,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("��������");
                        }
                    }
                }
                else if (transform.position.x > target.position.x)      // ���� ����
                {
                    NextMove = -1;
                    if (NextMove == -1 && rayHit.collider != null) // NextMove�� -1�� �� �׸��� ����ĳ��Ʈ ���� null�� �ƴ� ��
                    {
                        spriteRenderer.flipX = false;
                        transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("��������");
                        }
                    }
                    else if (NextMove == -1 && rayHit.collider == null)
                    {
                        transform.Translate(new Vector2(0, 0).normalized * Time.deltaTime * Enemy_Speed);   //Enemy�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ

                        if (Gap_Distance_X < Enemy_Range_X && Gap_Distance_Y < Enemy_Range_Y && (Enemy_Mod == 2 || Enemy_Mod == 3 || Enemy_Mod == 4))
                        {
                            StartCoroutine(Atking());
                            Debug.Log("��������");
                        }
                    }
                }
            }
            else if (Gap_Distance_Y > Enemy_Sensing_Y)
            {
                Move();
            }

        }
        else if (Gap_Distance_X > Enemy_Sensing_X && Gap_Distance_Y > Enemy_Sensing_Y)
        {
            Move();     // Move �Լ� ����
            yield return null;
        }
    }

    IEnumerator Atking()
    {
        gmobj.SetActive(true);
        yield return new WaitForSeconds(1f);
        gmobj.SetActive(false);
        Debug.Log("�����ڷ�ƾ����");
    }

}