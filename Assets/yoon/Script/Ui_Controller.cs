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
    public int PlayerLevel;
    public HpBar PlayerHp;
    public TextMeshProUGUI PlayerMaxHpText;
    public TextMeshProUGUI PlayerCurrentHpText;
    public TextMeshProUGUI AtkPowerValueText;
    public TextMeshProUGUI DefValueText;
    public TextMeshProUGUI AtkSpeedValueText;
    public TextMeshProUGUI DmgIncreaseValueText;
    public TextMeshProUGUI MarketTextBox;
    public TextMeshProUGUI MarketGoldText;
    public Image BloodScreen;
    public Player player;

    public TextMeshProUGUI GoldVelueUI;
    public int PlayerGold;

    public GameObject inven_ui;
    public GameObject inven_screen;
    public GameObject equip_screen;
    private bool openinven = false;
    public bool openMarket = false;
    public GameObject iconObject;
    private Image[] Icons;

    public GameObject pro_ui;
    private bool openpro = false;

    public GameObject Select_ui;
    public GameObject Select_error_text;
    public bool openSelect = false;

    public float duration = 1f; //�ִϸ��̼� �ð� (1��)
    public float elapsedTime = 0f; //��� �ð�
    public float CurrentHpValue; //���� ��
    public float RemainHpValue = 0; //��ǥ ��

    public bool isDown = false;

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        UiUpdate();
        Icons = iconObject.GetComponentsInChildren<Image>();
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
        Select_ui.SetActive(true);
        openSelect = true;
        this.gameObject.GetComponent<SelectUi>().OpenSelectUi();
        Time.timeScale = 0f;
        ExpBar.value = 0f;
        player.level++;
        LevelVelueUi.text = player.level.ToString();
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

    public void GetGold(int value)
    {
        player.gold += value;
        GoldVelueUI.text = player.gold.ToString();
        MarketGoldText.text = player.gold.ToString();
    }

    public bool UseGold(int value)
    {
        if (player.gold - value < 0)
        {
            Debug.Log("��� ����!");
            return false;
        }
        else
        {
            player.gold -= value;
            GoldVelueUI.text = player.gold.ToString();
            MarketGoldText.text = player.gold.ToString();
            return true;
        }
    }

    public void UiUpdate()
    {
        //ü�¹� ����
        PlayerHp = PlayerHpBar.GetComponent<HpBar>();
        PlayerHp.maxHp = player.MaxHp;
        PlayerHp.currentHp = player.CurrentHp;
        PlayerMaxHpText.text = PlayerHp.maxHp.ToString("F0");
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0");

        //�κ��丮 ����
        AtkPowerValueText.text = player.AtkPower.ToString();
        DefValueText.text = player.Def.ToString();
        AtkSpeedValueText.text = player.delayTime.ToString();
        DmgIncreaseValueText.text = player.DmgIncrease.ToString("F0")+"%";

        //���� ��� ����
        PlayerLevel = player.level;
        LevelVelueUi.text = player.level.ToString();
        GoldVelueUI.text = player.gold.ToString();
        MarketGoldText.text = player.gold.ToString();
    }

    IEnumerator SlidingUP()
    {
        float duration = 2.5f; //�ִϸ��̼� �ð� (1��)
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
