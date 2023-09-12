using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance; //추가함

    public Map_ui map_ui;
    public int[] CurrentStage;
    public int Difficulty;
    public GameObject CurrentStagePrefab;
    public GameObject[,] Stage_Prefabs = new GameObject[3,9];
    public GameObject[] Stage1_Prefab;
    public GameObject[] Stage2_Prefab;
    public GameObject[] Stage3_Prefab;
    public List<GameObject[]> mapList = new List<GameObject[]>();
    public DataManager dm;
    public GameObject Loading_Screen;
    public bool pause = false;
    public SoundManager sm;

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
        Difficulty = dm.returnDifficulty();
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
        if (CurrentStage[0] == 0 && CurrentStage[1] == 7)
        {
            Player.instance.transform.position = new Vector3(25, 5, 0);
        }
        map_ui.Setting();
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

    public void nextStage()
    {
        if (CurrentStage[0] == 2 && CurrentStage[1] == 8)
        {
            GameManager.Instance.GetComponent<Ui_Controller>().StatisticsUi.SetActive(true);
            StatisticsUi st = GameManager.Instance.GetComponent<Ui_Controller>().StatisticsUi.GetComponent<StatisticsUi>();
            st.isFalling = true;
            st.GameClear = true;
            StageMove = false;
            pause = true;
        }
        else
        {
            pause = true;
            StageMove = false;
            Loading_Screen.GetComponent<Loading>().Load();
            if (EnemyAudioSource.instance != null)
            {
                EnemyAudioSource.instance.SoundOff();
            }
            Invoke("PrefabLoad", 2.4f);
            Invoke("SoundUp", 3f);
        }
    }

    void PrefabLoad()
    {
        Destroy(CurrentStagePrefab);
        if (CurrentStage[1] == 8)
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
        map_ui.Setting();
        if (CurrentStage[0] == 0 && CurrentStage[1] == 7)
        {
            Player.instance.transform.position = new Vector3(25, 5, 0);
        }
        else
        {
            Player.instance.transform.position = new Vector3(0, 0, 0);
        }
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

    public void BossDie()
    {
        Invoke("nextStage", 8f);
    }
}
