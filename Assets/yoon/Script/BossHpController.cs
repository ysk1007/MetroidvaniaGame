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
    Vector3 ShakeObjectPos;
    [SerializeField] [Range(1f, 5f)] float shakeRange = 2.5f;
    [SerializeField] [Range(0.1f, 1f)] float duration = 0.5f;

    public float Animduration = 1f; // �ִϸ��̼� �ð� (��)
    float targetHeight = 150f; // ��ǥ�� �ϴ� ����

    private Vector3 startPosition;
    private Vector3 targetPosition;

    public GameObject TotalBar;
    public List<GameObject> BarObject;
    public HpBar[] HpSliders = { };
    public TextMeshProUGUI LineCount;

    public float BossTotalHp = 500f;
    public int BossHpLine = 5;
    private float DivisionHp;
    public int currentHpLine = 0;

    // Start is called before the first frame update
    void Start()
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

    public void BossSpawn()
    {
        StartCoroutine(BossHpUi_Up());
    }

    void BossDead()
    {
        StartCoroutine(BossHpUi_Down());
    }

    public void BossHit(float damage)
    {
        Shake();
        HpSliders[currentHpLine - 1].Dmg(damage);
        if (HpSliders[currentHpLine - 1].currentHp < 0 && currentHpLine != 1)
        {
            float overdmg = Math.Abs(HpSliders[currentHpLine - 1].currentHp);
            currentHpLine--;
            LineCount.text = "X " + currentHpLine.ToString();
            HpSliders[currentHpLine - 1].Dmg(overdmg);
        }
        if (HpSliders[0].currentHp <= 0)
        {
            Invoke("BossDead", 2f);
            Time.timeScale = 0.5f;
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
    }
}