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
                    values[i].text = player.selectAtkValue[i].ToString();
                }
                NameText.text = "�ο��";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectATSLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectATSValue[i].ToString();
                }
                NameText.text = "����";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectCCLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectCCValue[i].ToString();
                }
                NameText.text = "�޼�";
                NameText.color = red;
                Case.color = red;
                break;
            case "selectDefLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectDefValue[i].ToString();
                }
                NameText.text = "���";
                NameText.color = blue;
                Case.color = blue;
                break;
            case "selectHpLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectHpValue[i].ToString();
                }
                NameText.text = "�ִ�ü��";
                NameText.color = blue;
                Case.color = blue;
                break;
            case "selectGoldLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectGoldValue[i].ToString();
                }
                NameText.text = "�ݼ���";
                NameText.color = purple;
                Case.color = purple;
                break;
            case "selectExpLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectExpValue[i].ToString();
                }
                NameText.text = "����";
                NameText.color = purple;
                Case.color = purple;
                break;
            case "selectCoolTimeLevel":
                for (int i = 0; i < 3; i++)
                {
                    values[i].text = player.selectCoolTimeValue[i].ToString();
                }
                NameText.text = "��Ÿ�� ����";
                NameText.color = purple;
                Case.color = purple;
                break;
        }
    }
}
