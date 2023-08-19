using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float deleteTime = 1f;
    public float Dmg = 2f;
    public float speed = 20f;
    public int PlayerWeapon;
    public bool isMasterSkill = false;  //�÷��̾� �����ͽ�ų ��� ���� Ȯ��
    public bool isSkill = false;
    public int TreeCnt; //���� ���� ��ȣ ����
    private Vector3 Direction = Vector3.right;  //ȭ���� ������ ����

    public static Effect instance; //�߰���
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public Enemy enemy;
    public Transform pos;   // ���� ��ġ
    public Vector3 TreePos;
    public Transform part;  // ��ƼŬ ���� ��ġ
    public SpriteRenderer spriteRenderer;
    public GameObject BowTree1;  // ���õ� ������Ʈ
    public GameObject BowTree2;  // ���õ� ������Ʈ
    public GameObject BowTree3;  // ���õ� ������Ʈ
    public GameObject BowTree4;  // ���õ� ������Ʈ
    public GameObject ParticlePrefab;   //��ƼŬ ������
    public ParticleSystem Particle; // ��ƼŬ �ý��� 

    private void Awake()
    {
        player = Player.instance.GetComponent<Player>();
        Dmg = (10 + player.AtkPower + player.GridPower) * Dmg;
    }

    private void Start()
    {
        pos = transform;    //ȭ���� ���� ��ġ�� ����
        PlayerWeapon = player.WeaponChage;
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

            if (player.isMasterSkill == true)
            {
                isMasterSkill = true; // �����ͽ�ų ������̸� true�� ����
            }
            else
            {
                isMasterSkill = false; // �����ͽ�ų ��� ���� �ƴϸ� isMasterSkill ������ false�� ����

            }


            if (player.isSkill == true)
                isSkill = true;
            else
                isSkill = false;
        }
    }

    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Desrtory();
        pos.position += Direction * speed * Time.deltaTime; // ���� �̵�
        TreeCnt = Random.Range(1, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy" || collision.tag == "Boss")
        {
            enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (isMasterSkill)
                {
                    isMasterSkill = false;
                    TreePos = pos.position;
                    TreePos.Set(TreePos.x, 0f, TreePos.z);    // ������ y�� ��ġ�� ����
                    StartCoroutine(MasterSkill());
                }
            }
            if (PlayerWeapon == 3)
                Desrtory();
        }
        if (collision.tag == "Wall")
        {
            Desrtory();
        }
    }

    IEnumerator MasterSkill()
    {
        ParticleEfc();
        if (TreeCnt == 1)
        {
            Particle.startColor = new Color(0.827f, 0.447f, 0.518f, 1);  //������ ��ƼŬ ���� ����
            GameObject BowTree = Instantiate(BowTree1, TreePos, transform.rotation);   // ���� ����Ʈ ����
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);    //��ƼŬ ����
        }

        if (TreeCnt == 2)
        {
            Particle.startColor = new Color(0.51f, 0.773f, 0.196f, 1);
            GameObject BowTree = Instantiate(BowTree2, TreePos, transform.rotation);
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);
        }

        if (TreeCnt == 3)
        {
            Particle.startColor = new Color(1, 0.933f, 0.545f, 1);
            GameObject BowTree = Instantiate(BowTree3, TreePos, transform.rotation);
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);

        }

        if (TreeCnt == 4)
        {
            Particle.startColor = new Color(0.831f, 0.329f, 0.122f, 1);
            GameObject BowTree = Instantiate(BowTree4, TreePos, transform.rotation);
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);
        }

        Desrtory();
        yield return null;
    }

    void ParticleEfc()  //��ƼŬ ��ġ ����
    {
        part.rotation = ParticlePrefab.transform.rotation;
        part.position = new Vector3(pos.transform.position.x, pos.transform.position.y + 10f, pos.transform.position.z);
        Particle.Play();
    }

    void Desrtory()
    {
        Destroy(gameObject);
    }
}
