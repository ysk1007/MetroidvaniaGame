using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseOnText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // 마우스 커서가 UI에 진입했을 때 실행될 코드 작성
        TipText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 마우스 커서가 UI를 벗어났을 때 실행될 코드 작성
        TipText.SetActive(false);
    }
}
