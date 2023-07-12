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
    public SpriteRenderer spriteRenderer;
    public Vector3 dir;
    public GameObject BowTree;  // 숙련도 오브젝트
    public GameObject BowTree2;  // 숙련도 오브젝트
    public GameObject BowTree3;  // 숙련도 오브젝트
    public GameObject BowTree4;  // 숙련도 오브젝트

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
        print(TreeCnt);
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
                Desrtory();
            }
        }
        if (collision.tag == "Wall")
        {
            Desrtory();
        }
    }

    IEnumerator MasterSkill()
    {
        if (TreeCnt == 1)
            Instantiate(BowTree, pos.position, transform.rotation);
        if (TreeCnt == 2)
            Instantiate(BowTree2, pos.position, transform.rotation);
        if (TreeCnt == 3)
            Instantiate(BowTree3, pos.position, transform.rotation);
        if (TreeCnt == 4)
            Instantiate(BowTree4, pos.position, transform.rotation);
        
        yield return null;
    }

    void Desrtory()
    {
        Destroy(gameObject);
    }
}
