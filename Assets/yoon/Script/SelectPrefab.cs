using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectPrefab : MonoBehaviour
{
    public string Name;
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI ExplainText;

    public Image Icon;
    public Image Case;

    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI Lv1Value;
    public TextMeshProUGUI Lv2Value;
    public TextMeshProUGUI Lv3Value;
    public TextMeshProUGUI[] values;

    public Color32 red = new Color32(211, 11, 34, 255);
    public Color32 blue = new Color32(28, 101, 246, 255);
    public Color32 purple = new Color32(222, 12, 179, 255);

    public int ThisIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ThisSelect()
    {
        GameManager.Instance.GetComponent<SelectUi>().selectButton(ThisIndex);
    }

    public void Setting()
    {
        Player player = Player.instance.GetComponent<Player>();
        Icon.sprite = Resources.Load<Sprite>("Select_icon/" + Name + "_icon");
        switch (Name)
        {
            case "selectAtkLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectAtkValue[i] * 100f).ToString();
                }
                NameText.text = "싸움꾼";
                ExplainText.text = "공격시 추가 피해를 입힙니다.";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectATSLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectATSValue[i] * 100f).ToString();
                }
                NameText.text = "광란";
                ExplainText.text = "추가 공격 속도를 \n 획득 합니다.";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectCCLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectCCValue[i] * 100f).ToString();
                }
                NameText.text = "급소";
                ExplainText.text = "치명타 확률이 증가합니다.";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectLifeStillLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectLifeStillValue[i] * 100f).ToString();
                }
                NameText.text = "생명력 흡수";
                ExplainText.text = "피해량의 일정 비율만큼 \n 체력을 회복합니다.";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectDefLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectDefValue[i].ToString();
                }
                NameText.text = "기사";
                ExplainText.text = "추가 방어력을 획득 합니다.\n 받는 피해량 감소";
                NameText.color = blue;
                Case.color = blue;
                break;
            case "selectHpLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectHpValue[i].ToString();
                }
                NameText.text = "최대체력";
                ExplainText.text = "최대 체력이 증가 합니다.";
                NameText.color = blue;
                Case.color = blue;
                break;
            case "selectGoldLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectGoldValue[i] * 100f).ToString();
                }
                NameText.text = "금수저";
                ExplainText.text = "아이템 판매, 몬스터 처치 시, \n 얻는 골드량이 증가합니다.";
                NameText.color = purple;
                Case.color = purple;
                break;
            case "selectExpLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectExpValue[i] * 100f).ToString();
                }
                NameText.text = "학자";
                ExplainText.text = "몬스터 처치 시, 얻는 경험치량이 증가 합니다.";
                NameText.color = purple;
                Case.color = purple;
                break;
            case "selectCoolTimeLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = (player.selectCoolTimeValue[i] * 100f).ToString();
                }
                NameText.text = "쿨타임 감소";
                ExplainText.text = "스킬 쿨타임이 감소합니다.";
                NameText.color = purple;
                Case.color = purple;
                break;
        }
    }
}
