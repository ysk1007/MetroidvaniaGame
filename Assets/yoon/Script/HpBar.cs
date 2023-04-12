using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Slider BackHpSlider;
    public bool backHpHit = false;

    public float maxHp;
    public float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f);

        if (backHpHit)
        {
            BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
        }
    }

    public void Dmg(float damage)
    {
        backHpHit = false;
        currentHp -= damage;
        Invoke("BackUpFun", 1f);
    }

    public void Heal(float value)
    { 
        if (currentHp + value >= maxHp)
        {
            currentHp = maxHp;
            return;
        }
        backHpHit = false;
        currentHp += value;
       
        Invoke("BackUpFun", 1f);
    }

    void BackUpFun()
    {
        backHpHit = true;
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
