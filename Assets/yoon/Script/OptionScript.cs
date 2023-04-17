using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI ����Ϸ� ����
using UnityEngine.SceneManagement;

public class OptionScript : MonoBehaviour
{
    public TextMeshProUGUI ScreenText;
    public TextMeshProUGUI ResolutionText;
    public GameObject OptionCanvas;
    public GameObject Ui_Panel;
    public static GameObject instance; //���� �Ŵ��� �ν��Ͻ�(Ŭ������ ���Ǹ� ���� ������� ��ü)
    enum ScreenState
    {
        FullScreen,
        Windowscreen,
        NoBorder
    }

    private void Awake()
    {
        //�̱��� ����
        if (instance == null) //���� �Ŵ����� ������
        {
            instance = this.gameObject;
            DontDestroyOnLoad(instance); //�� ��ȯ�� �ı����� ����
        }
        else //���� �Ŵ����� �̹� ������
        {
            Destroy(gameObject); //�ڽ��� �ı�
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ValueChange()
    {
        
    }

    public void OptionClose() //�ɼ� â �ݴ� �Լ�
    {
        Ui_Panel.SetActive(false);
    }

    public void OptionOpen() //�ɼ� â Ȱ��ȭ �Լ�
    {
        Ui_Panel.SetActive(true);
    }
}
