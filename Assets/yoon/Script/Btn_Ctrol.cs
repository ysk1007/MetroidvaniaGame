using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // ui �̹����� �ǵ帮������ ����
using TMPro; //TextMeshProUGUI ����Ϸ� ����
public class Btn_Ctrol : MonoBehaviour
{
    public Button[] buttons; //��ư�� ���� �迭
    public OptionScript OptionCanvas; //�ɼ� ĵ���� ���� ������Ʈ ����
    public GameObject Panel; //�ɼ� Panel_ui ���� ������Ʈ ����
    public AudioClip clip; // ����� Ŭ��(ȿ����) ���� ����
    Scene_Move Scene_Move; //�� �̵� ��ũ��Ʈ ����

    public int[] index = { }; //��ư�� �ε����� ���� �迭
    public int currentIndex; //���� �ε����� ����ų ����
    private void Start()
    {
        OptionCanvas = GameObject.Find("Option_Canvas").GetComponent<OptionScript>(); //�ɼ� ĵ������ ã�ƿ� OptionScript�� �����ɴϴ�
        Panel = OptionCanvas.transform.GetChild(0).gameObject; //�ɼ� ĵ���� �ٷ� ���� ������Ʈ�� ������
        Scene_Move = GetComponent<Scene_Move>(); //�� �̵� ��ũ��Ʈ ������
        buttons = GetComponentsInChildren<Button>(); //��ư�� �迭�� ����
        index = new int[buttons.Length]; // �ε��� ���̴� ��ư�迭�� ���̸�ŭ
        for (int i = 0; i < buttons.Length; i++) //�迭�� �ε��� �߰���
        {
            index[i] = i;
        }
        currentIndex = 0; //���� �ε����� 0���� ����
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.DownArrow)&& !Panel.activeSelf) //�ɼ� ĵ������ ���������� �Ʒ� ����Ű�� ������
        {
            if (currentIndex == buttons.Length - 1) //���� �ε����� �������̶��
            {
                GetBtnImpo(0); //ó�� �ε����� ���ư�
            }
            else { GetBtnImpo(currentIndex + 1); } //���� �ε��� 1 �߰�
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow) && !Panel.activeSelf) //�ɼ� ĵ������ ���������� �� ����Ű�� ������
        {
            if (currentIndex == 0) //���� �ε����� ù ��°���
            {
                GetBtnImpo(buttons.Length - 1); //������ �ε����� ��
            }
            else { GetBtnImpo(currentIndex - 1); } //���� �ε��� 1 ����
        }
        else if (Input.GetKeyUp(KeyCode.Return) && !Panel.activeSelf)  //����Ű�� ������ ���� �ε��� ��� ���
        {
            BtnFunction(currentIndex); //-> BtnFunction �Լ�
        }
        select_btn(currentIndex); //-> select_btn �Լ�
    }

    void select_btn(int index) //���õ� �ε����� ��ư �����ϴ� �Լ�
    {
        TextMeshProUGUI text = buttons[index].GetComponentInChildren<TextMeshProUGUI>(); //���� �ε����� ��ư�� ���ڸ� ������
        text.fontSize = 50; //��Ʈ ����� Ű��
        text.color = new Color32(226, 190, 50, 255); //��������� ����
    }

    public void GetBtnImpo(int index) //��ư Ŭ���� �������� ������ �޴� �Լ�
    {
        if(index == currentIndex) //���� �ε����� ���� �ε����� ������
        {
            BtnFunction(currentIndex); //��� ����
        }
        else //���ο� ��ư�� ������
        {
            SoundManager.instance.SFXPlay("Seleect", clip); //��ư Ŭ�� ȿ����
            TextMeshProUGUI text = buttons[currentIndex].GetComponentInChildren<TextMeshProUGUI>();

            //���� ������ ��ư ���� ȿ���� ǰ
            text.fontSize = 40; 
            text.color = new Color32(255, 255, 255, 255);

            currentIndex = index; //���� �ε����� Ŭ���� ��ư�� �ε����� ����
        }
        
    }

    void BtnFunction(int index) //��� �Լ�
    {
        switch(index) //����ġ������ �ε��� ��ȣ�� �޾� �� ��� ����
        {
            case 0:
                Debug.Log("�� �̾߱� ����");
                Scene_Move.SceneLoader("ingame");
                break;
            case 1:
                Debug.Log("�̾ ����");
                break;
            case 2:
                OptionCanvas.GetComponent<OptionScript>().OptionOpen();
                break;
            case 3:
                Debug.Log("���� ����");
                break;
        }
    }
}
