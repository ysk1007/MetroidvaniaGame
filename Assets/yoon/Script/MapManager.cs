using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance; //추가함

    public int[] CurrentStage;
    public GameObject CurrentStagePrefab;
    public GameObject[,] Stage_Prefabs = new GameObject[3,8];
    public GameObject[] Stage1_Prefab;
    public GameObject[] Stage2_Prefab;
    public GameObject[] Stage3_Prefab;
    public List<GameObject[]> mapList = new List<GameObject[]>();
    public DataManager dm;
    public GameObject Loading_Screen;
    public bool pause = false;
    public SoundManager sm;

    public AudioClip Stage1_BossBGM;

    public bool StageMove = false;

    private void Awake()
    {
        instance = this; //추가함
    }

    // Start is called before the first frame update
    void Start()
    {
        dm = DataManager.instance;
        sm = SoundManager.instance;
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
        Invoke("Stage", 3f);
        Invoke("BossStage", 3f);
        Invoke("MarketStage", 3f);
        Invoke("SoundUp", 3f);
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
        pause = true;
        StageMove = false;
        Loading_Screen.GetComponent<Loading>().Load();
        if (EnemyAudioSource.instance != null)
        {
            EnemyAudioSource.instance.SoundOff();
        }
        Invoke("PrefabLoad",2.4f);
        Invoke("SoundUp", 3f);
    }

    void PrefabLoad()
    {
        Destroy(CurrentStagePrefab);
        if (CurrentStage[1] == 7)
        {
            CurrentStage[0]++;
            CurrentStage[1] = 0;
        }
        else
        {
            CurrentStage[1]++;
        }
        CurrentStage = dm.CurrentStage;
        Stage();
        BossStage();
        MarketStage();
        CurrentStagePrefab = Instantiate(Stage_Prefabs[CurrentStage[0], CurrentStage[1]], transform.parent);
        dm.NextStage();
        Player.instance.transform.position = new Vector3(0, 0, 0);
        pause = false;
    }

    void MarketStage()
    {
        if (CurrentStage[1] == 3 || CurrentStage[1] == 6)
        {
            sm.MarketStage();
        }
    }

    void BossStage() //보스 스테이지 인지 확인 함
    {
        if (CurrentStage[1] == 7)
        {
            int stage;
            if (CurrentStage[0] == 0)
            {
                stage = 1;
            }
            else if (CurrentStage[0] == 1)
            {
                stage = 2;
            }
            else
            {
                stage = 3;
            }
            sm.BossStage(stage);
        }
        else
        {
            return;
        }
    }

    void Stage()
    {
        int stage;
        if (CurrentStage[0] == 0)
        {
            stage = 1;
        }
        else if (CurrentStage[0] == 1)
        {
            stage = 2;
        }
        else
        {
            stage = 3;
        }
        sm.Stage(stage);
    }

    void SoundUp()
    {
        sm.SoundUp();
        if (EnemyAudioSource.instance != null)
        {
            EnemyAudioSource.instance.SoundOn();
        }
    }
}
