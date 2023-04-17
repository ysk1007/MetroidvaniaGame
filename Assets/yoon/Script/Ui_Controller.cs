using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Ui_Controller : MonoBehaviour
{
    //플레이어 체력,슬라이딩,경험치 바 ui 담을 변수
    public Slider PlayerHpBar; 
    public Slider SlidingBar; 
    public Slider ExpBar;

    public HpBar PlayerHp; //HpBar 스크립트 담을 변수

    //레벨, 최대체력, 현재체력 텍스트 변수
    public TextMeshProUGUI LevelVelueUi;
    public TextMeshProUGUI PlayerMaxHpText;
    public TextMeshProUGUI PlayerCurrentHpText;

    public GameObject inven_ui; //인벤토리 ui 판넬 담을 변수
    private bool openinven = false; //인벤토리 켜져있는지 여부 확인

    public int PlayerLevel = 1; //플레이어 레벨

    private void Awake()
    {
        PlayerHp = PlayerHpBar.GetComponent<HpBar>();
        PlayerHp.maxHp = 100f; //후에 json 파일을 읽어서 최대체력을 넘겨줘야 함
        PlayerHp.currentHp = 100f; //후에 json 파일을 읽어서 현재체력을 넘겨줘야 함
        PlayerMaxHpText.text = PlayerHp.maxHp.ToString("F0"); //후에 json 파일을 읽어서 최대체력 텍스트를 넘겨줘야 함
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0"); //후에 json 파일을 읽어서 현재체력 텍스트을 넘겨줘야 함
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) //인벤토리 i 입력 받았을 때
        {
            if (!openinven) //열려있지 않다면
            {
                inven_ui.SetActive(true); //인벤토리 ui 활성화
                openinven = true; // bool 값 트루
            }
            else //열려 있다면 닫기
            {
                inven_ui.SetActive(false);
                openinven = false;
            }
        }
    }

    public void GetExp(float value) //경험치 받는 함수
    {
        float Expvalue = value * 0.01f; //경험치 슬라이더 설정을 위해 퍼센트 단위로 바꿈
        if (ExpBar.value + Expvalue > 1f) //경험치 획득 후 100%보다 크다면
        {
            float OverExp = (ExpBar.value + Expvalue - 1f)*100f; //오버 경험치 계산
            LevelUp(); //레벨업 후
            GetExp(OverExp); // 오버경험치만큼 다시 재귀함수
        }
        else ExpBar.value += Expvalue; //경험치 획득
    }

    public void LevelUp() //레벨업
    {
        ExpBar.value = 0f;  //경험치 초기화
        PlayerLevel++; //레벨 증가
        LevelVelueUi.text = PlayerLevel.ToString(); // level ui 부분 텍스트 갱신
    }

    public void Damage(float damage) //데미지 받는 함수
    {
        PlayerHp.Dmg(damage); //HpBar 스크립트 함수에 데미지 받는 함수 호출
        if (PlayerHp.currentHp <= 0) //텍스트 갱신
        {
            PlayerCurrentHpText.text = "0";
        }
        else
            PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0"); //float -> String 변환 할 때 매개변수로 F'N' 넣으면 N만큼 자릿수 표기
    }

    public void Heal(float value) //회복 함수
    {
        PlayerHp.Heal(value);  //HpBar 스크립트 함수에 회복 함수 호출
        PlayerCurrentHpText.text = PlayerHp.currentHp.ToString("F0"); //텍스트 갱신
    }

    public void Sliding() //플레이어 조작에서 호출해야 할 듯
    {
        StartCoroutine(SlidingUP()); //코루틴 시작
    }


    //만들어야 할 함수
    // 아이템 장착, 해제 했을때 최대체력 갱신하는 함수


    IEnumerator SlidingUP() //슬라이딩 사용 후 슬라이더가 0부터 다시 1까지 차는 함수
    {
        float duration = 1f; //애니메이션 시간 (1초) <- 쿨 만큼 설정 하면 됨
        float elapsedTime = 0f; //경과 시간

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration; //경과 시간의 비율 (0~1)
            SlidingBar.value = Mathf.Lerp(0f, 1f, t); //시작 값에서 목표 값으로 보간
            elapsedTime += Time.deltaTime; //경과 시간 증가
            yield return null; //한 프레임 대기
        }

        SlidingBar.value = 1f; //목표 값으로 설정
    }
}
