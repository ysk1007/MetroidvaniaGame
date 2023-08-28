using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsUi : MonoBehaviour
{
    public DataManager dm;
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

    private RectTransform rectTransform;
    private Vector2 targetPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        if (rectTransform == null)
        {
            return;
        }

        // ��ǥ ��ġ�� ȭ�� �߾����� ����
        targetPosition = new Vector2(0f, 0f);
        Debug.Log(targetPosition);
    }

    private void Update()
    {
        if (isFalling)
        {
            // �������� �ӵ��� ������� UI ������Ʈ�� �Ʒ��� �̵�
            Vector2 newPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetPosition, fallSpeed * Time.deltaTime);
            rectTransform.anchoredPosition = newPosition;
            fallSpeed += 5;
            // ��ǥ ��ġ�� �����ϸ� �ִϸ��̼� ����
            if (newPosition == targetPosition)
            {
                isFalling = false;
                anim.SetTrigger("Play");
                Setting();
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
        KillCountText.text = p.EnemyKillCount.ToString();
        GetGoldText.text = p.TotalGetGold.ToString("F0")+" G";
        TotalDmageText.text = p.TotalDamaged.ToString("F0");
        PlayTimeText.text = op.returnTimerText();
        if (GameClear)
        {
            TitleText.text = "���� Ŭ����!";
        }
        List<GameObject> find = dm.finditem();
        GetItemText.text = (find.Count).ToString();
        for (int i = 0; i < find.Count-1; i++)
        {
            Instantiate(find[i], List.transform);
        }
    }
}
