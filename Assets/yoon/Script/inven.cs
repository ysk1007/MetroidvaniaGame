using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class inven : MonoBehaviour
{
    public Button[] inven_slots = { }; //인벤토리 버튼들 담을 배열
    public GameObject slots_obj; //실제 인벤토리 버튼들의 상위 오브젝트
    public int select_slot_index = -1; //인벤토리 버튼들 중 선택된 인덱스
    public GameObject select_case; //선택 강조용 오브젝트

    public Button[] equip_slots = { }; //장착 슬롯 버튼들 담을 배열
    public GameObject equips_obj; //실제 장착 슬롯 버튼들의 상위 오브젝트
    public int equip_slot_index = -1; //장착 슬롯 버튼들 중 선택된 인덱스
    public GameObject equip_case; //선택 강조용 오브젝트

    public itemStatus itemStatus; //아이템 스탯 코드를 가져옴
    public itemStatus[] itemStatus_list; //현재 장착하고있는 아이템 리스트 배열

    public TextMeshProUGUI ItemNameText; //아이템 이름 텍스트
    public TextMeshProUGUI ItemExplanationText; //설명 텍스트
    public TextMeshProUGUI ItemStatText; //텍스트 텍스트
    public Image Itemimg; //아이템 이미지

    public AudioClip Equip_clip; //장착/해제 효과 사운드
    private void Awake()
    {
        inven_slots = slots_obj.GetComponentsInChildren<Button>(); //인벤토리 버튼 오브젝트들을 가져옴
        equip_slots = equips_obj.GetComponentsInChildren<Button>(); //장착 슬롯 버튼 오브젝트들을 가져옴
        updateUi(); //ui 갱신
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
        if (select_slot_index == i) //선택한 슬롯이 현재 인덱스랑 같을 때
        {
            SoundManager.instance.SFXPlay("Equip_", Equip_clip);  //효과음 실행
            select_item();  //장착 기능 실행
        }
        else //현재 인덱스와 다른 인덱스 클릭
        {
            equip_case.SetActive(false); //인베토리를 클릭했기 때문에 장착 슬롯 강조는 끔
            select_case.SetActive(true); //인벤토리 강조 활성화

            select_slot_index = i; //현재 슬롯 인덱스는 선택한 인덱스
            select_case.transform.position = inven_slots[i].transform.position; //강조 오브젝트가 선택한 슬롯의 포지션과 동일하게 함

            if (inven_slots[i].GetComponentInChildren<itemStatus>() != null)  //선택한 슬롯이 빈 슬롯이 아니면
            {
                itemStatus itemstat = inven_slots[i].GetComponentInChildren<itemStatus>(); //하위 오브젝트의 itemstatus 코드를 가져옴
                itemstat.Using(Itemimg,ItemNameText, ItemExplanationText, ItemStatText); //using 함수로 이미지,텍스트 세팅
            }
            else  //빈 슬롯이면
            {
                TextReset();  //설명부 리셋
            }
            
        }
        
    }

    public void Equip_SlotSelect(int i) //장착슬롯에서 아이템 클릭
    {
        if (equip_slot_index == i) //선택한 슬롯이 현재 인덱스랑 같을 때
        {
            SoundManager.instance.SFXPlay("Equip_", Equip_clip); //효과음 실행
            select_equip_slot(); //해제 기능 실행
        }
        else //현재 인덱스와 다른 인덱스 클릭
        {
            select_case.SetActive(false); //장착 슬롯을 클릭했기 때문에 인벤토리 강조는 끔
            equip_case.SetActive(true); //장착 슬롯 강조 활성화

            equip_slot_index = i; //현재 슬롯 인덱스는 선택한 인덱스
            equip_case.transform.position = equip_slots[i].transform.position; //강조 오브젝트가 선택한 슬롯의 포지션과 동일하게 함

            if (equip_slots[i].GetComponentInChildren<itemStatus>() != null) //선택한 슬롯이 빈 슬롯이 아니면
            {
                itemStatus itemstat = equip_slots[i].GetComponentInChildren<itemStatus>(); //하위 오브젝트의 itemstatus 코드를 가져옴
                itemstat.Using(Itemimg, ItemNameText, ItemExplanationText, ItemStatText); //using 함수로 이미지,텍스트 세팅
            }
            else //빈 슬롯이면
            {
                TextReset(); //설명부 리셋
            }
        }
        

    }

    //현재 장착 슬롯 모두 껴있을때 대처 코드가 아직 없음

    void select_item() //장착
    {
        select_case.SetActive(false);  // 강조 비활성화
        equip_case.SetActive(false); // 강조 비활성화
        if (inven_slots[select_slot_index].GetComponentInChildren<itemStatus>() != null) //선택한 슬롯이 빈 슬롯이 아니면
        {
            itemStatus item = inven_slots[select_slot_index].GetComponentInChildren<itemStatus>(); //그 슬롯에 있는 아이템 스탯 코드를 가져옴
            for (int i = 0; i < equip_slots.Length; i++) //장착 슬롯 반복문을 돌면서
            {
                if (equip_slots[i].GetComponentInChildren<itemStatus>() == null) //가장 첫 번째 빈 슬롯을 찾으면
                {
                    Instantiate(item, equip_slots[i].transform); //그 장착 슬롯에 아이템을 생성
                    Destroy(item.transform.gameObject); //인벤토리에 있는 아이템은 파괴
                    equip_slots[i].GetComponentInChildren<Image>().color = Color.green; //장착 슬롯 색 효과
                    break; //반복문 종료
                }
            }
        }
        updateUi(); //ui 모두 갱신
        select_slot_index = -1; //인덱스 초기화
    }

    void select_equip_slot() //해체
    {
        select_case.SetActive(false);  // 강조 비활성화
        equip_case.SetActive(false); // 강조 비활성화
        if (equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>() != null) //선택한 슬롯이 빈 슬롯이 아니면
        {
            itemStatus item = equip_slots[equip_slot_index].GetComponentInChildren<itemStatus>(); //그 슬롯에 있는 아이템 스탯 코드를 가져옴
            for (int i = 0; i < inven_slots.Length; i++) //인벤토리 슬롯 반복문을 돌면서
            {
                if (inven_slots[i].GetComponentInChildren<itemStatus>() == null) //가장 첫 번째 빈 슬롯을 찾으면
                {
                    Instantiate(item, inven_slots[i].transform); //그 인벤토리 슬롯으로 아이템 생성
                    Destroy(item.transform.gameObject); //장착 슬롯에 있던 아이템은 파괴
                    equip_slots[equip_slot_index].GetComponentInChildren<Image>().color = Color.white; //장착 슬롯 색 원래대로
                    break; //반복문 종료
                }
            }
        }
        updateUi(); //ui 모두 갱신
        equip_slot_index = -1; //인덱스 초기화
    }

    void updateUi() // 전부 갱신 (갱신 이유 -> 오브젝트를 삭제하고 다시 생성되면서 할당이 풀림)
    {
        updateInven(); //인벤토리 ui 갱신
        updateEquip(); //장착 슬롯 ui 갱신
    }

    void updateInven() //인벤토리 슬롯 갱신
    {
        itemStatus[] append = new itemStatus[inven_slots.Length]; //새로운 배열 생성 길이는 인벤토리 슬롯길이만큼 12
        for (int i = 0; i < inven_slots.Length; i++) //반복문을 돌면서
        {
            append[i] = inven_slots[i].GetComponentInChildren<itemStatus>(); // 하위 오브젝트에 itemStatus가 있으면
            if (append[i] != null)
            {
                append[i].InitSetting(); //InitSetting 아이템 데이터 세팅
            }

        }
        itemStatus_list = append; //장착 리스트 갱신
    }

    void updateEquip() //장착 슬롯 갱신
    {
        itemStatus[] append = new itemStatus[equip_slots.Length]; //새로운 배열 생성 길이는 장착 슬롯 길이만큼 6
        for (int i = 0; i < equip_slots.Length; i++) //반복문을 돌면서
        {
            append[i] = equip_slots[i].GetComponentInChildren<itemStatus>(); // 하위 오브젝트에 itemStatus가 있으면
            if (append[i] != null)
            {
                append[i].InitSetting(); //InitSetting 아이템 데이터 세팅
            }

        }
        itemStatus_list = append; //장착 리스트 갱신
    }

    void TextReset() //빈 슬롯을 클릭했을때 이미지, 텍스트 초기화하는 함수
    {
        Itemimg.sprite = null;
        Itemimg.color = new Color32(255, 255, 255, 0);
        ItemNameText.text = null;
        ItemExplanationText.text = null;
        ItemStatText.text = null;
    }
}
