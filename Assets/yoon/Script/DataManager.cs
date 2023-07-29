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
    public int PlayerGold = 0;
    public float PlayerExp = 0f;
    public float PlayerMaxHp = 100f;
    public float PlayerCurrentHp = 100f;
    public Vector3 PlayerPos = new Vector3(-29.83f, -7.46f, 0);

    // ���õ� ������
    public int proWeaponSellect = 0; //���õ��� ������ ����
    public int proLevel = 0; //���õ� ����
    public float proFill = 0f; //���õ� ���� ��Ȳ

    // ������ ���� ������
    public int selectAtkLevel = 0;
    public int selectATSLevel = 0;
    public int selectCCLevel = 0;
    public int selectDefLevel = 0;
    public int selectHpLevel = 0;
    public int selectGoldLevel = 0;
    public int selectExpLevel = 0;
    public int selectCoolTimeLevel = 0;

    // ���� ���� ������
    public float MasterVolume = 1f;
    public float BGMVolume = 1f;
    public float SFXVolume = 1f;

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
public class UnlockSelect
{
    public bool Unlock;
}

[System.Serializable]
public class UnlockSelectList
{
    public Dictionary<string, int> Selects;
}

[System.Serializable]
public class SelectLevel
{
    public string SelectName;
    public int Level;
}

[System.Serializable]
public class SelectList
{
    public List<SelectLevel> Selects;
}

public class DataManager : MonoBehaviour
{
    public string PlayerPath;
    public string ItemPath;
    public string SelectPath;
    public Proficiency_ui proData;
    public SoundManager soundData;
    public SoundSlider sliderData;
    public Player playerData;
    public Item ItemData = new Item();
    public UnlockList UnlockList;
    public SelectList SelectList;
    public SelectList selectData;

    public string PlayerloadJson;
    public string ItemloadJson;

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
    }

    void Start()
    {
        PlayerPath = Path.Combine(Application.dataPath+ "/Resources", "PlayerData.json");
        ItemPath = Path.Combine(Application.dataPath + "/Resources", "ItemData.json");
        SelectPath = Path.Combine(Application.dataPath + "/Resources", "UnlockSelectList.txt");
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
            saveData = JsonUtility.FromJson<SaveData>(PlayerloadJson);
            ItemData = JsonUtility.FromJson<Item>(ItemloadJson);
            if (saveData != null)
            {
                switch (casedata)
                {
                    case "PlayerData":
                        if (Player.instance != null)
                        {
                            Debug.Log("����� : �÷��̾� ������ �ҷ����� ��");
                            Player.instance.level = saveData.PlayerLevel;
                            Player.instance.MaxHp = saveData.PlayerMaxHp;
                            Player.instance.CurrentHp = saveData.PlayerCurrentHp;
                            Player.instance.gold = saveData.PlayerGold;
                            GameManager.Instance.GetComponent<Ui_Controller>().ExpBar.value = saveData.PlayerExp;
                            Player.instance.transform.position = saveData.PlayerPos;
                            Debug.Log("����� : �÷��̾� ������ �ε� �Ϸ�");
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
                        break;
                    case "ProData":
                        if(Proficiency_ui.instance != null)
                        {
                            /*                                Proficiency_ui.instance.proWeaponIndex = saveData.proWeaponSellect;
                                                Proficiency_ui.instance.proLevel = saveData.proLevel;*/
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
                    jsonsave.PlayerMaxHp = Player.instance.MaxHp;
                    jsonsave.PlayerCurrentHp = Player.instance.CurrentHp;
                    jsonsave.PlayerGold = Player.instance.gold;
                    jsonsave.PlayerExp = GameManager.Instance.GetComponent<Ui_Controller>().ExpBar.value;
                    jsonsave.PlayerPos = Player.instance.transform.position;
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
                Debug.Log("����� : ���õ� ������ ���� ��");
                jsonsave.proWeaponSellect = Proficiency_ui.instance.proWeaponIndex;
                jsonsave.proLevel = Proficiency_ui.instance.proLevel;
                Debug.Log("����� : ���õ� ������ ���� �Ϸ�");
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
        Debug.Log("����� : �����͸� ���������� �����Ͽ����ϴ�");
    }

    public void CreatePlayerJson()
    {
        SaveData saveData = new SaveData();
        Debug.Log("����� : �÷��̾� ������ ���� ��");
        saveData.PlayerLevel = 1;
        saveData.PlayerGold = 0;
        saveData.PlayerExp = 0.0f;
        saveData.PlayerMaxHp = 100.0f;
        saveData.PlayerCurrentHp = 100.0f;
        saveData.PlayerPos = new Vector3(-29.83f, -7.55f, 0.0f);
        Debug.Log("����� : �÷��̾� ������ ���� �Ϸ�");
        Debug.Log("����� : ���� ������ ���� ��");
        saveData.MasterVolume = 1.0f;
        saveData.BGMVolume = 1.0f;
        saveData.SFXVolume = 1.0f;
        Debug.Log("����� : ���� ������ ���� �Ϸ�");
        Debug.Log("����� : ���õ� ������ ���� ��");
        saveData.proWeaponSellect = 0;
        saveData.proLevel = 0;
        saveData.proFill = 0.0f;
        Debug.Log("����� : ���õ� ������ ���� �Ϸ�");
        Debug.Log("����� : ������ ���� ���� ��");
        saveData.selectAtkLevel = 0;
        saveData.selectATSLevel = 0;
        saveData.selectCCLevel = 0;
        saveData.selectDefLevel = 0;
        saveData.selectHpLevel = 0;
        saveData.selectGoldLevel = 0;
        saveData.selectExpLevel = 0;
        saveData.selectCoolTimeLevel = 0;
        Debug.Log("����� : ������ ���� ���� �Ϸ�");
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

        // JSON ������ ����
        SelectList data = new SelectList();
        data.Selects = new List<SelectLevel>();

        data.Selects.Add(new SelectLevel { SelectName = "selectAtkLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectATSLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectCCLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectDefLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectHpLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectGoldLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectExpLevel", Level = 1 });
        data.Selects.Add(new SelectLevel { SelectName = "selectCoolTimeLevel", Level = 1 });

        // JSON ���Ϸ� ����
        /*SaveToJson(data);*/

        Debug.Log("����� : ������ ���� ����Ʈ ���� ���� �Ϸ�");
    }

    private void SaveToJson(Dictionary<string, int> data)
    {
        string jsonData = JsonUtility.ToJson(new JsonWrapper(data));
        File.WriteAllText(SelectPath, jsonData);
        Debug.Log("JSON ������ �����Ǿ����ϴ�.");
    }

    // Dictionary�� ����ȭ�ϱ� ���� ���� Ŭ����
    [System.Serializable]
    private class JsonWrapper
    {
        public Dictionary<string, int> data;

        public JsonWrapper(Dictionary<string, int> data)
        {
            this.data = data;
        }
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
        string fromJsonData = File.ReadAllText(path + "/UnlockSelectList.json");
        SelectList = JsonUtility.FromJson<SelectList>(fromJsonData);
        for (int i = 0; i < SelectList.Selects.Count; i++)
        {
            if (SelectList.Selects[i].SelectName == Name)
            {
                SelectList.Selects[i].Level++;
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
        File.WriteAllText(path + "/UnlockSelectList.json", jsonData);
    }



    public void DeleteJson()
    {
        string filePath = Path.Combine(Application.dataPath + "/Resources", "");

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
