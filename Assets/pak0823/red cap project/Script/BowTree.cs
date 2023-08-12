using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowTree : MonoBehaviour
{
    public float deleteTime = 10f;
    public float Dmg = 5;
    public float speed = 1f;
    public Enemy enemy;
    public GameObject Tree;  // ���õ� ������Ʈ
    public ParticleSystemRenderer particleRenderer;
    public bool flip = false;
    public int maxEnemies = 5;  // �ִ� ���� ��
    public float delay; // ���� ������ �ð�
    private List<Collider2D> enemyColliders = new List<Collider2D>();   //���� ���� �ߺ� üũ
    private List<Enemy> enemiesInRange = new List<Enemy>(); // Enemy Ÿ�� ����Ʈ�� ����
    new AudioSource audio;
    public AudioClip TreeSound;
    
    private void Start()
    {
        particleRenderer = this.gameObject.GetComponent<ParticleSystemRenderer>();
        leafFlip();
    }
    

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
        {
            Destroy(gameObject);
        }

        if(delay <= 0)
        {
            TreeDamage();
        }
        else
            delay -= Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other) // OnEnter �Լ� ���
    {
        enemyColliders.Add(other);
        audio.clip = TreeSound;
        audio.Play();
    }

    private void OnTriggerExit2D(Collider2D other) // OnExit �Լ� ���
    {
        enemyColliders.Remove(other);
    }

    void TreeDamage()
    {
        enemiesInRange = enemyColliders
            .Where(x => x != null && x.tag == "Enemy") // �����ϸ鼭 "Enemy" �±��� ���ӿ�����Ʈ�� ����
            .Select(x => x.GetComponent<Enemy>()) // ����� ���ӿ�����Ʈ���� Enemy ��ũ��Ʈ ������Ʈ�� ������ ����Ʈ�� ����
            .Distinct() // �ߺ��� Enemy ������Ʈ ����
            .ToList();

        foreach (Enemy enemy in enemiesInRange) // ������ ��� ������ ������ ����
        {
            StartCoroutine(enemy.Hit(Dmg));
        }
        delay = 1f;
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
