using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boar : MonoBehaviour
{
    public float Enemy_HP;  // ���� ü��
    public float Enemy_Speed;   // ���� �̵��ӵ�
    public bool Enemy_Left; // ���� ����
    public bool Attacking = false;
    public bool Hit_Set;    // ���͸� ����� ����
    public float Gap_Distance = 99;  // Fat_Boss�� Player ���� �Ÿ�.
   
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;

    public void Awake()
    {
        Hit_Set = false;    // �÷��̾�� ���� ���� ����
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = GameObject.Find("Player").transform;   // ������Ʈ �̸��� Player�� transform.
    }

    public void FixedUpdate()
    {
        StartCoroutine(Move());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")    // collicoin = ��Ҵٴ� ��, ���� ������Ʈ�� �±װ� Enemy�� ��
        {
            Debug.Log(collision.gameObject.tag);           
            StartCoroutine(StopRush());
        }
        if(collision.gameObject.tag == "Player")    // ü�� ���̴� �� �߰�.
        {           
            StartCoroutine(Rush());
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set=true;
        }
        else if(collision.gameObject.tag == "Sword")
        {
            StartCoroutine(Rush());
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Axe")
        {
            StartCoroutine(Rush());
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
        else if (collision.gameObject.tag == "Arrow")
        {
            StartCoroutine(Rush());
            Debug.Log(collision.gameObject.tag);
            animator.SetTrigger("Hit");
            Hit_Set = true;
        }
    }

    IEnumerator Move()
    {
        if (Hit_Set == true)    // �÷��̾�� �¾Ҵٸ�
        {

            if (animator.GetBool("Rush") && Enemy_Left == true)       // �ٴ� �ִϸ��̼��� ���� ���̰�, Fat_Left�� ���� true���
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.
                spriteRenderer.flipX = false;
            }
            else if (animator.GetBool("Rush") && Enemy_Left == false)  // Running �ִϸ��̼��� ���� ���̰� Fat_Left�� ���� false���
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // ���� ���� (1,0)���� speed�� ����� ���� ���� ��ġ�� �̵�, Translate�� ��ġ�� �ε巴�� �̵���Ŵ.           
                spriteRenderer.flipX = true;
            }
        }
        yield return null;
    }

    IEnumerator Rush()   // ���� �ڷ�ƾ.
    {
        if (target.transform.position.x < transform.position.x) // �÷��̾ ���ʿ� �ִٸ�.
        {
            Enemy_Left = true;
        }
        else if (target.transform.position.x > transform.position.x)    // �÷��̾ �����ʿ� �ִٸ�.
        {
            Enemy_Left = false;
        }
        yield return new WaitForSeconds(1f);    // �÷��̾� ���� �� �޷����� ���� �� ���� 1��
        Attacking = true;
        
        animator.SetBool("Rush", true);
        
        Enemy_Speed = 10f;        //  �ӵ� 10 ����.
        Debug.Log("���� �� �ٴ� ��");
    }


    IEnumerator StopRush()
    {
        animator.SetTrigger("Hit");
        animator.SetBool("Rush", false);
        Attacking = false;  // ���� �� ����
        Debug.Log("�浹");
        if (Enemy_Left == true)
        {
            Enemy_Left = false;
        }
        else if (Enemy_Left == false)
        {
            Enemy_Left = true;
        }
        yield return new WaitForSeconds(0.1f);
        Hit_Set = false;
        Debug.Log("StopRush �ڷ�ƾ ��");
    }

 }
