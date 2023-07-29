using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Save
{
    public List<string> testDataA = new List<string>();
    public List<int> testDataB = new List<int>();

    public int gold;
    public int power;
}

public class TestData : MonoBehaviour
{
    string path;

    void Start()
    {
        path = Path.Combine(Application.dataPath, "database2.json");
        JsonLoad();
    }

    public void JsonLoad()
    {
        Save saveData = new Save();

        if (!File.Exists(path))
        {
            JsonSave();
        }
        else
        {
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<Save>(loadJson);

            if (saveData != null)
            {
                for (int i = 0; i < saveData.testDataA.Count; i++)
                {
                    /*GameManager.instance.testDataA.Add(saveData.testDataA[i]);*/
                }
                for (int i = 0; i < saveData.testDataB.Count; i++)
                {
                    /*GameManager.instance.testDataB.Add(saveData.testDataB[i]);*/
                }
                /*GameManager.instance.playerGold = saveData.gold;
                GameManager.instance.playerPower = saveData.power;*/
            }
        }
    }

    public void JsonSave()
    {
        Save saveData = new Save();

        for (int i = 0; i < 10; i++)
        {
            saveData.testDataA.Add("테스트 데이터 no " + i);
        }

        for (int i = 0; i < 10; i++)
        {
            saveData.testDataB.Add(i);
        }

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}