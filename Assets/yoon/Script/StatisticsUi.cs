using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsUi : MonoBehaviour
{
    public DataManager dm;
    public MapManager mp;
    public Scene_Move sm;
    public Fade_img fade;
    public Player p;
    public OptionManager op;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI GetGoldText;
    public TextMeshProUGUI TotalDmageText;
    public TextMeshProUGUI PlayTimeText;
    public TextMeshProUGUI GetItemText;
    public GameObject List;

    public Animator anim;
    public float fallSpeed = 750f;
    public bool isFalling = false;
    public bool GameClear = false;
    public bool Ending = false;

    private RectTransform rectTransform;
    public Vector2 targetPosition;

    public bool die = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            return;
        }

        // 목표 위치를 화면 중앙으로 설정
        targetPosition = new Vector2(0f, 0f);
    }

    private void Update()
    {
        if (isFalling)
        {
            // 내려오는 속도를 기반으로 UI 오브젝트를 아래로 이동
            Vector2 newPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, fallSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = newPosition;
            fallSpeed += 5;
            // 목표 위치에 도달하면 애니메이션 중지
            if (newPosition == targetPosition)
            {
                isFalling = false;
                anim.SetTrigger("Play");
                Setting();
            }
        }
        if (Input.GetKeyUp(KeyCode.Return) && !Ending)
        {
            if (die)
            {
                Invoke("GoTitleScreen", 1f);
            }
            else if (GameClear)
            {
                Ending = true;
                GameManager.Instance.GetComponent<Ui_Controller>().endCredit.isEnding = true;
                fade.thisFade();
            }
        }
    }

    public void StartSlideIn()
    {
        isFalling = true;
    }

    public void Setting()
    {
        p = Player.instance;
        op = OptionManager.instance;
        dm = DataManager.instance;
        mp = MapManager.instance;
        KillCountText.text = p.EnemyKillCount.ToString();
        GetGoldText.text = p.TotalGetGold.ToString("F0")+" G";
        TotalDmageText.text = p.TotalDamaged.ToString("F0");
        PlayTimeText.text = op.returnTimerText();
        if (GameClear)
        {
            TitleText.text = "게임 클리어!";
            dm.GameClear(mp.Difficulty);
        }
        else
        {
            die = true;
        }
        List<GameObject> find = dm.finditem();
        GetItemText.text = (find.Count).ToString();
        for (int i = 0; i < find.Count; i++)
        {
            Instantiate(find[i], List.transform);
        }
        OptionManager.instance.Playing = false;
        dm.DeleteJson();
        dm.finditemList.Clear();
    }

    void GoTitleScreen()
    {
        sm.SceneLoader("Title_Scene");
    }

    public void invokefall()
    {
        Invoke("fall",1f);
    }

    public void fall()
    {
        isFalling = true;
    }
}
