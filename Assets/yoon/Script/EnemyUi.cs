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

    public void ShowDamgeText(int DamagedValue)
    {
        ShowHpBar();
        GameObject Text = DamagedText;
        DamagedText TextCs = Text.GetComponentInChildren<DamagedText>();
        TextCs.DamagedValue.text = DamagedValue.ToString();
        TextCs.startPosition = ThisEnemy.transform.position;
        Instantiate(Text, ThisEnemy.transform.parent);
    }
}