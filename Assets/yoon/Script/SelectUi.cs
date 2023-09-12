using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SelectUi : MonoBehaviour
{
    public GameObject SelectPanel;
    public GameObject SelectPrefab;
    public List<GameObject> SelectList;
    public List<bool> List_Do_Select;
    public bool DoSelect = false;
    Vector3 BigScale = new Vector3(1.1f, 1.1f, 1.1f);
    Vector3 SmallScale = new Vector3(0.9f, 0.9f, 0.9f);
    public List<int> RandomList;

    public SelectList SelectFromJson;
    private Ui_Controller ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GameManager.Instance.GetComponent<Ui_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && DoSelect && ui.openSelect)
        {

            for (int i = 0; i < List_Do_Select.Count; i++)
            {
                if (List_Do_Select[i] == true)
                {
                    DoSelect = false;
                    SelectPrefab Prefab = SelectList[i].GetComponent<SelectPrefab>();
                    DataManager.instance.GetComponent<DataManager>().SelectListUpdate(Prefab.Name);
                    ui.Select_ui.SetActive(false);
                    ui.Select_error_text.SetActive(false);
                    DestroyList();
                    ui.UiUpdate();
                    Time.timeScale = 1f;
                    ui.openSelect = false;
                    break;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.E) && !DoSelect && ui.openSelect)
        {
            ui.Select_error_text.SetActive(true);
        }
    }

    public void OpenSelectUi()
    {
        ui = GameManager.Instance.GetComponent<Ui_Controller>();
        string path = Application.dataPath + "/Resources";

        //FromJson 부분
        string fromJsonData = File.ReadAllText(path + "/UnlockSelectList.txt");
        SelectFromJson = JsonUtility.FromJson<SelectList>(fromJsonData);

        FindRemainSelect();
        FillList(4);
    }

    void FindRemainSelect() //MAX(4) 레벨이 아닌 선택지만 불러옴
    {
        for (int i = 0; i < SelectFromJson.Selects.Length; i++)
        {
            if (SelectFromJson.Selects[i].Level != 4)
            {
                RandomList.Add(i);
            }
        }
    }

    void FillList(int count)
    {
        for (int i = 0; i < count; i++)
        {
            int randomNumber = Random.Range(0, RandomList.Count);
            Debug.Log(randomNumber);
            if (RandomList.Count == 0) //남아 있는 선택지 없으면 종료
            {
                break;
            }
            CreatePrefab(i, RandomList[randomNumber]);
        }
    }

    void CreatePrefab(int index, int randomNumber)
    {
        //능력치 프리팹 생성
        GameObject list = Instantiate(SelectPrefab) as GameObject;
        list.transform.SetParent(SelectPanel.transform, false);
        SelectPrefab Prefab = list.GetComponent<SelectPrefab>();
        Prefab.ThisIndex = index;
        Prefab.Name = SelectFromJson.Selects[randomNumber].SelectName;
        Prefab.LevelText.text = SelectFromJson.Selects[randomNumber].Level.ToString();
        switch (SelectFromJson.Selects[randomNumber].Level)
        {
            case 1:
                Prefab.Lv1Value.color = Color.green;
                break;
            case 2:
                Prefab.Lv2Value.color = Color.green;
                break;
            case 3:
                Prefab.LevelText.text = "MAX";
                Prefab.Lv3Value.color = Color.green;
                break;
        }
        Prefab.Setting();
        SelectList.Add(list);
        List_Do_Select.Add(false);
        RandomList.Remove(randomNumber);
    }

    public void selectButton(int index)
    {
        for (int i = 0; i < List_Do_Select.Count; i++)
        {
            if (i == index)
            {
                List_Do_Select[i] = true;
                DoSelect = true;
                SelectList[i].transform.localScale = BigScale;
                SelectList[i].GetComponent<Image>().color = Color.yellow;
            }
            else
            {
                List_Do_Select[i] = false;
                SelectList[i].transform.localScale = SmallScale;
                SelectList[i].GetComponent<Image>().color = Color.white;
            }
                
        }
    }

    void DestroyList()
    {
        int count = SelectList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject go = SelectList[i].gameObject;
            Destroy(go);
        }
        List<GameObject> NewSelectList = new List<GameObject>();
        List<bool> NewDoList = new List<bool>();
        List<int> NewRandomList = new List<int>();
        SelectList = NewSelectList;
        List_Do_Select = NewDoList;
        RandomList = NewRandomList;
    }
}
