using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MarketItem : MonoBehaviour
{
    public GameObject icon;
    public TextMeshProUGUI itemname;
    public TextMeshProUGUI price;
    public itemStatus randomitem;
    // Start is called before the first frame update
    void Start()
    {
        randomitem = icon.GetComponentInChildren<itemStatus>();
        randomitem.InitSetting();
        icon.GetComponent<Image>().color = randomitem.data.color;
        itemname.text = randomitem.data.itemName;
        itemname.color = randomitem.data.color;
        price.text = randomitem.data.itemPrice.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void buy_item()
    {
        Ui_Controller ui = GameManager.Instance.GetComponent<Ui_Controller>();
        inven iv = GameManager.Instance.GetComponent<inven>();
        bool EmptySloatSearch = false;
        if (ui.UseGold(int.Parse(price.text)) == true)
        {
            ui.MarketTextBox.text = "\"������ ģ��!\"";
            GameManager.Instance.GetComponent<Ui_Controller>().UiUpdate();
            if (randomitem.GetComponentInChildren<itemStatus>().data.itemNameEng == "HpPotion")
            {
                GameManager.Instance.GetComponent<Ui_Controller>().Heal(Player.instance.GetComponent<Player>().MaxHp / 2);
                Destroy(this.gameObject);
            }
            else
            {
                for (int i = 0; i < iv.inven_slots.Length; i++)
                {
                    if (iv.inven_slots[i].GetComponentInChildren<itemStatus>() == null)
                    {
                        int itemNumber = randomitem.GetComponentInChildren<itemStatus>().data.itemNumber;
                        EmptySloatSearch = true;
                        DataManager.instance.GetComponent<DataManager>().UnlockListUpdate(itemNumber);
                        Instantiate(randomitem, iv.inven_slots[i].transform);
                        Destroy(this.gameObject);
                        break;
                    }
                }
                if (!EmptySloatSearch)
                {
                    ui.MarketTextBox.text = "\"������ ���� á��!\"";
                    ui.GetGold(int.Parse(price.text)); //ȯ��
                }
            }
        }
        else
        {
            ui.MarketTextBox.text = "\"��尡 �������ݾ�!\"";
        }
    }
}