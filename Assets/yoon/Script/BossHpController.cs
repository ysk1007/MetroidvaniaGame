using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; //Ʃ�� ����
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

    public float Animduration = 1f; // �ִϸ��̼� �ð� (��)
    float targetHeight = 150f; // ��ǥ�� �ϴ� ����

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

        ShakeObjectPos = targetPosition; //��鸱 ������Ʈ ���� ��ġ ����
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
        InvokeRepeating("StartShake", 0f, 0.005f); //InvokeRepeating ���� ���� ȣ��
        Invoke("StopShake", duration); //���� �ð� �� ���ߴ� �Լ� ȣ��
    }

    public void StartShake() //��鸲 ȿ��
    {
        float ObjectPosX = Random.value * shakeRange * 2 - shakeRange;
        float ObjectPosY = Random.value * shakeRange * 2 - shakeRange;
        Vector3 ShakeObjectPos = ShakeObject.transform.position;
        ShakeObjectPos.x += ObjectPosX;
        ShakeObjectPos.y += ObjectPosY;
        ShakeObject.transform.position = ShakeObjectPos; //�ٲ� ��ġ ����
    }

    public void StopShake() //��鸲 ���ߴ� �Լ�
    {
        CancelInvoke("StartShake"); //���� Invoke ����Ǵ� �Լ� ������ ���
        ShakeObject.transform.position = ShakeObjectPos; //ó�� �����ߴ� ������ ui ����ġ
    }

    public IEnumerator BossHpUi_Up()
    {
        float elapsedTime = 0f;

        while (elapsedTime < Animduration)
        {
            elapsedTime += Time.deltaTime;

            // t�� 0���� 1���� ���������� �����ϴ� ������, 0�� �� ���� ��ġ, 1�� �� ��ǥ ��ġ�� �ش��մϴ�.
            float t = elapsedTime / Animduration;

            // ������ ��ġ ���
            Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, t);

            // ������Ʈ �̵�
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

            // t�� 0���� 1���� ���������� �����ϴ� ������, 0�� �� ��ǥ ��ġ, 1�� �� ���� ��ġ�� �ش��մϴ�.
            float t = elapsedTime / Animduration;

            // ������ ��ġ ���
            Vector3 newPosition = Vector3.Lerp(targetPosition, startPosition, t);

            // ������Ʈ �̵�
            ShakeObject.transform.position = newPosition;

            yield return null;
        }
        ShakeObject.transform.position = startPosition; //ó�� �����ߴ� ������ ui ����ġ
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
        InvokeRepeating("StartCameraShake", 0f, 0.005f); //InvokeRepeating ���� ���� ȣ��
        Invoke("StopCameraShake", 2f); //���� �ð� �� ���ߴ� �Լ� ȣ��
    }

    public void StartCameraShake() //��鸲 ȿ��
    {
        CameraObjectPos = CameraObject.transform.position;
        float ObjectPosX = Random.value * CamerashakeRange * 2 - CamerashakeRange;
        float ObjectPosY = Random.value * CamerashakeRange * 2 - CamerashakeRange;
        Vector3 ShakeObjectPos = CameraObject.transform.position;
        ShakeObjectPos.x += ObjectPosX;
        ShakeObjectPos.y += ObjectPosY;
        CameraObject.transform.position = ShakeObjectPos; //�ٲ� ��ġ ����
    }

    public void StopCameraShake() //��鸲 ���ߴ� �Լ�
    {
        CancelInvoke("StartCameraShake"); //���� Invoke ����Ǵ� �Լ� ������ ���
        CameraObject.transform.position = CameraObjectPos; //ó�� �����ߴ� ������ ui ����ġ
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
