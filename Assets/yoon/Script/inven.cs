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

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemExplanationText;
    public Image Itemimg;
    private void Awake()
    {
        inven_slots = slots_obj.GetComponentsInChildren<Button>();
        equip_slots = equips_obj.GetComponentsInChildren<Button>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void inven_SlotSelect(int i)
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
                itemstat.Using(Itemimg,ItemNameText, ItemExplanationText);
            }
            else
            {
                Itemimg.sprite = null;
                Itemimg.color = new Color32(255, 255, 255, 0);
                ItemNameText.text = "";
                ItemExplanationText.text = "";
            }
            
        }
        
    }

    public void Equip_SlotSelect(int i)
    {
        if (equip_slot_index == i)
        {
            select_equip_slot();
        }
        else
        {
            select_case.SetActive(false);
            equip_case.SetActive(true);

            equip_slot_index = i;
            equip_case.transform.position = equip_slots[i].transform.position;
            
        }
        

    }

    void select_item()
    {
        select_case.SetActive(false);
        equip_case.SetActive(false);
        select_slot_index = -1;
    }

    void select_equip_slot()
    {
        select_case.SetActive(false);
        equip_case.SetActive(false);
        equip_slot_index = -1;
    }
}
