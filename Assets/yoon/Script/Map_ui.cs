using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Map_ui : MonoBehaviour
{
    public GameObject MapUi;
    public bool MapUiOpen = false;
    public MapManager mp;
    public TextMeshProUGUI mapName;
    public TextMeshProUGUI stageNumber;
    public GameObject[] ClearBadge;
    public GameObject[] pos;
    public GameObject Player;

    public int CurrentMap;
    public int CurrentStage;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MapUiOpen)
            {
                MapUi.SetActive(false);
                MapUiOpen = false;
            }
            else
            {
                MapUi.SetActive(true);
                MapUiOpen = true;
            }
        }
    }

    public void Setting()
    {
        CurrentMap = mp.CurrentStage[0];
        CurrentStage = mp.CurrentStage[1]+1;

        switch (CurrentMap)
        {
            case 0:
                mapName.text = "±íÀº ½£";
                break;
            case 1:
                mapName.text = "À¸½º½ºÇÑ ¹¦Áö";
                break;
            case 2:
                mapName.text = "¹ö·ÁÁø Æó±¤";
                break;
        }

        stageNumber.text = (CurrentMap + 1) + "-"+ CurrentStage;

        for (int i = 0; i < ClearBadge.Length; i++)
        {
            if (i < CurrentStage-1)
            {
                ClearBadge[i].SetActive(true);
            }
            else
            {
                ClearBadge[i].SetActive(false);
            }
        }
        Player.transform.parent = pos[CurrentStage - 1].transform;
        Vector3 newPos = Player.GetComponent<RectTransform>().anchoredPosition;
        newPos.x = 0;
        newPos.y = 0;
        Player.GetComponent<RectTransform>().anchoredPosition = newPos;
    }
}
