using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    public GameObject axe_bar;
    public Image bar_Gauge;
    public Player player;
    public GameObject charging_effect;
    public Animator charging_Anim;
    public Animator LevelUp_Anim;
    public bool Full_Charge = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bar_Gauge.fillAmount == 1f && Full_Charge == false)
        {
            Full_Charge = true;
            charging_effect.SetActive(true);
            charging_Anim.SetTrigger("Charging");
        }
    }

    public void GuageIncrease(float value)
    {
        bar_Gauge.fillAmount = Mathf.Clamp(value, 0f, 1f);
    }

    public void ChargeStart()
    {
        axe_bar.SetActive(true);
        charging_effect.SetActive(false);
        Full_Charge = false;
        bar_Gauge.fillAmount = 0f;
    }

    public void ChargeEnd()
    {
        axe_bar.SetActive(false);
    }

    public void LevelUpEffect()
    {
        LevelUp_Anim.SetTrigger("LevelUp");
    }
}
