using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class SaveData
{
    // �÷��̾� ������
    public int PlayerLevel = 1;
    public float PlayerGold = 0f;
    public float PlayerExp = 0f;
    public float PlayerCurrentHp = 100f;
    public Vector3 PlayerPos = new Vector3(-29.83f, -7.46f, 0);

    // ���õ� ������
    public int proWeaponSellect = 0; //���õ��� ������ ����
    public int proLevel = 0; //���õ� ����
    public float proFill = 0f; //���õ� ���� ��Ȳ

    // ���� ���� ������
    public float MasterVolume = 1f;
    public float BGMVolume = 1f;
    public float SFXVolume = 1f;

    public float PlayTime = 0f;
    public List<float> getVolume()
    {
        List<float> Volumes = new List<float>();
        Volumes.Add(MasterVolume);
        Volumes.Add(BGMVolume);
        Volumes.Add(SFXVolume);
        return Volumes;
    }
}

[System.Serializable]
public class Item
{
    public string[] itemEquip = new string[6];
    public string[] itemInven = new string[12];
}

[System.Serializable]
public class SelectLevel
{
    public string SelectName;
    public int Level;

    public SelectLevel(string selectName, int level)
    {
        SelectName = selectName;
        Level = level;
    }
}

[System.Serializable]
public class SelectList
{
    public SelectLevel[] Selects;
}

[System.Serializable]
public class DMUlcokItem
{
    public string ItemName;
    public bool isUnlock;

    public DMUlcokItem(string itemName, bool unlock)
    {
        ItemName = itemName;
        isUnlock = unlock;
    }
}

[System.Serializable]
public class ItemList
{
    public List<DMUlcokItem> items;
}

public class DataManager : MonoBehaviour
{
    public string PlayerPath;
    public string ItemPath;
    public string SelectPath;
    public string ItemUlockPath;
    public Proficiency_ui proData;
    public SoundManager soundData;
    public SoundSlider sliderData;
    public Player playerData;
    public Item ItemData = new Item();
    public SelectList SelectData = new SelectList();
    public UnlockList UnlockList;
    public SelectList SelectList;
    public SelectList selectData;

    public string PlayerloadJson;
    public string ItemloadJson;
    public string SelectloadJson;

    public bool PlayerDataLoadComplete;
    public bool SoundDataLoadComplete;
    public bool ItemDataLoadComplete;

