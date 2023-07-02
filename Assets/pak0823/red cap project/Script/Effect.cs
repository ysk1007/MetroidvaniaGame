using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float deleteTime=2f;
    public float Dmg = 10;
    private float speed = 20f;
    private Vector3 Direction = Vector3.right;

    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public Enemy enemy;
    public Transform pos;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        player = FindObjectOfType<Player>(); // Player ��ũ��Ʈ�� ���� ���� ������Ʈ�� ã�Ƽ� �Ҵ�
        pos = transform;

        if (player != null)
        {
            if (player.GetComponent<SpriteRenderer>().flipX)
            {
                // �÷��̾ �������� �ٶ󺸸� ���������� �߻�
                Direction = Vector3.right;
                spriteRenderer.flipX = false;
            }
            else
            {
                // �÷��̾ ������ �ٶ󺸸� �������� �߻�
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

        pos.position += Direction * speed * Time.deltaTime; // ���� �̵�
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
