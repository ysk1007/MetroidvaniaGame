using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class inven : MonoBehaviour
{
    public Button[] inven_slots = { }; //�κ��丮 ��ư�� ���� �迭
    public GameObject slots_obj; //���� �κ��丮 ��ư���� ���� ������Ʈ
    public int select_slot_index = -1; //�κ��丮 ��ư�� �� ���õ� �ε���
    public GameObject select_case; //���� ������ ������Ʈ

    public Button[] equip_slots = { }; //���� ���� ��ư�� ���� �迭
    public GameObject equips_obj; //���� ���� ���� ��ư���� ���� ������Ʈ
    public int equip_slot_index = -1; //���� ���� ��ư�� �� ���õ� �ε���
    public GameObject equip_case; //���� ������ ������Ʈ

    public itemStatus itemStatus; //������ ���� �ڵ带 ������
    public itemStatus[] itemStatus_list; //���� �����ϰ��ִ� ������ ����Ʈ �迭

    public TextMeshProUGUI ItemNameText; //������ �̸� �ؽ�Ʈ
    public TextMeshProUGUI ItemExplanationText; //���� �ؽ�Ʈ
    public TextMeshProUGUI ItemStatText; //�ؽ�Ʈ �ؽ�Ʈ
    public Image Itemimg; //������ �̹���

    public AudioClip Equip_clip; //����/���� ȿ�� ����
    private void Awake()
    {
        inven_slots = slots_obj.GetComponentsInChildren<Button>(); //�κ��丮 ��ư ������Ʈ���� ������
        equip_slots = equips_obj.GetComponentsInChildren<Button>(); //���� ���� ��ư ������Ʈ���� ������
        updateUi(); //ui ����
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void inven_SlotSelect(int i) //�κ��丮���� ������ Ŭ��
    {
        if (select_slot_index == i) //������ ������ ���� �ε����� ���� ��
        {
            SoundManager.instance.SFXPlay("Equip_", Equip_clip);  //ȿ���� ����
            select_item();  //���� ��� ����
        }
        else //���� �ε����� �ٸ� �ε��� Ŭ��
        {
            equip_case.SetActive(false); //�κ��丮�� Ŭ���߱� ������ ���� ���� ������ ��
            select_case.SetActive(true); //�κ��丮 ���� Ȱ��ȭ

            select_slot_index = i; //���� ���� �ε����� ������ �ε���
            select_case.transform.position = inven_slots[i].transform.position; //���� ������Ʈ�� ������ ������ �����ǰ� �����ϰ� ��

            if (inven_slots[i].GetComponentInChildren<itemStatus>() != null)  //������ ������ �� ������ �ƴϸ�
            {
                itemStatus itemstat = inven_slots[i].GetComponentInChildren<itemStatus>(); //���� ������Ʈ�� itemstatus �ڵ带 ������
                itemstat.Using(Itemimg,ItemNameText, ItemExplanationText, ItemStatText); //using �Լ��� �̹���,�ؽ�Ʈ ����
            }
            else  //�� �����̸�
            {
                TextReset();  //����� ����
            }
            
        }
        
    }

    public void Equip_SlotSelect(int i) //�������Կ��� ������ Ŭ��
    {
        if (equip_slot_index == i) //������ ������ ���� �ε����� ���� ��
        {
            SoundManager.instance.SFXPlay("Equip_", Equip_clip); //ȿ���� ����
            select_equip_slot(); //���� ��� ����
        }
        else //���� �ε����� �ٸ� �ε��� Ŭ��
        {
            select_case.SetActive(false); //���� ������ Ŭ���߱� ������ �κ��丮 ������ ��
            equip_case.SetActive(true); //���� ���� ���� Ȱ��ȭ

            equip_slot_index = i; //���� ���� �ε����� ������ �ε���
            equip_case.transform.position = equip_slots[i].transform.position; //���� ������Ʈ�� ������ ������ �����ǰ� �����ϰ� ��

            if (equip_slots[i].GetComponentInChildren<itemStatus>() != null) //������ ������ �� ������ �ƴϸ�
            {
                itemStatus itemstat = equip_slots[i].GetComponentInChildren<itemStatus>(); //���� ������Ʈ�� itemstatus �ڵ带 ������
                itemstat.Using(Itemimg, ItemNameText, ItemExplanationText, ItemStatText); //using �Լ��� �̹���,�ؽ�Ʈ ����
            }
            else //�� �����̸�
            {
                TextReset(); //����� ����
            }
        }
        

    }

    //���� ���� ���� ��� �������� ��ó �ڵ尡 ���� ����

    void select_item() //����
    {
        select_case.SetActive(false);  // ���� ��Ȱ��ȭ
        equip_case.SetActive(false); // ���� ��Ȱ��ȭ
        if (inven_slots[select_slot_index].GetComponentInChildren<itemStatus>() != null) //������ ������ �� ������ �ƴϸ�
        {
            itemStatus item = inven_slots[select_slot_index].GetComponentInChildren<itemStatus>(); //�� ���Կ� �ִ� ������ ���� �ڵ带 ������
            for (int i = 0; i < equip_slots.Length; i++) //���� ���� �ݺ����� ���鼭
            {
                if (equip_slots[i].GetComponentInChildren<itemStatus>() == null) //���� ù ��° �� ������ ã����
                {
                    Instantiate(item, equip_slots[i].transform); //�� ���� ���Կ� �������� ����
                    Destroy(item.transform.gameObject); //�κ��丮�� �ִ� �������� �ı�
                    equip_slots[i].GetComponentInChildren<Image>().color = Color.green; //���� ���� �� ȿ��
                    break; //�ݺ��� ����
                }
            }
        }
        updateUi(); //ui ��� ����
        select_slot_index = -1; //�ε��� �ʱ�ȭ
    }

    void select_equip_slot() //��ü
    {
        select_case.SetActive(false);  // ���� ��Ȱ��ȭ
        equip_case.SetActive(false); // ���� ��Ȱ��ȭ
        if (equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>() != null) //������ ������ �� ������ �ƴϸ�
        {
            itemStatus item = equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>(); //�� ���Կ� �ִ� ������ ���� �ڵ带 ������
            for (int i = 0; i < inven_slots.Length; i++) //�κ��丮 ���� �ݺ����� ���鼭
            {
                if (inven_slots[i].GetComponentInChildren<itemStatus>() == null) //���� ù ��° �� ������ ã����
                {
                    Instantiate(item, inven_slots[i].transform); //�� �κ��丮 �������� ������ ����
                    Destroy(item.transform.gameObject); //���� ���Կ� �ִ� �������� �ı�
                    equip_slots[equip_slot_index].GetComponentInChildren<Image>().color = Color.white; //���� ���� �� �������
                    break; //�ݺ��� ����
                }
            }
        }
        updateUi(); //ui ��� ����
        equip_slot_index = -1; //�ε��� �ʱ�ȭ
    }

    void updateUi() // ���� ���� (���� ���� -> ������Ʈ�� �����ϰ� �ٽ� �����Ǹ鼭 �Ҵ��� Ǯ��)
    {
        updateInven(); //�κ��丮 ui ����
        updateEquip(); //���� ���� ui ����
    }

    void updateInven() //�κ��丮 ���� ����
    {
        itemStatus[] append = new itemStatus[inven_slots.Length]; //���ο� �迭 ���� ���̴� �κ��丮 ���Ա��̸�ŭ 12
        for (int i = 0; i < inven_slots.Length; i++) //�ݺ����� ���鼭
        {
            append[i] = inven_slots[i].GetComponentInChildren<itemStatus>(); // ���� ������Ʈ�� itemStatus�� ������
            if (append[i] != null)
            {
                append[i].InitSetting(); //InitSetting ������ ������ ����
            }

        }
        itemStatus_list = append; //���� ����Ʈ ����
    }

    void updateEquip() //���� ���� ����
    {
        itemStatus[] append = new itemStatus[equip_slots.Length]; //���ο� �迭 ���� ���̴� ���� ���� ���̸�ŭ 6
        for (int i = 0; i < equip_slots.Length; i++) //�ݺ����� ���鼭
        {
            append[i] = equip_slots[i].GetComponentInChildren<itemStatus>(); // ���� ������Ʈ�� itemStatus�� ������
            if (append[i] != null)
            {
                append[i].InitSetting(); //InitSetting ������ ������ ����
            }

        }
        itemStatus_list = append; //���� ����Ʈ ����
    }

    void TextReset() //�� ������ Ŭ�������� �̹���, �ؽ�Ʈ �ʱ�ȭ�ϴ� �Լ�
    {
        Itemimg.sprite = null;
        Itemimg.color = new Color32(255, 255, 255, 0);
        ItemNameText.text = null;
        ItemExplanationText.text = null;
        ItemStatText.text = null;
    }
}
