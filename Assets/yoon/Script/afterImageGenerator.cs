using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afterImageGenerator : MonoBehaviour
{
    /// 포트폴리오가 아닌 개인사용 목적으로 만들어진 스크립트 입니다.
    /// 실력부족으로 인해 최적화에 문제가 있을 수 있습니다.

    public bool Active = false;
    [Header("실루엣")]
    public int SlideEA = 10;
    public float SlideTime = 0.1f;

    [Header("잔상 RGB 범위")]
    public float RedMin = 0;
    public float RedMax = 255;
    public float GreenMin = 0;
    public float GreenMax = 255;
    public float BlueMin = 0;
    public float BlueMax = 255;

    GameObject Bank;
    List<GameObject> SilhouetteList = new List<GameObject>();
    int Limit = 0;
    int SlideNow = 0;
    float delta = 0;
    bool ErrorDebug = false;

    private void Awake()
    {
        // Component로 SpriteRender를 가지지 않으면 작동하지 않도록.
        if (!GetComponent<SpriteRenderer>())
        {
            Debug.Log("Not Find SpriteRender. from Move_Slide for " + gameObject.name);
            ErrorDebug = true;
        }
    }

    void SlideCreate()
    {
        if (!Bank)
        {
            Bank = new GameObject(gameObject.name + " SilhouetteListList Bank");
            // 하이라이키가 너무 난잡해진다. Bank라는 오브젝트를 만들어 그 하위 개체로 넣는다.
            if (SlideNow > SilhouetteList.Count)
            {
                for (int i = SilhouetteList.Count; SlideNow > i; i++)
                {
                    GameObject SpriteCopy = new GameObject(transform.gameObject.name + " SilhouetteList " + i); // 빈 게임오브젝트를 만들어서
                    SpriteCopy.transform.parent = Bank.transform;
                    SpriteCopy.AddComponent<SpriteRenderer>(); // 스프라이트렌더를 넣고
                    SilhouetteList.Insert(i, SpriteCopy); // 한번에 관리하기 쉽도록 리스트에 넣는다.
                    //오브젝트와 스프라이트 렌더를 생성하는 과정에서 성능저하가 있을 가능성이 있음.
                }
            }
        }
    }

    void DefaultSet()
    {
        delta = 0;
        Limit = 0;

        SilhouetteList.Clear();
        Destroy(Bank);
    }

    void Update()
    {
        // 도중에 슬라이드 갯수가 변하면 재생성
        if (SlideNow != SlideEA)
        {
            SlideNow = SlideEA;
            DefaultSet();
        }

        //Awake 단계에서 에러가 났다면 작동금지
        if (ErrorDebug && SlideNow > 0)
            return;


        SlideCreate();
        delta += Time.deltaTime;

        if (delta > SlideTime)
        {
            delta = 0;
            if (Active)
            {
                if (SilhouetteList.Count > 0)
                {
                    SilhouetteList[Limit].transform.position = transform.position; // 실루엣을 실루엣의 주인에게 이동하되,
                    SilhouetteList[Limit].transform.position += new Vector3(0, 0, 1); // 한 레이어 뒤에서.
                    SilhouetteList[Limit].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite; // 지금 주인의 스프라이트를 받아서 실루엣에게 적용.
                    if (this.GetComponent<SpriteRenderer>().flipX == true)
                    {
                        SilhouetteList[Limit].GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        SilhouetteList[Limit].GetComponent<SpriteRenderer>().flipX = false;
                    }
                    SilhouetteList[Limit].transform.localScale = transform.localScale; // 좌우반전을 크기로 적용하기 때문에 크기도 받는다.

                    float R = Random.Range(RedMin, RedMax), G = Random.Range(GreenMin, GreenMax), B = Random.Range(BlueMin, BlueMax);
                    SilhouetteList[Limit].GetComponent<SpriteRenderer>().color = new Color(R / 255, G / 255, B / 255, 1);
                    //무지개빛 총공격

                    Limit++;
                    if (Limit > SilhouetteList.Count - 1)
                    {
                        Limit = 0;
                    }
                }
            }

            for (int i = 0; SilhouetteList.Count > i; i++)
            {
                //모든 실루엣이 점점 투명해져라.
                SilhouetteList[i].GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f / SilhouetteList.Count);
            }
        }
    }
}