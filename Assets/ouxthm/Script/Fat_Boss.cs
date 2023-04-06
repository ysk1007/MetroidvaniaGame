using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fat_Boss : MonoBehaviour
{
    Rigidbody rigid;
    BoxCollider2D boxcollider;
    SpriteRenderer spriteRenderer;
    Animator animator;
    Transform target;
    GameObject GameObject;

    public float Fat_speed = 2f; // Fat_Boss�� �ӵ�
    public int Fat_pattern;   // Fat_Boss�� ������ ������ ����
    public float Gap_Distance = 99;  // Fat_Boss�� Player ���� �Ÿ�
    public float AtkDistance = 5f;  // Fat_Boss�� ���� ��Ÿ�
    public float Fat_HP;    // Fat_Boss�� ü��

    public bool Attacking;

    void Awake()
    {

        rigid = GetComponent<Rigidbody>();
        boxcollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();      

        target = GameObject.Find("Player").transform;   // ������Ʈ �̸��� Player�� transform.
        Fat_HP = 100f;
        AtkDistance = 5f;   // Fat_Boss�� ���� ��Ÿ�

        Attacking = false;
        StartCoroutine(Think());
    }

    void Update()
    {
        Gap_Distance = Mathf.Abs(target.position.x - transform.position.x); // �÷��̾�� Fat_Boss�� ���̰Ÿ�

        Move();
    }

    void Move()
    {
        if (target.transform.position.x < transform.position.x)  // �÷��̾ ���ʿ� ���� ��
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = true;    // x�� �ø�
            }
            animator.SetBool("Walking", true);
            transform.Translate(new Vector2(-1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
        }
        else if(target.transform.position.x > transform.position.x)     // �÷��̾ �����ʿ� ���� ��
        {
            if (Attacking == false)
            {
                spriteRenderer.flipX = false;   // x�� �ø����� ����
            }
            animator.SetBool("Walking", true);
            gameObject.transform.Translate(new Vector2(1, 0).normalized * Time.deltaTime * Fat_speed);   //Enemy_Cop�� ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ
        }
    }

    IEnumerator Think()
    {
        if (Gap_Distance < AtkDistance)  // ��Ÿ� ���̸�
        {
            animator.SetBool("Walking", false);
            Fat_speed = 0;
            Debug.Log("��Ÿ� ���̾�");
            Attacking = false;
            StartCoroutine(Attack());   // ���� �ڷ�ƾ ����
            yield return new WaitForSeconds(0.1f);
        }
        else if (Gap_Distance > AtkDistance) // ��Ÿ� ���̸� 
        {
            yield return new WaitForSeconds(1f);
            Attacking = true;
            Debug.Log("����Ǵ�?");
            StartCoroutine(Think());
        }
    }

    IEnumerator Attack()
    {
       yield return new WaitForSeconds(0f);

       if (Attacking == false)
        {
            Attacking = true;
            Fat_pattern = Random.Range(1, 3);     // ���� ��ȣ�� 1 ~ 2���� �������� ����
            
            //Fat_pattern = Random.Range(1, 4);     // ���� ��ȣ�� 1 ~ 3���� �������� ����
        }

        if (Fat_pattern == 1)
        {
            StartCoroutine(Left_Hooking());
        }
        if(Fat_pattern == 2)
        {
            StartCoroutine(Right_Hooking());
        }
        else if(Fat_pattern == 3)
        {
            StartCoroutine(Running());
        }
    }

    IEnumerator Left_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss�� �ӵ��� 0���� �ٲ�
        animator.SetTrigger("Left_Hooking");
        Debug.Log("�޼�");
        yield return new WaitForSeconds(1.5f);
        Fat_speed = 2;
        Attacking = false;
        StartCoroutine(Think());
    }

    IEnumerator Right_Hooking()
    {
        Fat_speed = 0;      // Fat_Boss�� �ӵ��� 0���� �ٲ�
        animator.SetTrigger("Right_Hooking");        
        Debug.Log("������");
        yield return new WaitForSeconds(1.5f);
        Fat_speed = 2;
        Attacking = false;
        StartCoroutine(Think());
    }

    IEnumerator Running()
    {
        Fat_speed = 10f;      // Fat_Boss�� �ӵ��� 5�� �ٲ�
        animator.SetBool("Running", true);
        yield return new WaitForSeconds(10f);
        animator.SetBool("Running", false);
        Fat_speed = 0;
    }
}
