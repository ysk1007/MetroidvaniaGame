using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float deleteTime=2f;
    public float Dmg = 10;
    public float speed = 15f;
    public bool isMasterSkill = false;  //플레이어 마스터스킬 사용 유무 확인
    public int TreeCnt; //랜덤 나무 번호 저장
    private Vector3 Direction = Vector3.right;  //화살이 나가는 방향

    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public Enemy enemy;
    public Transform pos;
    public Transform part;
    public SpriteRenderer spriteRenderer;
    public Vector3 dir;
    public GameObject BowTree1;  // 숙련도 오브젝트
    public GameObject BowTree2;  // 숙련도 오브젝트
    public GameObject BowTree3;  // 숙련도 오브젝트
    public GameObject BowTree4;  // 숙련도 오브젝트
    public GameObject ParticlePrefab;   //파티클 프리펩
    public ParticleSystem Particle; // 파티클 시스템 

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

            if (player.isMasterSkill == true)
            {
                isMasterSkill = true; // 마스터스킬 사용중이면 true로 설정
            }
            else
            {
                isMasterSkill = false; // 마스터스킬 사용 중이 아니면 isMasterSkill 변수를 false로 설정

            }
        }
    }

    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
            Desrtory();

        pos.position += Direction * speed * Time.deltaTime; // 직진 이동
        TreeCnt = Random.Range(1, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.tag == "Enemy")
        {
            enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                if (isMasterSkill)
                {
                    isMasterSkill = false;
                    StartCoroutine(MasterSkill());
                }
            }
            if(player.WeaponChage == 3)
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
            Particle.startColor = new Color(0.827f, 0.447f, 0.518f,1);  //나무별 파티클 색상 변경
            GameObject BowTree = Instantiate(BowTree1, pos.position, transform.rotation);   // 나무 이펙트 생성
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);    //파티클 생성
        }
            
        if (TreeCnt == 2)
        {
            Particle.startColor = new Color(0.51f, 0.773f, 0.196f,1);
            GameObject BowTree = Instantiate(BowTree2, pos.position, transform.rotation);
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);
        }
            
        if (TreeCnt == 3)
        {
            Particle.startColor = new Color(1, 0.933f, 0.545f,1);
            GameObject BowTree = Instantiate(BowTree3, pos.position, transform.rotation);
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);
            
        }
            
        if (TreeCnt == 4)
        {
            Particle.startColor = new Color(0.831f, 0.329f, 0.122f,1);
            GameObject BowTree = Instantiate(BowTree4, pos.position, transform.rotation);
            GameObject particle = Instantiate(ParticlePrefab, part.position, part.rotation);
        }
        Desrtory();
        yield return null;
    }

    void ParticleEfc()  //파티클 위치 지정
    {
        part.rotation = ParticlePrefab.transform.rotation;
        part.position = new Vector3(pos.transform.position.x, pos.transform.position.y + 6f, pos.transform.position.z);
        Particle.Play();
    }

    void Desrtory()
    {
        Destroy(gameObject);
    }
}
