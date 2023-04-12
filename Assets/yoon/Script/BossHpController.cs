using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; //Æ©ÇÃ À§ÇÔ

public class BossHpController : MonoBehaviour
{
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

    public void BossHit(float damage)
    {
        HpSliders[currentHpLine - 1].Dmg(damage);
        if (HpSliders[currentHpLine - 1].currentHp < 0 && currentHpLine != 1)
        {
            float overdmg = Math.Abs(HpSliders[currentHpLine - 1].currentHp);
            currentHpLine--;
            LineCount.text = "X " + currentHpLine.ToString();
            HpSliders[currentHpLine - 1].Dmg(overdmg);
        }
        
    }
}
