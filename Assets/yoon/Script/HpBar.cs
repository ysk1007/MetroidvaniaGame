using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpSlider;
    public Slider BackHpSlider;
    public bool backHpHit = false;

    public Image BloodImage;
    private bool Blood = false;

    public float maxHp;
    public float currentHp;

    // Start is called before the first frame update
    void Start()
    {
        BackUpFun();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= maxHp/5 && !Blood && BloodImage != null)
        {
            StartCoroutine("blood");
        }
        if (currentHp > maxHp / 5 && BloodImage != null)
        {
            BloodImage.enabled = false;
            Blood = false;
        }
        if (!Blood && BloodImage != null)
        {
            StopCoroutine("blood");
        }
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
        gameObject.SetActive(false);
    }

    public void SelfCreate()
    {
        gameObject.SetActive(true);
    }
    IEnumerator blood()
    {
        Blood = true;
        BloodImage.enabled = true;
        // Fade out
        float t = 0f; //0%
        while (t< 1f) //100%까지
            {
                t += Time.deltaTime* 2f;
                Color color = BloodImage.color;
                color.a = Mathf.Lerp(1f, 0f, t); //하향 1차 그래프
                BloodImage.color = color;
                yield return null;
            }
        yield return new WaitForSeconds(1f);
        StartCoroutine("blood");
    }


}
