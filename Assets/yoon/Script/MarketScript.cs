using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

[System.Serializable]
public class UnlockItem
{
    public bool Unlock;
}

[System.Serializable]
public class UnlockItemList
{
    public Dictionary<string, UnlockItem> Items;
}

[System.Serializable]
public class Unlock
{
    public string ItemName;
    public bool isUnlock;
}

[System.Serializable]
public class UnlockList
{
    public List<Unlock> items;
}

public class MarketScript : MonoBehaviour
{
    public static MarketScript instance; //2023-08-15 추가

    public GameObject marketList;
    public GameObject marketItem;
    public GameObject marketUi;
    public GameObject KeyUi;
    private bool MarketOpen = false;
    public bool PlayerVisit = false;    // 2023-08-15 private -> public 변경
    public List<int> RandomList;

    public UnlockList ItemFromJson;

    public AudioClip SellSound;
    public AudioClip BuySound;
    public AudioSource sfx;

    void Awake()
    {
        instance = this; //2023-08-15 추가
        MarketUi ui = MarketUi.instance;
        marketUi = ui.gameObject;
        marketList = ui.item_list;
    }
    // Start is called before the first frame update
    void Start()
    {
        string path = Application.dataPath + "/Resources";

        //FromJson 부분
        string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
        ItemFromJson = JsonUtility.FromJson<UnlockList>(fromJsonData);

        FindRemainItem();
        FillItemList(5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && PlayerVisit == true)
        {
            if (MarketOpen == true)
            {
                marketUi.SetActive(false);
                MarketOpen = false;
                KeyUi.SetActive(true);
                Ui_Controller ui = GameManager.Instance.GetComponent<Ui_Controller>();
                ui.inven_ui.SetActive(false);
                ui.openMarket = false;
                Destroy(ui.DescriptionBox);
                Time.timeScale = 1f;
            }
            else
            {
                marketUi.SetActive(true);
                MarketOpen = true;
                KeyUi.SetActive(false);
                Ui_Controller ui = GameManager.Instance.GetComponent<Ui_Controller>();
                ui.inven_ui.SetActive(true);
                ui.openMarket = true;
                Time.timeScale = 0f;
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            PlayerVisit = true;
            KeyUi.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Sliding"))
        {
            PlayerVisit = false;
            marketUi.SetActive(false);
            KeyUi.SetActive(false);
            MarketOpen = false;
        }
    }



    void FindRemainItem() //언락 안된 아이템만 불러옴
    {
        LoadPotion();
        for (int i = 0; i < ItemFromJson.items.Count; i++)
        {
            if (ItemFromJson.items[i].isUnlock == false)
            {
                RandomList.Add(i);
            }
        }
    }

    void FillItemList(int number) //상점 리스트 채움
    {
        for (int i = 0; i < number; i++)
        {
            int randomNumber = Random.Range(0, RandomList.Count);
            if (RandomList.Count == 0) //남아 있는 아이템 없으면 종료
            {
                break;
            }
            LoadPrefab(RandomList[randomNumber]);
        }
    }
    
    void LoadPrefab(int randomNumber) //프리팹 불러옴
    {
        if (ItemFromJson.items[randomNumber].isUnlock == false)
        {
            //상점 품목
            GameObject list = Instantiate(marketItem) as GameObject;
            list.transform.SetParent(marketList.transform, false);
            list.GetComponent<MarketItem>().market = this;
            //아이템
            GameObject randomItem = Resources.Load<GameObject>("item/" + ItemFromJson.items[randomNumber].ItemName);

            Image img = randomItem.GetComponent<Image>();
            img.SetNativeSize();
            RectTransform rectTransform = randomItem.GetComponent<RectTransform>();
            // 이미지의 너비와 높이 값을 가져옵니다.
            float width = rectTransform.sizeDelta.x;
            float height = rectTransform.sizeDelta.y;
            rectTransform.sizeDelta = new Vector2(width * 3f, height * 3f);

            Instantiate(randomItem, list.GetComponent<MarketItem>().icon.transform);
            RandomList.Remove(randomNumber);
        }
    }

    void LoadPotion()
    {
        //상점 품목
        GameObject list = Instantiate(marketItem) as GameObject;
        list.transform.SetParent(marketList.transform, false);
        list.GetComponent<MarketItem>().market = this;

        //아이템
        GameObject randomItem = Resources.Load<GameObject>("item/HpPotion");
        Instantiate(randomItem, list.GetComponent<MarketItem>().icon.transform);
    }

    public void BuySoundPlay()
    {
        sfx.clip = BuySound;
        sfx.Play();
    }

    public void SellSoundPlay()
    {
        sfx.clip = SellSound;
        sfx.Play();
    }
}
