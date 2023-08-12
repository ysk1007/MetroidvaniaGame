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
    public ParticleSystemRenderer particleRenderer;
    public bool flip = false;

    private void Start()
    {
        particleRenderer = this.gameObject.GetComponent<ParticleSystemRenderer>();
        leafFlip();
    }
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

    void leafFlip()
    {
        if (particleRenderer == null)
        {
            return;
        }
        else if (particleRenderer != null)
        {
            if (!flip)
            {
                Vector3 vc = new Vector3(1, 0, 0);
                particleRenderer.flip = vc;
                flip = true;
            }
            else if (flip)
            {
                Vector3 vc = new Vector3(0, 0, 0);
                particleRenderer.flip = vc;
                flip = false;
            }
            Invoke("leafFlip", 1.5f);
        }
    }
}
