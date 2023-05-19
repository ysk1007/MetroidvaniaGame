using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MouseOnText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject TipText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // ���콺 Ŀ���� UI�� �������� �� ����� �ڵ� �ۼ�
        TipText.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺 Ŀ���� UI�� ����� �� ����� �ڵ� �ۼ�
        TipText.SetActive(false);
    }
}
