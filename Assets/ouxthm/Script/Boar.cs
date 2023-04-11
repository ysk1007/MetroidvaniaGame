using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Boar : MonoBehaviour
{
    public float Enemy_HP;  // 적의 체력
    public float Enemy_Speed;   // 적의 이동속도
    public bool Enemy_Left; // 적의 방향
    public bool Attacking = false;
    public bool Hit_Set;    // 몬스터를 깨우는 변수
    public float Gap_Distance = 99;  // Fat_Boss와 Player 사이 거리.
   
    Animator animator;
    Transform target;
    SpriteRenderer spriteRenderer;

    public void Awake()
    {
        Hit_Set = false;    // 플레이어에게 맞지 않은 상태
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        target = GameObject.Find("Player").transform;   // 오브젝트 이름이 Player의 transform.
    }

    public void FixedUpdate()
    {
        StartCoroutine(Move());
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")    // collicoin = 닿았다는 뜻, 닿은 오브젝트의 태그가 Enemy일 때
        {
            Debug.Log(collision.gameObject.tag);           
            StartCoroutine(StopRush());
        }
        if(collision.gameObject.tag == "Player")    // 체력 깎이는 것 추가.
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
        if (Hit_Set == true)    // 플레이어에게 맞았다면
        {

            if (animator.GetBool("Rush") && Enemy_Left == true)       // 뛰는 애니메이션이 실행 중이고, Fat_Left의 값이 true라면
            {
                gameObject.transform.Translate(new Vector2(-1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.
                spriteRenderer.flipX = false;
            }
            else if (animator.GetBool("Rush") && Enemy_Left == false)  // Running 애니메이션이 실행 중이고 Fat_Left의 값이 false라면
            {
                gameObject.transform.Translate(new Vector2(1, 0) * Time.deltaTime * Enemy_Speed);   // 벡터 값을 (1,0)에서 speed에 저장된 값을 곱한 위치로 이동, Translate는 위치로 부드럽게 이동시킴.           
                spriteRenderer.flipX = true;
            }
        }
        yield return null;
    }

    IEnumerator Rush()   // 돌진 코루틴.
    {
        if (target.transform.position.x < transform.position.x) // 플레이어가 왼쪽에 있다면.
        {
            Enemy_Left = true;
        }
        else if (target.transform.position.x > transform.position.x)    // 플레이어가 오른쪽에 있다면.
        {
            Enemy_Left = false;
        }
        yield return new WaitForSeconds(1f);    // 플레이어 인지 후 달려가기 위한 기 모음 1초
        Attacking = true;
        
        animator.SetBool("Rush", true);
        
        Enemy_Speed = 10f;        //  속도 10 설정.
        Debug.Log("작은 놈 뛰는 중");
    }


    IEnumerator StopRush()
    {
        animator.SetTrigger("Hit");
        animator.SetBool("Rush", false);
        Attacking = false;  // 공격 중 끄기
        Debug.Log("충돌");
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
        Debug.Log("StopRush 코루틴 끝");
    }

 }
