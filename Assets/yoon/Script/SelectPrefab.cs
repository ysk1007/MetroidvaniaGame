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
                    values[i].text = player.selectAtkLevel[i].ToString();
                }
                NameText.text = "싸움꾼";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectATSLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectATSLevel[i].ToString();
                }
                NameText.text = "광란";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectCCLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectCCLevel[i].ToString();
                }
                NameText.text = "급소";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectDefLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectDefLevel[i].ToString();
                }
                NameText.text = "기사";
                NameText.color = blue;
                Case.color = blue;
                break;
            case "selectHpLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectHpLevel[i].ToString();
                }
                NameText.text = "최대체력";
                NameText.color = blue;
                Case.color = blue;
                break;
            case "selectGoldLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectGoldLevel[i].ToString();
                }
                NameText.text = "금수저";
                NameText.color = purple;
                Case.color = purple;
                break;
            case "selectExpLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectExpLevel[i].ToString();
                }
                NameText.text = "학자";
                NameText.color = purple;
                Case.color = purple;
                break;
            case "selectCoolTimeLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectCoolTimeLevel[i].ToString();
                }
                NameText.text = "쿨타임 감소";
                NameText.color = purple;
                Case.color = purple;
                break;
        }
    }
}
