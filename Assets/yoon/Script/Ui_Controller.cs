using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_Controller : MonoBehaviour
{
    public Slider PlayerHpBar;
    public Slider SlidingBar;
    public Slider ExpBar;
    public TextMeshProUGUI LevelVelueUi;
    public HpBar PlayerHp;
    public TextMeshProUGUI PlayerMaxHpText;
    public TextMeshProUGUI PlayerCurrentHpText;
    public Image BloodScreen;

    public GameObject inven_ui;
    private bool openinven = false;
    public GameObject iconObject;
    private Image[] Icons;

    public GameObject pro_ui;
    private bool openpro = false;

    public float duration = 1f; //�ִϸ��̼� �ð� (1��)
    public float elapsedTime = 0f; //��� �ð�
    public float CurrentHpValue; //���� ��
    public float RemainHpValue = 0; //��ǥ ��

    public bool isDown = false;
    public int PlayerLevel = 1;

    private void Awake()
    {
        PlayerHp = PlayerHpBar.GetComponent<HpBar>();
        PlayerHp.maxHp = 100f;
        PlayerHp.currentHp = 100f;
        PlayerMaxHpText.text = PlayerHp.maxHp.ToString("F0");
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0");
        Icons = iconObject.GetComponentsInChildren<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (openpro)
            {
                pro_ui.SetActive(false);
                openpro = false;
            }

            if (!openinven)
            {
                inven_ui.SetActive(true);
                openinven = true;
                Icons[0].gameObject.GetComponent<Image>().enabled = true;
                Icons[1].gameObject.GetComponent<Image>().enabled = false;
            }
            else
            {
                inven_ui.SetActive(false);
                openinven = false;
                Icons[0].gameObject.GetComponent<Image>().enabled = false;
                Icons[1].gameObject.GetComponent<Image>().enabled = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (openinven)
            {
                inven_ui.SetActive(false);
                openinven = false;
                Icons[0].gameObject.GetComponent<Image>().enabled = false;
                Icons[1].gameObject.GetComponent<Image>().enabled = true;
            }

            if (!openpro)
            {
                pro_ui.SetActive(true);
                openpro = true;
            }
            else
            {
                pro_ui.SetActive(false);
                openpro = false;
            }
        }
    }

    public void GetExp(float value)
    {
        float Expvalue = value * 0.01f;
        if (ExpBar.value + Expvalue > 1f) //���� ����ġ ���
        {
            float OverExp = (ExpBar.value + Expvalue - 1f)*100f;
            LevelUp();
            GetExp(OverExp);
        }
        else ExpBar.value += Expvalue;
    }

    public void LevelUp()
    {
        ExpBar.value = 0f;
        PlayerLevel++;
        LevelVelueUi.text = PlayerLevel.ToString();
    }

    public void Damage(float damage)
    {
        StartCoroutine("ShowBloodScreen"); //�ǰ� ȭ��ȿ�� �ڷ�ƾ
        PlayerHp.Dmg(damage);
        if (PlayerHp.currentHp <= 0)
        {
            PlayerCurrentHpText.text = "0";
        }
        else
            PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0");
    }

    public void Heal(float value)
    {
        PlayerHp.Heal(value);
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0");
    }

    public void Sliding()
    {
        StartCoroutine(SlidingUP());
    }

    IEnumerator SlidingUP()
    {
        float duration = 1f; //�ִϸ��̼� �ð� (1��)
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

    IEnumerator ShowBloodScreen() //�ǰݽ� ȭ�� �׵θ� ���� ȿ��
    {
        //���� ȭ�� ���� ���� ����
        BloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.3f, 0.4f));
        yield return new WaitForSeconds(0.15f);
        BloodScreen.color = Color.clear;    //���� �� ���� ������ 
    }


}
