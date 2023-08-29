using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Diagnostics;

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
    public TextMeshProUGUI CriticalChanceText;
    public TextMeshProUGUI MarketTextBox;
    public TextMeshProUGUI MarketGoldText;
    public Image BloodScreen;
    public Player player;

    public TextMeshProUGUI GoldVelueUI;
    public int PlayerGold;

    public GameObject inven_ui;
    public GameObject inven_screen;
    public GameObject equip_screen;
    public GameObject Status_screen;
    public GameObject WeaponSelect_screen;
    public GameObject DescriptionBox;
    public GameObject StatisticsUi;
    private bool openinven = false;
    public bool openMarket = false;
    public GameObject iconObject;
    private Image[] Icons;

    public GameObject pro_ui;
    private bool openpro = false;

    public GameObject Select_ui;
    public GameObject Select_error_text;
    public bool openSelect = false;

    public float duration = 1f; //애니메이션 시간 (1초)
    public float elapsedTime = 0f; //경과 시간
    public float CurrentHpValue; //시작 값
    public float RemainHpValue = 0; //목표 값

    public bool isDown = false;

    public float[] ExpValue = {10f,20f,30f}; // 1,2,3 스테이지 값
    public float[] GoldValue = {50f,75f,90f}; // 1,2,3 스테이지 값
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
        AtkPowerValueText.text = (player.AtkPower + player.ATP + player.GridPower).ToString("F0");
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
                Status_screen.SetActive(false);
                Icons[0].gameObject.GetComponent<Image>().enabled = true;
                Icons[1].gameObject.GetComponent<Image>().enabled = false;
            }
            else
            {
                inven_ui.SetActive(false);
                openinven = false;
                Destroy(DescriptionBox);
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
                if (Player.instance.proSelectWeapon == 4)
                {
                    WeaponSelect_screen.SetActive(true);
                    WeaponSelect_screen.GetComponent<WeaponSelect>().WeaponSelectUiOpen = true;
                }
                pro_ui.SetActive(true);
                openpro = true;
            }
            else
            {
                WeaponSelect_screen.SetActive(false);
                WeaponSelect_screen.GetComponent<WeaponSelect>().WeaponSelectUiOpen = false;
                pro_ui.SetActive(false);
                openpro = false;
            }
        }
    }

    public void GetExp(int stage)
    {
        float MaxExp = player.ExpBarValue[player.level - 1];
        float value = ExpValue[stage - 1];
        float Expvalue = Mathf.Lerp(0f, 1f, value * player.EXPGet / MaxExp);
        if (ExpBar.value + Expvalue > 1f) //오버 경험치 계산
        {
            float OverExp = (ExpBar.value + Expvalue - 1f)*100f;
            LevelUp();
            RemainGetExp(OverExp);
        }
        else ExpBar.value += Expvalue;
    }

    public void RemainGetExp(float value)
    {
        float Expvalue = value * 0.01f;
        ExpBar.value += Expvalue;
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
        StartCoroutine("ShowBloodScreen"); //피격 화면효과 코루틴
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
        if (player.CurrentHp + value <= 0)
        {
            PlayerHp.currentHp = 1;
            PlayerCurrentHpText.text = 1.ToString("F0");
            player.CurrentHp = int.Parse(PlayerCurrentHpText.text);
        }
        else
        {
            PlayerHp.Heal(value);
            PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0");
            player.CurrentHp = int.Parse(PlayerCurrentHpText.text);
        }
    }

    public void Sliding()
    {
        StartCoroutine(SlidingUP());
    }

    public void GetGold(int stage)
    {
        float value = GoldValue[stage - 1];
        player.gold += value * player.GoldGet;
        player.TotalGetGold += value * player.GoldGet;
        GoldVelueUI.text = player.gold.ToString();
        MarketGoldText.text = player.gold.ToString();
    }

    public void GetGold(float price)
    {
        player.gold += price * player.GoldGet;
        player.TotalGetGold += price * player.GoldGet;
        GoldVelueUI.text = player.gold.ToString();
        MarketGoldText.text = player.gold.ToString();
    }

    public bool UseGold(int value)
    {
        if (player.gold - value < 0)
        {
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
        //체력바 갱신
        PlayerHp = PlayerHpBar.GetComponent<HpBar>();
        PlayerHp.maxHp = player.MaxHp;
        PlayerHp.currentHp = player.CurrentHp;
        PlayerMaxHpText.text = PlayerHp.maxHp.ToString("F0");
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0");

        //인벤토리 갱신
        AtkPowerValueText.text = (player.AtkPower + player.ATP + player.GridPower + player.VulcanPower).ToString("F0");
        DefValueText.text = player.Def.ToString();
        AtkSpeedValueText.text = player.ATS.ToString("F1");
        CriticalChanceText.text = (player.CriticalChance * 100f).ToString() + "%";

        //레벨 골드 갱신
        PlayerLevel = player.level;
        LevelVelueUi.text = player.level.ToString();
        GoldVelueUI.text = player.gold.ToString();
        MarketGoldText.text = player.gold.ToString();

        Status_screen.GetComponent<StatusScreen>().StatusUpdate(player);
    }

    IEnumerator SlidingUP()
    {
        float duration = player.SlidingCool + 0.5f; //애니메이션 시간 (0.5는 무적시간)
        float elapsedTime = 0f; //경과 시간

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; //경과 시간의 비율 (0~1)
            SlidingBar.value = Mathf.Lerp(0f, 1f, t); //시작 값에서 목표 값으로 보간
            elapsedTime += Time.deltaTime; //경과 시간 증가
            yield return null; //한 프레임 대기
        }

        SlidingBar.value = 1f; //목표 값으로 설정
    }

    IEnumerator ShowBloodScreen() //피격시 화면 테두리 붉은 효과
    {
        //붉은 화면 투명도 값을 조정
        BloodScreen.color = new Color(1, 0, 0, UnityEngine.Random.Range(0.3f, 0.4f));
        yield return new WaitForSeconds(0.15f);
        BloodScreen.color = Color.clear;    //지연 후 원래 색으로 
    }


}
