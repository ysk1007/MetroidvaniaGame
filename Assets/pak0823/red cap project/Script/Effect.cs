using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float deleteTime=2f;
    public float Dmg = 10;
    private float speed = 20f;
    private Vector3 Direction = Vector3.right;

    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public Enemy enemy;
    public Transform pos;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Player 스크립트를 가진 게임 오브젝트를 찾아서 할당
        pos = transform;

        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // 플레이어가 오른쪽을 바라보면 오른쪽으로 발사
                Direction = Vector3.right;
                spriteRenderer.flipX = false;
            }
            else
            {
                // 플레이어가 왼쪽을 바라보면 왼쪽으로 발사
                Direction = Vector3.left;
                spriteRenderer.flipX = true;
            }
        }
    }

    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Desrtory();

        pos.position += Direction * speed * Time.deltaTime; // 직진 이동
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                //StartCoroutine(enemy.Hit(Dmg));
                //Debug.Log(Dmg + "Player");
                Desrtory();
            }
        }
        if (collision.tag == "Wall")
        {
            Desrtory();
        }
    }

    void Desrtory()
    {
        Destroy(gameObject);
    }
}
