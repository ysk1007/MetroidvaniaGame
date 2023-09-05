using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MouseOnItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemExplanationText;
    public TextMeshProUGUI ItemStatText;
    public TextMeshProUGUI PriceText;
    public Image Itemimg;
    public GameObject DescriptionGo;
    public GameObject instance;
    public bool MouseOn = false;
    public int thisIndex;
    public string slot_type;

    void Update()
    {
        if (Input.GetKey(KeyCode.E))
        {
            MouseOn = false;    
        }
        if (Input.GetMouseButtonUp(1) && MouseOn)
        {
            if (slot_type == "inven")
            {

                if (MarketScript.instance != null && MarketScript.instance.MarketOpen)
                {
                    Debug.Log("�Ǹ�");
                    GameManager.Instance.GetComponent<inven>().sell_item();
                    MouseOn = false;
                    Destroy(instance);
                }
                else
                {
                    Debug.Log("����");
                    GameManager.Instance.GetComponent<inven>().select_item();
                    MouseOn = false;
                    Destroy(instance);
                }
            }
            else if (slot_type == "Market")
            {
                Debug.Log("����");
                this.gameObject.GetComponent<MarketItem>().buy_item();
                MouseOn = false;
                Destroy(instance);
            }
            else if (slot_type == "Material")
            {
                return;
            }
            else
            {
                Debug.Log("����");
                GameManager.Instance.GetComponent<inven>().select_equip_slot();
                MouseOn = false;
                Destroy(instance);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���콺 Ŀ���� UI�� �������� �� ����� �ڵ� �ۼ�
        if (this.GetComponentInChildren<itemStatus>() != null)
        {
            itemStatus itemstat = this.GetComponentInChildren<itemStatus>();
            if (instance == null)
            {
                ShowStatus(itemstat, slot_type);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺 Ŀ���� UI�� ����� �� ����� �ڵ� �ۼ�
        MouseOn = false;
        Destroy(instance);
    }

    void ShowStatus(itemStatus itemstat, string slot_type)
    {
        MouseOn = true;
        Vector3 vc = new Vector3(0,0,0);
        string tiptext;
        string pricetext;
        bool isSell = false;
        if (slot_type == "inven")
        {
            GameManager.Instance.GetComponent<inven>().select_slot_index = thisIndex;
            vc.x = -380;
            vc.y = 60;
            tiptext = "��Ŭ�� �Ͽ� ����";
            if (MarketScript.instance != null)
            {
                if (MarketScript.instance.MarketOpen)
                {
                    tiptext = "��Ŭ�� �Ͽ� �Ǹ�";
                }
            }
            pricetext = "�ǸŰ� : ";
            isSell = true;
        }
        else if (slot_type == "Market")
        {
            GameManager.Instance.GetComponent<inven>().equip_slot_index = thisIndex;
            vc.x = 720;
            vc.y = 60;
            tiptext = "��Ŭ�� �Ͽ� ����";
            pricetext = "���Ű� : ";
        }
        else if (slot_type == "Material")
        {
            vc.y = -360;
            tiptext = "";
            pricetext = "";
        }
        else
        {
            GameManager.Instance.GetComponent<inven>().equip_slot_index = thisIndex;
            vc.x = 380;
            vc.y = -60;
            tiptext = "��Ŭ�� �Ͽ� ���� ����";
            pricetext = "�ǸŰ� : ";
            isSell = true;
        }
        Vector3 newPosition = gameObject.transform.position + vc;
        instance = Instantiate(DescriptionGo, newPosition, Quaternion.identity);
        instance.transform.SetParent(gameObject.transform.parent.transform.parent.transform.parent.transform.parent.transform.parent);
        itemstat.InitSetting();
        Description_Setting tip = instance.GetComponent<Description_Setting>();
        itemstat.Using(tip.ItemIcon, tip.ItemName, tip.ItemDescription, tip.ItemStatus, tip.ItemPrice, tip.RateText);
        tip.tipText.text = tiptext;
        GameManager.Instance.GetComponent<Ui_Controller>().DescriptionBox = instance;
        if (isSell)
        {
            float sellprice = float.Parse(tip.ItemPrice.text) * 0.3f;
            int newprice = (int)sellprice;
            tip.ItemPrice.text = pricetext + newprice.ToString();
        }
        else
        {
            tip.ItemPrice.text = pricetext + tip.ItemPrice.text;
        }
    }
}
