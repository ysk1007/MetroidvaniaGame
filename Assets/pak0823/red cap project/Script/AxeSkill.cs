using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSkill : MonoBehaviour
{
    public float Dmg;
    public float speed = 15f;
    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public Enemy enemy;
    public SpriteRenderer spriteRenderer;
    public Transform trans;



    private void Start()
    {
        player = Player.instance.GetComponent<Player>();
        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // 플레이어가 오른쪽을 바라보면 오른쪽으로 발사
                spriteRenderer.flipX = false;
            }
            else
            {
                // 플레이어가 왼쪽을 바라보면 왼쪽으로 발사
                Vector3 euler = transform.rotation.eulerAngles;
                euler.y = 180f;
                transform.rotation = Quaternion.Euler(euler);
            }
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Dmg = 999;
                StartCoroutine(Delay());
                StartCoroutine(enemy.Hit(Dmg));
            }
        }
        if(collision != null && collision.tag == "Boss")    //보스에게 주는 대미지는 hp의 몇 퍼센트 대미지 주는걸로 생각중
        {
            enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                Dmg = (enemy.Enemy_HP) / 3;
                StartCoroutine(Delay());
                StartCoroutine(enemy.Hit(Dmg));
            }
        }
        StartCoroutine(Destroy());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
