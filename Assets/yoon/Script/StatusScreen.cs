using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatusScreen : MonoBehaviour
{
    public TextMeshProUGUI MaxHpValue;
    public TextMeshProUGUI DamageValue;
    public TextMeshProUGUI DmgIncValue;
    public TextMeshProUGUI ATSValue;
    public TextMeshProUGUI SpeedValue;
    public TextMeshProUGUI CCValue;
    public TextMeshProUGUI CriDmgValue;
    public TextMeshProUGUI CoolTimeValue;
    public TextMeshProUGUI DefValue;
    public TextMeshProUGUI LifeStillValue;
    public TextMeshProUGUI GetGoldValue;
    public TextMeshProUGUI GetExpValue;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void StatusUpdate(Player player)
    {
        MaxHpValue.text = player.MaxHp.ToString("F1");
        DamageValue.text = (player.AtkPower + player.DmgChange + player.GridPower).ToString("F0");
        DmgIncValue.text = (player.DmgIncrease * 100f).ToString("F0") + "%";
        ATSValue.text = player.ATS.ToString("F1");
        SpeedValue.text = (player.SpeedChange * 100f).ToString("F0") + "%";
        CCValue.text = (player.CriticalChance * 100f).ToString("F0") + "%";
        CriDmgValue.text = (player.CriDmgIncrease * 100f).ToString("F0") + "%";
        CoolTimeValue.text = (player.DecreaseCool * 100f).ToString("F0") + "%";
        if (player.Def > 0)
        {
            DefValue.text = (0.007f * player.Def * 100f).ToString("F0") + "%";
        }
        else if (player.Def == 0)
        {
            DefValue.text = "0%";
        }
        else
        {
            DefValue.text = "- "+(-0.05f * player.Def * 100f).ToString("F0") + "%";
        }
        LifeStillValue.text = (player.lifeStill * 100f).ToString("F0") + "%";
        GetGoldValue.text = (player.GoldGet * 100f).ToString("F0") + "%";
        GetExpValue.text = (player.EXPGet * 100f).ToString("F0") + "%";
    }

    public void StatusScreenOpen()
    {
        StatusUpdate(Player.instance.GetComponent<Player>());
        gameObject.SetActive(true);
    }

    public void StatusScreenClose()
    {
        gameObject.SetActive(false);
    }
}
