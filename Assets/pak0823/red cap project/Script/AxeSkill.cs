using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSkill : MonoBehaviour
{
    public float Dmg;
    public float speed = 15f;
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
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
                // �÷��̾ �������� �ٶ󺸸� ���������� �߻�
                spriteRenderer.flipX = false;
            }
            else
            {
                // �÷��̾ ������ �ٶ󺸸� �������� �߻�
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
        if(collision != null && collision.tag == "Boss")    //�������� �ִ� ������� hp�� �� �ۼ�Ʈ ����� �ִ°ɷ� ������
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
