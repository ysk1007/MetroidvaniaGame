using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; //튜플 위함
using Random = UnityEngine.Random;

public class BossHpController : MonoBehaviour
{
    public GameObject ShakeObject;
    public GameObject CameraObject;
    Vector3 ShakeObjectPos;
    Vector3 CameraObjectPos;
    [SerializeField] [Range(1f, 5f)] float shakeRange = 2.5f;
    [SerializeField] [Range(0.1f, 2f)] float CamerashakeRange = 1f;
    [SerializeField] [Range(0.1f, 1f)] float duration = 0.5f;

    public float Animduration = 1f; // 애니메이션 시간 (초)
    float targetHeight = 150f; // 목표로 하는 높이

    private Vector3 startPosition;
    private Vector3 targetPosition;

    public GameObject TotalBar;
    public List<GameObject> BarObject;
    public HpBar[] HpSliders = { };
    public TextMeshProUGUI LineCount;

    public TextMeshProUGUI BossName;
    public float BossTotalHp = 500f;
    public int BossHpLine = 5;
    private float DivisionHp;
    public int currentHpLine = 0;

    public Enemy boss;
    // Start is called before the first frame update
    void BossStart()
    {
        startPosition = ShakeObject.transform.position;
        targetPosition = startPosition + new Vector3(0f, targetHeight, 0f);

        ShakeObjectPos = targetPosition; //흔들릴 오브젝트 현재 위치 저장
        HpSliders = TotalBar.GetComponentsInChildren<HpBar>();
        /*BarObject = TotalBar.transform.GetChildCount();*/
        DivisionHp = BossTotalHp / BossHpLine;
        for (int i = 0; i < HpSliders.Length; i++)
        {
            HpSliders[i].maxHp = DivisionHp;
            HpSliders[i].currentHp = DivisionHp;
        }

        for (int i = 5; i > BossHpLine; i--)
        {
            HpSliders[i - 1].SelfDestroy();
        }

        currentHpLine = BossHpLine;
        LineCount.text = "X "+currentHpLine.ToString();
    }



    // Update is called once per frame
    void Update()
    {
        if(currentHpLine == 1)
        {
            LineCount.text = "";
        }
    }

    public void BossSpawn(Enemy enemy)
    {
        boss = enemy;
        BossName.text = boss.Enemy_Name;
        BossTotalHp = boss.Enemy_HP;
        BossHpLine = boss.BossHpLine;
        TotalBar.SetActive(true);
        BossStart();
        StartCoroutine(BossHpUi_Up());
    }

    public void BossDead()
    {
        CameraShake();
        Time.timeScale = 0.5f;
        Invoke("BossHpUiDown", 3f);
    }

    void BossHpUiDown()
    {
        CancelInvoke();
        StartCoroutine(BossHpUi_Down());
    }

    public void BossHit(float damage)
    {
        Shake();
        HpSliders[currentHpLine - 1].Dmg(damage);
        overDamageCarcul();
        if (HpSliders[0].currentHp <= 0)
        {
            BossDead();
        }
    }

    public void Shake()
    {
        InvokeRepeating("StartShake", 0f, 0.005f); //InvokeRepeating 일정 간격 호출
        Invoke("StopShake", duration); //지연 시간 후 멈추는 함수 호출
    }

    public void StartShake() //흔들림 효과
    {
        float ObjectPosX = Random.value * shakeRange * 2 - shakeRange;
        float ObjectPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 ShakeObjectPos = ShakeObject.transform.position;
        ShakeObjectPos.x += ObjectPosX;
        ShakeObjectPos.y += ObjectPosY;
        ShakeObject.transform.position = ShakeObjectPos; //바뀐 위치 적용
    }

    public void StopShake() //흔들림 멈추는 함수
    {
        CancelInvoke("StartShake"); //현재 Invoke 실행되는 함수 있으면 취소
        ShakeObject.transform.position = ShakeObjectPos; //처음 지정했던 곳으로 ui 원위치
    }

    public IEnumerator BossHpUi_Up()
    {
        float elapsedTime = 0f;

        while (elapsedTime < Animduration)
        {
            elapsedTime += Time.deltaTime;

            // t는 0부터 1까지 점진적으로 증가하는 값으로, 0일 때 시작 위치, 1일 때 목표 위치에 해당합니다.
            float t = elapsedTime / Animduration;

            // 보간된 위치 계산
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // 오브젝트 이동
            ShakeObject.transform.position = newPosition;

            yield return null;
        }
    }

    IEnumerator BossHpUi_Down()
    {
        Time.timeScale = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < Animduration)
        {
            elapsedTime += Time.deltaTime;

            // t는 0부터 1까지 점진적으로 증가하는 값으로, 0일 때 목표 위치, 1일 때 시작 위치에 해당합니다.
            float t = elapsedTime / Animduration;

            // 보간된 위치 계산
            Vector3 newPosition = Vector3.Lerp(targetPosition, startPosition, t);

            // 오브젝트 이동
            ShakeObject.transform.position = newPosition;

            yield return null;
        }
        ShakeObject.transform.position = startPosition; //처음 지정했던 곳으로 ui 원위치
        for (int i = 0; i < 5; i++)
        {
            if (HpSliders[i] != null)
            {
                HpSliders[i].SelfCreate();
            }
        }
    }

    public void CameraShake()
    {
        InvokeRepeating("StartCameraShake", 0f, 0.005f); //InvokeRepeating 일정 간격 호출
        Invoke("StopCameraShake", 2f); //지연 시간 후 멈추는 함수 호출
    }

    public void StartCameraShake() //흔들림 효과
    {
        CameraObjectPos = CameraObject.transform.position;
        float ObjectPosX = Random.value * CamerashakeRange * 2 - CamerashakeRange;
        float ObjectPosY = Random.value * CamerashakeRange * 2 - CamerashakeRange;
        Vector3 ShakeObjectPos = CameraObject.transform.position;
        ShakeObjectPos.x += ObjectPosX;
        ShakeObjectPos.y += ObjectPosY;
        CameraObject.transform.position = ShakeObjectPos; //바뀐 위치 적용
    }

    public void StopCameraShake() //흔들림 멈추는 함수
    {
        CancelInvoke("StartCameraShake"); //현재 Invoke 실행되는 함수 있으면 취소
        CameraObject.transform.position = CameraObjectPos; //처음 지정했던 곳으로 ui 원위치
    }

    public void overDamageCarcul()
    {
        if (HpSliders[currentHpLine - 1].currentHp < 0 && currentHpLine != 1)
        {
            float overdmg = Math.Abs(HpSliders[currentHpLine - 1].currentHp);
            currentHpLine--;
            LineCount.text = "X " + currentHpLine.ToString();
            HpSliders[currentHpLine - 1].Dmg(overdmg);
            overDamageCarcul();
        }
    }
}
