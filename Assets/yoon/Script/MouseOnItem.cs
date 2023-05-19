using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class MouseOnItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject ItemSlot;

    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemExplanationText;
    public TextMeshProUGUI ItemStatText;
    public Image Itemimg;
    public GameObject DescriptionGo;
    public GameObject Instance;

    public void OnPointerEnter(PointerEventData eventData)
    {
        var Object = Instantiate(DescriptionGo, gameObject.transform);
        Object.transform.SetParent(gameObject.transform.parent.transform.parent);
        // ���콺 Ŀ���� UI�� �������� �� ����� �ڵ� �ۼ�
        /*        if (ItemSlot.GetComponentInChildren<itemStatus>() != null)
                {
                    itemStatus itemstat = ItemSlot.GetComponentInChildren<itemStatus>();
                    itemstat.Using(Itemimg, ItemNameText, ItemExplanationText, ItemStatText);
                }*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // ���콺 Ŀ���� UI�� ����� �� ����� �ڵ� �ۼ�
        Object.Destroy(this);

    }
}
