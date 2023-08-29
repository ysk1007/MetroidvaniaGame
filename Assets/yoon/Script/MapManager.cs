using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance; //추가함

    public int[] CurrentStage;
    public GameObject CurrentStagePrefab;
    public GameObject[,] Stage_Prefabs = new GameObject[3,6];
    public GameObject[] Stage1_Prefab;
    public GameObject[] Stage2_Prefab;
    public GameObject[] Stage3_Prefab;
    public List<GameObject[]> mapList = new List<GameObject[]>();
    public DataManager dm;
    public GameObject Loading_Screen;

    public bool StageMove = false;

    private void Awake()
    {
        instance = this; //추가함
    }

    // Start is called before the first frame update
    void Start()
    {
        dm = DataManager.instance;
        mapList.Add(Stage1_Prefab);
        mapList.Add(Stage2_Prefab);
        mapList.Add(Stage3_Prefab);
        for (int j = 0; j < mapList.Count; j++)
        {
            GameObject[] go = mapList[j];
            for (int i = 0; i < go.Length; i++)
            {
                Stage_Prefabs[j,i] = go[i];
            }
        }
        CurrentStage = dm.CurrentStage;
        CurrentStagePrefab = Instantiate(Stage_Prefabs[CurrentStage[0], CurrentStage[1]], transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (StageMove)
        {
            nextStage();
        }
    }

    void nextStage()
    {
        StageMove = false;
        Time.timeScale = 0f;
        Loading_Screen.GetComponent<Loading>().Load();
        Invoke("PrefabLoad",1.5f);

        Time.timeScale = 1f;
    }

    void PrefabLoad()
    {
        Destroy(CurrentStagePrefab);
        if (CurrentStage[1] == 5)
        {
            CurrentStage[0]++;
            CurrentStage[1] = 0;
        }
        else
        {
            CurrentStage[1]++;
        }
        CurrentStage = dm.CurrentStage;
        CurrentStagePrefab = Instantiate(Stage_Prefabs[CurrentStage[0], CurrentStage[1]], transform.parent);
        dm.NextStage();
        Player.instance.transform.position = new Vector3(0, 0, 0);
    }
}
