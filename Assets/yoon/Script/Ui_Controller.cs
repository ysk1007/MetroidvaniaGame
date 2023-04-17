using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_Controller : MonoBehaviour
{
    //�÷��̾� ü��,�����̵�,����ġ �� ui ���� ����
    public Slider PlayerHpBar; 
    public Slider SlidingBar; 
    public Slider ExpBar;

    public HpBar PlayerHp; //HpBar ��ũ��Ʈ ���� ����

    //����, �ִ�ü��, ����ü�� �ؽ�Ʈ ����
    public TextMeshProUGUI LevelVelueUi;
    public TextMeshProUGUI PlayerMaxHpText;
    public TextMeshProUGUI PlayerCurrentHpText;

    public GameObject inven_ui; //�κ��丮 ui �ǳ� ���� ����
    private bool openinven = false; //�κ��丮 �����ִ��� ���� Ȯ��

    public int PlayerLevel = 1; //�÷��̾� ����

    private void Awake()
    {
        PlayerHp = PlayerHpBar.GetComponent<HpBar>();
        PlayerHp.maxHp = 100f; //�Ŀ� json ������ �о �ִ�ü���� �Ѱ���� ��
        PlayerHp.currentHp = 100f; //�Ŀ� json ������ �о ����ü���� �Ѱ���� ��
        PlayerMaxHpText.text = PlayerHp.maxHp.ToString("F0"); //�Ŀ� json ������ �о �ִ�ü�� �ؽ�Ʈ�� �Ѱ���� ��
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0"); //�Ŀ� json ������ �о ����ü�� �ؽ�Ʈ�� �Ѱ���� ��
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //�κ��丮 i �Է� �޾��� ��
        {
            if (!openinven) //�������� �ʴٸ�
            {
                inven_ui.SetActive(true); //�κ��丮 ui Ȱ��ȭ
                openinven = true; // bool �� Ʈ��
            }
            else //���� �ִٸ� �ݱ�
            {
                inven_ui.SetActive(false);
                openinven = false;
            }
        }
    }

    public void GetExp(float value) //����ġ �޴� �Լ�
    {
        float Expvalue = value * 0.01f; //����ġ �����̴� ������ ���� �ۼ�Ʈ ������ �ٲ�
        if (ExpBar.value + Expvalue > 1f) //����ġ ȹ�� �� 100%���� ũ�ٸ�
        {
            float OverExp = (ExpBar.value + Expvalue - 1f)*100f; //���� ����ġ ���
            LevelUp(); //������ ��
            GetExp(OverExp); // ��������ġ��ŭ �ٽ� ����Լ�
        }
        else ExpBar.value += Expvalue; //����ġ ȹ��
    }

    public void LevelUp() //������
    {
        ExpBar.value = 0f;  //����ġ �ʱ�ȭ
        PlayerLevel++; //���� ����
        LevelVelueUi.text = PlayerLevel.ToString(); // level ui �κ� �ؽ�Ʈ ����
    }

    public void Damage(float damage) //������ �޴� �Լ�
    {
        PlayerHp.Dmg(damage); //HpBar ��ũ��Ʈ �Լ��� ������ �޴� �Լ� ȣ��
        if (PlayerHp.currentHp <= 0) //�ؽ�Ʈ ����
        {
            PlayerCurrentHpText.text = "0";
        }
        else
            PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0"); //float -> String ��ȯ �� �� �Ű������� F'N' ������ N��ŭ �ڸ��� ǥ��
    }

    public void Heal(float value) //ȸ�� �Լ�
    {
        PlayerHp.Heal(value);  //HpBar ��ũ��Ʈ �Լ��� ȸ�� �Լ� ȣ��
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0"); //�ؽ�Ʈ ����
    }

    public void Sliding() //�÷��̾� ���ۿ��� ȣ���ؾ� �� ��
    {
        StartCoroutine(SlidingUP()); //�ڷ�ƾ ����
    }


    //������ �� �Լ�
    // ������ ����, ���� ������ �ִ�ü�� �����ϴ� �Լ�


    IEnumerator SlidingUP() //�����̵� ��� �� �����̴��� 0���� �ٽ� 1���� ���� �Լ�
    {
        float duration = 1f; //�ִϸ��̼� �ð� (1��) <- �� ��ŭ ���� �ϸ� ��
        float elapsedTime = 0f; //��� �ð�

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; //��� �ð��� ���� (0~1)
            SlidingBar.value = Mathf.Lerp(0f, 1f, t); //���� ������ ��ǥ ������ ����
            elapsedTime += Time.deltaTime; //��� �ð� ����
            yield return null; //�� ������ ���
        }

        SlidingBar.value = 1f; //��ǥ ������ ����
    }
}