    public static DataManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        PlayerPath = Path.Combine(Application.dataPath + "/Resources", "PlayerData.json");
        ItemPath = Path.Combine(Application.dataPath + "/Resources", "ItemData.json");
        SelectPath = Path.Combine(Application.dataPath + "/Resources", "UnlockSelectList.txt");
        ItemUlockPath = Path.Combine(Application.dataPath + "/Resources", "UnlockItemList.txt");
    }

    void Start()
    {
        PlayerPath = Path.Combine(Application.dataPath+ "/Resources", "PlayerData.json");
        ItemPath = Path.Combine(Application.dataPath + "/Resources", "ItemData.json");
        SelectPath = Path.Combine(Application.dataPath + "/Resources", "UnlockSelectList.txt");
        ItemUlockPath = Path.Combine(Application.dataPath + "/Resources", "UnlockItemList.txt");
        JsonLoad("Default");
        JsonLoad("ItemData");
    }
    public void JsonLoad(string casedata)
    {
        SaveData saveData = new SaveData();
        if (!File.Exists(PlayerPath)) //�ʱⰪ ����
        {
            Debug.Log("����� : ����� ������ ����");
            CreateJson();
        }
        else 
        {
            Debug.Log("����� : ����� ������ �ҷ����� ��");
            PlayerloadJson = File.ReadAllText(PlayerPath);
            ItemloadJson = File.ReadAllText(ItemPath);
            SelectloadJson = File.ReadAllText(SelectPath);

            saveData = JsonUtility.FromJson<SaveData>(PlayerloadJson);
            ItemData = JsonUtility.FromJson<Item>(ItemloadJson);
            SelectData = JsonUtility.FromJson<SelectList>(SelectloadJson);

            if (saveData != null)
            {
                switch (casedata)
                {
                    case "PlayerData":
                        if (Player.instance != null)
                        {
                            Debug.Log("����� : �÷��̾� ������ �ҷ����� ��");
                            Player.instance.level = saveData.PlayerLevel;
                            Player.instance.CurrentHp = saveData.PlayerCurrentHp;
                            Player.instance.gold = saveData.PlayerGold;
                            GameManager.Instance.GetComponent<Ui_Controller>().ExpBar.value = saveData.PlayerExp;
                            Player.instance.transform.position = saveData.PlayerPos;
                            Player.instance.proSelectWeapon = saveData.proWeaponSellect;
                            Player.instance.proLevel = saveData.proLevel;
                            OptionManager.instance.TotalPlayTime = saveData.PlayTime;
                            Proficiency_ui.instance.proWeaponIndex = saveData.proWeaponSellect;
                            Proficiency_ui.instance.proLevel = saveData.proLevel;
                            Proficiency_ui.instance.Profill.fillAmount = saveData.proFill;
                            Debug.Log("����� : �÷��̾� ������ �ε� �Ϸ�");
                            Debug.Log("����� : ������ ������ �ҷ����� ��");
                            for (int i = 0; i < SelectData.Selects.Length; i++)
                            {
                                switch (SelectData.Selects[i].SelectName)
                                {
                                    case "selectAtkLevel":
                                        Player.instance.selectAtkLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectATSLevel":
                                        Player.instance.selectATSLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectCCLevel":
                                        Player.instance.selectCCLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectLifeStillLevel":
                                        Player.instance.selectLifeStillLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectDefLevel":
                                        Player.instance.selectDefLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectHpLevel":
                                        Player.instance.selectHpLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectGoldLevel":
                                        Player.instance.selectGoldLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectExpLevel":
                                        Player.instance.selectExpLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                    case "selectCoolTimeLevel":
                                        Player.instance.selectCoolTimeLevel = SelectData.Selects[i].Level - 1;
                                        break;
                                }

                            }
                            Debug.Log("����� : ������ ������ �ε� �Ϸ�");
                            Player.instance.GetComponent<Player>().GetSelectValue("Start"); //���۽� ������ �ɷ�ġ ����
                        }
                        break;
                    case "SliderData":
                        if (SoundSlider.instance != null)
                        {
                            Debug.Log("����� : ���� ������ �ҷ����� ��");
                            SoundSlider.instance.master_slider.value = saveData.MasterVolume;
                            SoundSlider.instance.bgm_slider.value = saveData.BGMVolume;
                            SoundSlider.instance.sfx_slider.value = saveData.SFXVolume;
                            Debug.Log("����� : ���� ������ �ε� �Ϸ�");
                        }
                        break;
                    case "ItemData":
                        if (GameManager.Instance != null)
                        {
                            Debug.Log("����� : ������ ������ �ҷ����� ��");
                            for (int i = 0; i < ItemData.itemEquip.Length; i++)
                            {
                                if (ItemData.itemEquip[i] != "")
                                {
                                    GameObject prefab = Resources.Load<GameObject>("item/" + ItemData.itemEquip[i]);
                                    GameObject temp = Instantiate(prefab, GameManager.Instance.GetComponent<inven>().equip_slots[i].transform);
                                    temp.transform.SetParent(GameManager.Instance.GetComponent<inven>().equip_slots[i].transform);
                                    GameManager.Instance.GetComponent<inven>().equip_slots[i].GetComponentInChildren<Image>().color = Color.green;
                                }
                            }

                            for (int i = 0; i < ItemData.itemInven.Length; i++)
                            {
                                if (ItemData.itemInven[i] != "")
                                {
                                    GameObject prefab = Resources.Load<GameObject>("item/" + ItemData.itemInven[i]);
                                    GameObject temp = Instantiate(prefab, GameManager.Instance.GetComponent<inven>().inven_slots[i].transform);
                                    temp.transform.SetParent(GameManager.Instance.GetComponent<inven>().inven_slots[i].transform);
                                }
                            }
                            Debug.Log("����� : ������ ������ �ε� �Ϸ�");
                        }
                        break;
                    case "ProData":
                        if(Proficiency_ui.instance != null)
                        {
                            Debug.Log("���� ������ �ҷ���");
                            Proficiency_ui.instance.proWeaponIndex = saveData.proWeaponSellect;
                            Proficiency_ui.instance.proLevel = saveData.proLevel;
                            Proficiency_ui.instance.Profill.fillAmount = saveData.proFill;
                        }
                        break;

                }
            }
            Debug.Log("����� : ����� ������ ���������� �ҷ���");
        }
    }

    public void JsonSave(string casedata)
    {
        Debug.Log("����� : ������ ���� �ϴ���..");
        PlayerloadJson = File.ReadAllText(PlayerPath);
        SaveData jsonsave = JsonUtility.FromJson<SaveData>(PlayerloadJson);
        switch (casedata)
        {
            case "PlayerData":
                if (Player.instance != null)
                {
                    Debug.Log("����� : �÷��̾� ������ ���� ��");
                    jsonsave.PlayerLevel = Player.instance.level;
                    jsonsave.PlayerCurrentHp = Player.instance.CurrentHp;
                    jsonsave.PlayerGold = Player.instance.gold;
                    jsonsave.PlayerExp = GameManager.Instance.GetComponent<Ui_Controller>().ExpBar.value;
                    jsonsave.PlayerPos = Player.instance.transform.position;
                    jsonsave.proWeaponSellect = Proficiency_ui.instance.proWeaponIndex;
                    jsonsave.proLevel = Proficiency_ui.instance.proLevel;
                    jsonsave.proFill = Proficiency_ui.instance.Profill.fillAmount;
                    jsonsave.PlayTime = OptionManager.instance.TotalPlayTime;
                }
                Debug.Log("����� : �÷��̾� ������ ���� �Ϸ�");
                break;
            case "SliderData":
                Debug.Log("����� : ���� ������ ���� ��");
                jsonsave.MasterVolume = SoundSlider.instance.master_slider.value;
                jsonsave.BGMVolume = SoundSlider.instance.bgm_slider.value;
                jsonsave.SFXVolume = SoundSlider.instance.sfx_slider.value;
                Debug.Log("����� : ���� ������ ���� �Ϸ�");
                break;
            case "ItemData":
                if (GameManager.Instance != null)
                {
                    Debug.Log("����� : ������ ������ ���� ��");
                    itemStatus[] equip_list = GameManager.Instance.GetComponent<inven>().itemStatus_list_equip;
                    itemStatus[] inven_list = GameManager.Instance.GetComponent<inven>().itemStatus_list_inven;
                    for (int i = 0; i < equip_list.Length; i++)
                    {
                        if (equip_list[i] != null)
                        {
                            ItemData.itemEquip[i] = equip_list[i].data.itemNameEng;
                            Debug.Log(ItemData.itemEquip[i]);
                        }
                        else
                        {
                            ItemData.itemEquip[i] = null;
                        }
                    }
                    for (int i = 0; i < inven_list.Length; i++)
                    {
                        if (inven_list[i] != null)
                        {
                            ItemData.itemInven[i] = inven_list[i].data.itemNameEng;
                            Debug.Log(ItemData.itemInven[i]);
                        }
                        else
                        {
                            ItemData.itemInven[i] = null;
                        }
                    }
                    Debug.Log("����� : ������ ������ ���� �Ϸ�");
                }
                break;
            case "ProData":
                if (Proficiency_ui.instance != null)
                {
                    Debug.Log("���� ������ ����");
                    jsonsave.proWeaponSellect = Proficiency_ui.instance.proWeaponIndex;
                    jsonsave.proLevel = Proficiency_ui.instance.proLevel;
                    jsonsave.proFill = Proficiency_ui.instance.Profill.fillAmount;
                }
                break;
        }
        string Playerjson = JsonUtility.ToJson(jsonsave, true);
        string itemjson = JsonUtility.ToJson(ItemData, true);
        File.WriteAllText(PlayerPath, Playerjson);
        File.WriteAllText(ItemPath, itemjson);
        Debug.Log("����� : ��� �����͸� ���������� �����Ͽ����ϴ�");
    }

    public void CreateJson()
    {
        Debug.Log("����� : ������ ���� �ϴ���..");
        CreatePlayerJson();
        CreateItemJson();
        CreateSelectJson();
        CreateUnlockItemJson();
        Debug.Log("����� : �����͸� ���������� �����Ͽ����ϴ�");
    }

    public void CreatePlayerJson()
    {
        SaveData saveData = new SaveData();
        Debug.Log("����� : �÷��̾� ������ ���� ��");
        saveData.PlayerLevel = 1;
        saveData.PlayerGold = 0;
        saveData.PlayerExp = 0.0f;
        saveData.PlayerCurrentHp = 100.0f;
        saveData.PlayerPos = new Vector3(-29.83f, -7.55f, 0.0f);
        saveData.PlayTime = 0f;
        Debug.Log("����� : �÷��̾� ������ ���� �Ϸ�");
        Debug.Log("����� : ���� ������ ���� ��");
        saveData.MasterVolume = 1.0f;
        saveData.BGMVolume = 1.0f;
        saveData.SFXVolume = 1.0f;
        Debug.Log("����� : ���� ������ ���� �Ϸ�");
        Debug.Log("����� : ���õ� ������ ���� ��");
        saveData.proWeaponSellect = 4;
        saveData.proLevel = 0;
        saveData.proFill = 0.0f;
        Debug.Log("����� : ���õ� ������ ���� �Ϸ�");
        string Playerjson = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(PlayerPath, Playerjson);
    }

    public void CreateItemJson()
    {
        Debug.Log("����� : ������ ������ ���� ��");
        Item itemData = new Item();
        itemData.itemEquip = new string[6];
        itemData.itemInven = new string[12];
        Debug.Log("����� : ������ ������ ���� �Ϸ�");
        string itemjson = JsonUtility.ToJson(itemData, true);
        File.WriteAllText(ItemPath, itemjson);
    }
    public void CreateSelectJson()
    {
        Debug.Log("����� : ������ ���� ����Ʈ ���� ���� ��");

        // SelectLevel ������ ����
        List<SelectLevel> selectLevels = new List<SelectLevel>();
        selectLevels.Add(new SelectLevel("selectAtkLevel", 1));
        selectLevels.Add(new SelectLevel("selectATSLevel", 1));
        selectLevels.Add(new SelectLevel("selectCCLevel", 1));
        selectLevels.Add(new SelectLevel("selectLifeStillLevel", 1));
        selectLevels.Add(new SelectLevel("selectDefLevel", 1));
        selectLevels.Add(new SelectLevel("selectHpLevel", 1));
        selectLevels.Add(new SelectLevel("selectGoldLevel", 1));
        selectLevels.Add(new SelectLevel("selectExpLevel", 1));
        selectLevels.Add(new SelectLevel("selectCoolTimeLevel", 1));

        // �����͸� JSON ���Ϸ� ����
        SaveToJson(selectLevels);

        Debug.Log("����� : ������ ���� ����Ʈ ���� ���� �Ϸ�");
    }

    void SaveToJson(List<SelectLevel> data)
    {
        SelectList selectList = new SelectList();
        selectList.Selects = data.ToArray();

        string jsonData = JsonUtility.ToJson(selectList);
        File.WriteAllText(SelectPath, jsonData);
        Debug.Log("JSON ������ �����Ǿ����ϴ�.");
    }

    void CreateUnlockItemJson()
    {
        // Item ������ ����
        List<DMUlcokItem> items = new List<DMUlcokItem>();
        items.Add(new DMUlcokItem("SkyWalker", false));
        items.Add(new DMUlcokItem("Club", false));
        items.Add(new DMUlcokItem("JadeEmblem", false));
        items.Add(new DMUlcokItem("ClownCloth", false));
        items.Add(new DMUlcokItem("ClownHat", false));
        items.Add(new DMUlcokItem("ClownGloves", false));
        items.Add(new DMUlcokItem("ClownPants", false));
        items.Add(new DMUlcokItem("ClownBoots", false));
        items.Add(new DMUlcokItem("SymbolRich", false));
        items.Add(new DMUlcokItem("LightningGloves", false));
        items.Add(new DMUlcokItem("BattleBookBeginner", false));
        items.Add(new DMUlcokItem("StrangeCandy", false));
        items.Add(new DMUlcokItem("VampireCup", false));
        items.Add(new DMUlcokItem("Cookie", false));
        items.Add(new DMUlcokItem("PoisonMushroom", false));
        items.Add(new DMUlcokItem("RootOfTree", false));
        items.Add(new DMUlcokItem("NightofCountingtheStars", false));
        items.Add(new DMUlcokItem("GlassSword", false));
        items.Add(new DMUlcokItem("GridsSword", false));
        items.Add(new DMUlcokItem("PastThatWantToErase", false));
        items.Add(new DMUlcokItem("ReproductionOfMassacre", false));
        items.Add(new DMUlcokItem("DivinePower", false));
        items.Add(new DMUlcokItem("BrokenWatch", false));
        items.Add(new DMUlcokItem("EyeOfBeast", false));
        items.Add(new DMUlcokItem("AttackClaw", false));
        items.Add(new DMUlcokItem("BlackCard", false));
        items.Add(new DMUlcokItem("RedCard", false));
        items.Add(new DMUlcokItem("AssassinDagger", false));
        items.Add(new DMUlcokItem("DoubleEdgedAxe", false));
        items.Add(new DMUlcokItem("ElfBow", false));
        items.Add(new DMUlcokItem("EscapeRope", false));
        items.Add(new DMUlcokItem("BundleOfGifts", false));
        items.Add(new DMUlcokItem("LoveLetter", false));
        items.Add(new DMUlcokItem("MiniStar", false));
        items.Add(new DMUlcokItem("HeroMask", false));
        items.Add(new DMUlcokItem("RepressionShield", false));
        items.Add(new DMUlcokItem("TransmitterHammer", false));
        items.Add(new DMUlcokItem("SuspiciousMirror", false));

        // �����͸� JSON ���Ϸ� ����
        SaveToJson(items);
    }

    void SaveToJson(List<DMUlcokItem> data)
    {
        ItemList itemList = new ItemList();
        itemList.items = data;

        // Unity�� JsonUtility�� ����Ͽ� JSON ���� ����
        string jsonData = JsonUtility.ToJson(itemList, true);
        File.WriteAllText(ItemUlockPath, jsonData);
        Debug.Log("JSON ������ �����Ǿ����ϴ�.");
    }

    public List<float> getVolume()
    {
        SaveData saveData = new SaveData();
        string loadJson = File.ReadAllText(PlayerPath);
        saveData = JsonUtility.FromJson<SaveData>(loadJson);
        return saveData.getVolume();
    }

    public void UnlockListUpdate(int ItemNumber)
    {
        List<Unlock> newUnlockList = new List<Unlock>();

        string path = Application.dataPath + "/Resources";
        string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
        UnlockList = JsonUtility.FromJson<UnlockList>(fromJsonData);
        UnlockList.items[ItemNumber - 1].isUnlock = true;

        UnlockList Item = new UnlockList();
        Item.items = UnlockList.items;

        string jsonData = JsonUtility.ToJson(Item, true);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/UnlockItemList.txt", jsonData);
    }

    public void SelectListUpdate(string Name)
    {
        List<SelectLevel> newSelectList = new List<SelectLevel>();

        string path = Application.dataPath + "/Resources";
        string fromJsonData = File.ReadAllText(path + "/UnlockSelectList.txt");
        SelectList = JsonUtility.FromJson<SelectList>(fromJsonData);
        for (int i = 0; i < SelectList.Selects.Length; i++)
        {
            if (SelectList.Selects[i].SelectName == Name)
            {
                SelectList.Selects[i].Level++;
                switch (SelectList.Selects[i].SelectName)
                {
                    case "selectAtkLevel":
                        Player.instance.selectAtkLevel++;
                        Player.instance.GetSelectValue("selectAtkLevel");
                        break;
                    case "selectATSLevel":
                        Player.instance.selectATSLevel++;
                        Player.instance.GetSelectValue("selectATSLevel");
                        break;
                    case "selectCCLevel":
                        Player.instance.selectCCLevel++;
                        Player.instance.GetSelectValue("selectCCLevel");
                        break;
                    case "selectLifeStillLevel":
                        Player.instance.selectLifeStillLevel++;
                        Player.instance.GetSelectValue("selectLifeStillLevel");
                        break;
                    case "selectDefLevel":
                        Player.instance.selectDefLevel++;
                        Player.instance.GetSelectValue("selectDefLevel");
                        break;
                    case "selectHpLevel":
                        Player.instance.selectHpLevel++;
                        Player.instance.GetSelectValue("selectHpLevel");
                        GameManager.Instance.GetComponent<Ui_Controller>().UiUpdate();
                        break;
                    case "selectGoldLevel":
                        Player.instance.selectGoldLevel++;
                        Player.instance.GetSelectValue("selectGoldLevel");
                        break;
                    case "selectExpLevel":
                        Player.instance.selectExpLevel++;
                        Player.instance.GetSelectValue("selectExpLevel");
                        break;
                    case "selectCoolTimeLevel":
                        Player.instance.selectCoolTimeLevel++;
                        Player.instance.GetSelectValue("selectCoolTimeLevel");
                        break;
                }
                break;
            }
        }

        SelectList Select = new SelectList();
        Select.Selects = SelectList.Selects;

        string jsonData = JsonUtility.ToJson(Select, true);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        File.WriteAllText(path + "/UnlockSelectList.txt", jsonData);
    }

    public GameObject ChestItem()
    {
        UnlockList Json;

        string path = Application.dataPath + "/Resources";
        string fromJsonData = File.ReadAllText(path + "/UnlockItemList.txt");
        Json = JsonUtility.FromJson<UnlockList>(fromJsonData);

        List<int> ItemList = new List<int> { };
        for (int i = 0; i < Json.items.Count; i++)
        {
            if (Json.items[i].isUnlock == false)
            {
                ItemList.Add(i);
                Debug.Log(Json.items[i].ItemName);
            }
        }
        int randomNumber = Random.Range(0, ItemList.Count);
        if (ItemList.Count == 0) //���� �ִ� ������ ������ ����
        {
            GameObject Potion = Resources.Load<GameObject>("item/HpPotion");
            return Potion;
        }
        GameObject randomItem = Resources.Load<GameObject>("item/" + Json.items[ItemList[randomNumber]].ItemName);
        return randomItem;
    }

    public void DeleteJson()
    {
        string filePath = Path.Combine(Application.dataPath + "/Resources", "PlayerData.json");

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("JSON ������ ���������� �����Ǿ����ϴ�.");
        }
        else
        {
            Debug.Log("������ JSON ������ �������� �ʽ��ϴ�.");
        }
    }
}
