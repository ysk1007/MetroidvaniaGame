using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public struct Data //아이템
{
    public Image itemimg; //이미지
    public AudioClip equipSfx; //장착 효과음
    public string itemName; //이름
    public string itemExplanation; //설명
    public string itemStat; //스탯
    public int itemNumber; //아이템 고유 번호

    public int AtkPower; //공격력
    public float AtkSpeed; //공속
    public int Def; //방어력
    public int MaxHp; //최대체력
    public float Speed; //이동속도
    public float CriticalChance; //치명타 확률
}

public abstract class itemStatus : MonoBehaviour
{
    public Data data;

    public abstract void InitSetting(); //abstract -> 상속받은 스크립트에서 데이터 선언

    public virtual void Using(Image img,TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText)  //virtual 은 상위에서 기능 구현 함
    {
        TextImageSettings(img, NameText, ExplanationText, StatText);
    }

    public virtual void TextImageSettings(Image img, TextMeshProUGUI NameText, TextMeshProUGUI ExplanationText, TextMeshProUGUI StatText) //텍스트 및 이미지 세팅
    {
        img.sprite = this.data.itemimg.sprite; //자신의 이미지 스프라이트를 가져옴
        img.SetNativeSize(); //SetNativeSize 실제 png 사이즈로 변환 (이미지마다 비율이 달라서 한 번 초기화 함)

        RectTransform rect = (RectTransform)img.transform; //초기화 한 사이즈 가져옴
        rect.sizeDelta = new Vector2(rect.sizeDelta.x * 5, rect.sizeDelta.y * 5); // 5 곱함 (아이콘 크기)
        img.rectTransform.sizeDelta = rect.sizeDelta; // 사이즈 적용

        img.color = new Color32(255, 255, 255, 255); // 투명도 100%

        NameText.text = this.data.itemNumber.ToString()+". " + this.data.itemName;  //아이템 이름 텍스트 부분 고유번호 붙여서 갱신
        ExplanationText.text = this.data.itemExplanation; //설명 텍스트 부분 갱신
        StatText.text = this.data.itemStat; //스탯 텍스트 부분 갱신
    }
}
