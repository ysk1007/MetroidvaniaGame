using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowTree : MonoBehaviour
{
    public float deleteTime = 5f;
    public float Attackdelay = 0;
    public float Dmg = 5;
    public float speed = 1f;
    public Enemy enemy;
    public GameObject Tree;  // ���õ� ������Ʈ
    private List<Collider2D> enemyColliders = new List<Collider2D>();

    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
        {
            Desrtory();
        }
            
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy" && Attackdelay <= 0)
        {
            ApplyAOEDamage();
            if (!enemyColliders.Contains(collision))
            {
                enemyColliders.Add(collision); // ó�� ������ collider�� ����Ʈ�� �߰�
            }
        }
        else
            Attackdelay -= Time.deltaTime;
    }

    void ApplyAOEDamage()
    {
        foreach (Collider2D enemyCollider in enemyColliders)
        {
            if (enemyCollider != null)
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    StartCoroutine(enemy.Hit(Dmg));
                    Attackdelay = 1f;
                }
            }
        }
        enemyColliders.Clear(); // ����Ʈ ����
    }
    void Desrtory()
    {
        Destroy(gameObject);
    }
}
