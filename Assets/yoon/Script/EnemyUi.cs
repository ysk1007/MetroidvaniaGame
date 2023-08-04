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
        TextCs.DamagedValue.text = DamagedValue.ToString();
        TextCs.startPosition = ThisEnemy.transform.position;
        if (isCC)
        {
            TextCs.DamagedValue.color = Color.yellow;
        }
        else
        {
            TextCs.DamagedValue.color = Color.white;
        }
        Instantiate(Text, ThisEnemy.transform.parent);
    }

    public void ShowBleedText(float DamagedValue)
    {
        ShowHpBar();
        GameObject Text = BleedText;
        BleedText TextCs = Text.GetComponentInChildren<BleedText>();
        TextCs.DamagedValue.text = DamagedValue.ToString();
        TextCs.startPosition = ThisEnemy.transform.position;
        Instantiate(Text, ThisEnemy.transform.parent);
    }
}
