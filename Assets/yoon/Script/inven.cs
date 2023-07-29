using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class inven : MonoBehaviour
{
    public Button[] inven_slots = { };
    public itemStatus[] itemStatus_list_inven = { };
    public GameObject slots_obj;
    public int select_slot_index = -1;

    public Button[] equip_slots = { };
    public GameObject equips_obj;
    public int equip_slot_index = -1;

    public itemStatus itemStatus;
    public itemStatus[] itemStatus_list_equip;

    public AudioClip Equip_clip;
    private void Awake()
    {
        inven_slots = slots_obj.GetComponentsInChildren<Button>();
        equip_slots = equips_obj.GetComponentsInChildren<Button>();
        updateUi();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartEquipSpecialpower();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void select_item() //장착
    {
        if (inven_slots[select_slot_index].GetComponentInChildren<itemStatus>() != null)
        {
            itemStatus item = inven_slots[select_slot_index].GetComponentInChildren<itemStatus>();
            for (int i = 0; i < equip_slots.Length; i++)
            {
                if (equip_slots[i].GetComponentInChildren<itemStatus>() == null)
                {
                    SoundManager.instance.SFXPlay("Equip_", Equip_clip);
                    item.StatusGet(Player.instance);
                    GameManager.Instance.GetComponent<Ui_Controller>().UiUpdate();
                    Instantiate(item, equip_slots[i].transform);
                    item.data.SpecialPower = true;
                    item.SpecialPower();
                    Destroy(item.transform.gameObject);
                    equip_slots[i].GetComponentInChildren<Image>().color = Color.green;
                    break;
                }
            }
        }
        updateUi();
        select_slot_index = -1;
    }

    public void select_equip_slot() //해체
    {
        if (equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>() != null)
        {
            itemStatus item = equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>();
            for (int i = 0; i < inven_slots.Length; i++)
            {
                if (inven_slots[i].GetComponentInChildren<itemStatus>() == null)
                {
                    SoundManager.instance.SFXPlay("Equip_", Equip_clip);
                    item.StatusReturn(Player.instance);
                    GameManager.Instance.GetComponent<Ui_Controller>().UiUpdate();
                    Instantiate(item, inven_slots[i].transform);
                    item.data.SpecialPower = false;
                    item.SpecialPower();
                    Destroy(item.transform.gameObject);
                    equip_slots[equip_slot_index].GetComponentInChildren<Image>().color = Color.white;
                    break;
                }
            }
        }
        updateUi();
        equip_slot_index = -1;
    }

    public void sell_item() //판매
    {
        if (inven_slots[select_slot_index].GetComponentInChildren<itemStatus>() != null)
        {
            itemStatus item = inven_slots[select_slot_index].GetComponentInChildren<itemStatus>();
            float sellprice = (float)item.data.itemPrice * 0.3f;
            int newprice = (int)sellprice;
            GameManager.Instance.GetComponent<Ui_Controller>().GetGold(newprice);
            Destroy(item.transform.gameObject);
        }
        updateUi();
        select_slot_index = -1;
    }

    public void updateUi() // 전부 갱신
    {
        updateInven();
        updateEquip();
    }

    void updateInven() //인벤토리 갱신
    {
        itemStatus[] append = new itemStatus[inven_slots.Length];
        for (int i = 0; i < inven_slots.Length; i++)
        {
            append[i] = inven_slots[i].GetComponentInChildren<itemStatus>();
            if (append[i] != null)
            {
                append[i].InitSetting();
            }

        }
        itemStatus_list_inven = append;
    }

    void updateEquip() //장착 슬롯 갱신
    {
        itemStatus[] append = new itemStatus[equip_slots.Length];
        for (int i = 0; i < equip_slots.Length; i++)
        {
            append[i] = equip_slots[i].GetComponentInChildren<itemStatus>();
            if (append[i] != null)
            {
                append[i].InitSetting();
            }

        }
        itemStatus_list_equip = append;
    }
    void StartEquipSpecialpower() //시작할 때 착용한 장비 특수 능력치 키는 함수
    {
        itemStatus[] append = new itemStatus[equip_slots.Length];
        for (int i = 0; i < equip_slots.Length; i++)
        {
            append[i] = equip_slots[i].GetComponentInChildren<itemStatus>();
            if (append[i] != null)
            {
                append[i].InitSetting();
                append[i].data.SpecialPower = true;
                append[i].SpecialPower();
            }

        }
        itemStatus_list_equip = append;
    }
}
