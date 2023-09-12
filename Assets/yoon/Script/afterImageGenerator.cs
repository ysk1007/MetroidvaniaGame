using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class afterImageGenerator : MonoBehaviour
{
    /// ��Ʈ�������� �ƴ� ���λ�� �������� ������� ��ũ��Ʈ �Դϴ�.
    /// �Ƿº������� ���� ����ȭ�� ������ ���� �� �ֽ��ϴ�.

    public bool Active = false;
    [Header("�Ƿ翧")]
    public int SlideEA = 10;
    public float SlideTime = 0.1f;

    [Header("�ܻ� RGB ����")]
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
        // Component�� SpriteRender�� ������ ������ �۵����� �ʵ���.
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
            // ���̶���Ű�� �ʹ� ����������. Bank��� ������Ʈ�� ����� �� ���� ��ü�� �ִ´�.
            if (SlideNow > SilhouetteList.Count)
            {
                for (int i = SilhouetteList.Count; SlideNow > i; i++)
                {
                    GameObject SpriteCopy = new GameObject(transform.gameObject.name + " SilhouetteList " + i); // �� ���ӿ�����Ʈ�� ����
                    SpriteCopy.transform.parent = Bank.transform;
                    SpriteCopy.AddComponent<SpriteRenderer>(); // ��������Ʈ������ �ְ�
                    SilhouetteList.Insert(i, SpriteCopy); // �ѹ��� �����ϱ� ������ ����Ʈ�� �ִ´�.
                    //������Ʈ�� ��������Ʈ ������ �����ϴ� �������� �������ϰ� ���� ���ɼ��� ����.
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
        // ���߿� �����̵� ������ ���ϸ� �����
        if (SlideNow != SlideEA)
        {
            SlideNow = SlideEA;
            DefaultSet();
        }

        //Awake �ܰ迡�� ������ ���ٸ� �۵�����
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
                    SilhouetteList[Limit].transform.position = transform.position; // �Ƿ翧�� �Ƿ翧�� ���ο��� �̵��ϵ�,
                    SilhouetteList[Limit].transform.position += new Vector3(0, 0, 1); // �� ���̾� �ڿ���.
                    SilhouetteList[Limit].GetComponent<SpriteRenderer>().sprite = this.GetComponent<SpriteRenderer>().sprite; // ���� ������ ��������Ʈ�� �޾Ƽ� �Ƿ翧���� ����.
                    if (this.GetComponent<SpriteRenderer>().flipX == true)
                    {
                        SilhouetteList[Limit].GetComponent<SpriteRenderer>().flipX = true;
                    }
                    else
                    {
                        SilhouetteList[Limit].GetComponent<SpriteRenderer>().flipX = false;
                    }
                    SilhouetteList[Limit].transform.localScale = transform.localScale; // �¿������ ũ��� �����ϱ� ������ ũ�⵵ �޴´�.

                    float R = Random.Range(RedMin, RedMax), G = Random.Range(GreenMin, GreenMax), B = Random.Range(BlueMin, BlueMax);
                    SilhouetteList[Limit].GetComponent<SpriteRenderer>().color = new Color(R / 255, G / 255, B / 255, 1);
                    //�������� �Ѱ���

                    Limit++;
                    if (Limit > SilhouetteList.Count - 1)
                    {
                        Limit = 0;
                    }
                }
            }

            for (int i = 0; SilhouetteList.Count > i; i++)
            {
                //��� �Ƿ翧�� ���� ����������.
                SilhouetteList[i].GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1f / SilhouetteList.Count);
            }
        }
    }
}