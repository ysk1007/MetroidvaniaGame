using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HolyArrow : MonoBehaviour
{
    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public Enemy enemy;
    public GameObject HolyEffect;    // 적이 화살을 맞았을 시 발생하는 피격 이펙트
    public LayerMask islayer; // 충돌 감지를 할 레이어
    public Transform pos; // 화살 위치 정보
    public float speed = 20f; // 화살 이동 속도
    private Vector3 moveDirection = Vector3.right; // 화살이 나가는 방향

    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = Player.instance.GetComponent<Player>();
        Invoke("DestroyArrow", 1.5f); // 일정 시간이 지난 후 화살을 제거하는 Invoke 함수를 호출
        pos = transform;

        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // 플레이어가 오른쪽을 바라보면 화살을 오른쪽으로 발사
                moveDirection = Vector3.right;
                spriteRenderer.flipX = false;
            }
            else
            {
                // 플레이어가 왼쪽을 바라보면 화살을 왼쪽으로 발사
                moveDirection = Vector3.left;
                spriteRenderer.flipX = true;
            }
        }
    }


    Collider2D FindCollider(Collider2D[] colliders)// 가장 가까운 적 찾기
    {
        Collider2D closestCollider = null;
        float distance = float.MaxValue;
        foreach (Collider2D coll in colliders)
        {
            if (LayerMask.LayerToName(coll.gameObject.layer) != "Pad" && LayerMask.LayerToName(coll.gameObject.layer) != "Tilemap")
            {
                float tempDist = Vector2.Distance(transform.position, coll.transform.position);
                if (tempDist < distance)
                {
                    closestCollider = coll;
                    distance = tempDist;
                }
            }
        }
        return closestCollider;
    }

    private void Update()
    {
        pos.position += moveDirection * speed * Time.deltaTime; // 화살 직진 이동
    }

    private void OnTriggerEnter2D(Collider2D collision) //화살이 충돌시 확인후 공격 및 사라지게함
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            enemy = collision.GetComponent<Enemy>();
            if(enemy != null)
            {
                Vector3 newpos = enemy.Pos.position;
                Instantiate(HolyEffect, newpos, transform.rotation);
                DestroyArrow();
            }
        }
    }

    public void DestroyArrow()  // 화살 제거 함수
    {
        Destroy(gameObject);
    }
}


