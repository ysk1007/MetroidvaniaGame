using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatisticsUi : MonoBehaviour
{
    public Player p;
    public OptionManager op;
    public TextMeshProUGUI TitleText;
    public TextMeshProUGUI KillCountText;
    public TextMeshProUGUI GetGoldText;
    public TextMeshProUGUI TotalDmageText;
    public TextMeshProUGUI PlayTimeText;

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
        KillCountText.text = p.EnemyKillCount.ToString();
        GetGoldText.text = p.TotalGetGold.ToString("F0")+" G";
        TotalDmageText.text = p.TotalDamaged.ToString("F0");
        PlayTimeText.text = op.returnTimerText();
        if (GameClear)
        {
            TitleText.text = "���� Ŭ����!";
        }
    }
}
