using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BowTree : MonoBehaviour
{
    public float deleteTime = 10f;
    public float Dmg;
    public float speed = 1f;
    public int maxEnemies = 10;  // 최대 몬스터 수
    public float delay; // 공격 딜레이 시간
    private List<Collider2D> enemyColliders = new List<Collider2D>();   //공격 몬스터 중복 체크
    private List<Enemy> enemiesInRange = new List<Enemy>(); // Enemy 타입 리스트로 변경
    new AudioSource audio;
    public AudioClip TreeSound;
    public Player player;

    private void Awake()
    {
        player = Player.instance;
        audio = GetComponent<AudioSource>();
        Dmg = player.Dmg / 2;
    }

    public void Start()
    {
        Dmg = Dmg + (player.AtkPower + player.GridPower) * 0.5f;
    }
    void Update()
    {
        deleteTime -= Time.deltaTime;
        if (deleteTime <= 0)
        {
            Destroy(gameObject);
        }

        if (delay <= 0)
        {
            TreeDamage();
        }
        else
            delay -= Time.deltaTime;

    }

    private void OnTriggerEnter2D(Collider2D other) // OnEnter 함수 사용
    {
        enemyColliders.Add(other);
        audio.clip = TreeSound;
        audio.Play();
    }

    private void OnTriggerExit2D(Collider2D other) // OnExit 함수 사용
    {
        enemyColliders.Remove(other);
    }

    void TreeDamage()
    {
        enemiesInRange = enemyColliders
            .Where(x => x != null && x.tag == "Enemy") // 존재하면서 "Enemy" 태그인 게임오브젝트를 추출
            .Select(x => x.GetComponent<Enemy>()) // 추출된 게임오브젝트에서 Enemy 스크립트 컴포넌트를 가져와 리스트에 저장
            .Distinct() // 중복된 Enemy 컴포넌트 제거
            .ToList();

        foreach (Enemy enemy in enemiesInRange) // 감지된 모든 적에게 데미지 입힘
        {
            StartCoroutine(enemy.Hit(Dmg));
        }
        delay = 1f;
    }
}
