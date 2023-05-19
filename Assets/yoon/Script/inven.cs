using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class inven : MonoBehaviour
{
    public Button[] inven_slots = { };
    public GameObject slots_obj;
    public int select_slot_index = -1;
    public GameObject select_case;

    public Button[] equip_slots = { };
    public GameObject equips_obj;
    public int equip_slot_index = -1;
    public GameObject equip_case;

    public itemStatus itemStatus;
    public itemStatus[] itemStatus_list;

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemExplanationText;
    public TextMeshProUGUI ItemStatText;
    public Image Itemimg;

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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void inven_SlotSelect(int i) //인벤토리에서 아이템 클릭
    {
        if (select_slot_index == i)
        {
            select_item();
        }
        else
        {
            equip_case.SetActive(false);
            select_case.SetActive(true);

            select_slot_index = i;
            select_case.transform.position = inven_slots[i].transform.position;
            if (inven_slots[i].GetComponentInChildren<itemStatus>() != null)
            {
                itemStatus itemstat = inven_slots[i].GetComponentInChildren<itemStatus>();
                itemstat.Using(Itemimg,ItemNameText, ItemExplanationText, ItemStatText);
            }
            else
            {
                TextReset();
            }
            
        }
        
    }

    public void Equip_SlotSelect(int i) //장착슬롯에서 아이템 클릭
    {
        if (equip_slot_index == i)
        {
            SoundManager.instance.SFXPlay("Equip_", Equip_clip);
            select_equip_slot();
        }
        else
        {
            select_case.SetActive(false);
            equip_case.SetActive(true);

            equip_slot_index = i;
            equip_case.transform.position = equip_slots[i].transform.position;
            if (equip_slots[i].GetComponentInChildren<itemStatus>() != null)
            {
                itemStatus itemstat = equip_slots[i].GetComponentInChildren<itemStatus>();
                itemstat.Using(Itemimg, ItemNameText, ItemExplanationText, ItemStatText);
            }
            else
            {
                TextReset();
            }
        }
        

    }

    void select_item() //장착
    {
        select_case.SetActive(false);
        equip_case.SetActive(false);
        if (inven_slots[select_slot_index].GetComponentInChildren<itemStatus>() != null)
        {
            itemStatus item = inven_slots[select_slot_index].GetComponentInChildren<itemStatus>();
            for (int i = 0; i < equip_slots.Length; i++)
            {
                if (equip_slots[i].GetComponentInChildren<itemStatus>() == null)
                {
                    SoundManager.instance.SFXPlay("Equip_", Equip_clip);
                    Instantiate(item, equip_slots[i].transform);
                    Destroy(item.transform.gameObject);
                    equip_slots[i].GetComponentInChildren<Image>().color = Color.green;
                    break;
                }
            }
        }
        updateUi();
        select_slot_index = -1;
    }

    void select_equip_slot() //해체
    {
        select_case.SetActive(false);
        equip_case.SetActive(false);
        if (equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>() != null)
        {
            itemStatus item = equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>();
            for (int i = 0; i < inven_slots.Length; i++)
            {
                if (inven_slots[i].GetComponentInChildren<itemStatus>() == null)
                {
                    Instantiate(item, inven_slots[i].transform);
                    Destroy(item.transform.gameObject);
                    equip_slots[equip_slot_index].GetComponentInChildren<Image>().color = Color.white;
                    break;
                }
            }
        }
        updateUi();
        equip_slot_index = -1;
    }

    void updateUi() // 전부 갱신
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
        itemStatus_list = append;
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
        itemStatus_list = append;
    }

    void TextReset() //아이템 설명 null
    {
        Itemimg.sprite = null;
        Itemimg.color = new Color32(255, 255, 255, 0);
        ItemNameText.text = null;
        ItemExplanationText.text = null;
        ItemStatText.text = null;
    }
}
