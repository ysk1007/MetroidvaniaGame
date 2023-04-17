using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    public Slider hpSlider; // HP 슬라이더를 담을 객체
    public Slider BackHpSlider; // 잔상 슬라이더를 담을 객체
    public bool backHpHit = false; // 잔상 체력이 떨어지고 있는지 확인, 제어하는 bool 값

    public float maxHp; //최대 체력
    public float currentHp; //현재 체력

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //슬라이더의 밸류는 [현재 체력/최대 체력] Mathf.Lerp로 현재 밸류에서 목표 밸류까지 부드럽게 감소
        hpSlider.value = Mathf.Lerp(hpSlider.value, currentHp / maxHp, Time.deltaTime * 5f); 

        if (backHpHit) //잔상 체력 깎게 제어 들어옴
        {
            //잔상체력 슬라이더의 밸류를 체력 슬라이더의 밸류만큼 부드럽게 감소
            BackHpSlider.value = Mathf.Lerp(BackHpSlider.value, hpSlider.value, Time.deltaTime * 10f);
        }
    }

    public void Dmg(float damage) //데미지를 입는 함수 (외부에서 불러옴)
    {
        backHpHit = false; //잔상이 멈추도록 함
        currentHp -= damage; //현재 체력에서 밸류만큼 깎음
        Invoke("BackUpFun", 1f); //1초뒤 BackUpFun 함수
    }

    public void Heal(float value) //회복하는 함수
    { 
        if (currentHp + value >= maxHp) //회복했을때 최대체력을 넘는것을 방지
        {
            currentHp = maxHp;
            return;
        }
        backHpHit = false; 
        currentHp += value; //현재 체력에서 밸류만큼 더함

        Invoke("BackUpFun", 1f);
    }

    void BackUpFun() //잔상 체력 깎게 함
    {
        backHpHit = true;
    }

    public void SelfDestroy() //체력바 없앰 (보스 전용)
    {
        Destroy(gameObject);
    }
}
