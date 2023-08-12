using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUi : MonoBehaviour
{
    public Image HpBar;
    public Enemy ThisEnemy;
    public GameObject DamagedText;
    public GameObject BleedText;
    public GameObject HpUi;
    public float MaxHp;
    public float CurrentHp;
    public Color BleedColor = new Color32(176, 0, 0, 255);
    public Color NormarColor = new Color32(255, 255, 255, 255);
    public GameObject BleedIcon;
    public TextMeshProUGUI BleedStack;
    Transform pos;
    // Start is called before the first frame update
    void Start()
    {
        ThisEnemy = this.GetComponentInParent<Enemy>();
        pos = this.gameObject.transform;
        MaxHp = ThisEnemy.Enemy_HP;
        CurrentHp = MaxHp;
        MaxHp = CurrentHp;
        /*InvokeRepeating("ShowDamgeText", 1f, 1f);*/
    }

    // Update is called once per frame
    void Update()
    {
        CurrentHp = ThisEnemy.Enemy_HP;
        HpBar.fillAmount = CurrentHp / MaxHp;
        if (ThisEnemy.bleedingTime > 0)
        {
            HpBar.color = BleedColor;
            BleedIcon.SetActive(true);
            /*BleedStack.text = ThisEnemy.bleedStack.ToString();*/
            BleedStack.text = ThisEnemy.bleedLevel.ToString();
        }
        else if (ThisEnemy.bleedingTime < 0)
        {
            HpBar.color = NormarColor;
            BleedIcon.SetActive(false);
            /*BleedStack.text = ThisEnemy.bleedStack.ToString();*/
        }
    }

    public void ShowHpBar()
    {
        HpUi.SetActive(true);
    }

    public void ShowDamgeText(float DamagedValue, bool isCC)
    {
        ShowHpBar();
        GameObject Text = DamagedText;
        DamagedText TextCs = Text.GetComponentInChildren<DamagedText>();
        TextCs.DamagedValue.text = DamagedValue.ToString("F0");
        TextCs.startPosition = ThisEnemy.transform.position;
        if (isCC)
        {
            TextCs.DamagedValue.colorGradientPreset = TextCs.criColor;
        }
        else
        {
            TextCs.DamagedValue.colorGradientPreset = TextCs.NormalColor;
        }
        Instantiate(Text, ThisEnemy.transform.parent);
    }

    public void ShowBleedText(float DamagedValue)
    {
        ShowHpBar();
        GameObject Text = BleedText;
        BleedText TextCs = Text.GetComponentInChildren<BleedText>();
        TextCs.DamagedValue.text = DamagedValue.ToString("F0");
        TextCs.startPosition = ThisEnemy.transform.position;
        Instantiate(Text, ThisEnemy.transform.parent);
    }
}
