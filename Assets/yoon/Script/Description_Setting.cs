using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Description_Setting : MonoBehaviour
{
    public TextMeshProUGUI ItemName;
    public TextMeshProUGUI ItemStatus;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI tipText;
    public TextMeshProUGUI RateText;
    public TextMeshProUGUI ItemPrice;
    public Image ItemIcon;
    public Image Case;

    public GameObject KeyIcon;
    public Image MouseIcon;
    public Image GoldIcon;

    void Start()
    {
        Case.color = ItemName.color;
    }

    public void Setting(Image icon, TextMeshProUGUI Name, TextMeshProUGUI stat, TextMeshProUGUI Description, TextMeshProUGUI PriceText, TextMeshProUGUI Rate)
    {
        ItemName.text = Name.text;
        ItemStatus.text = stat.text;
        ItemDescription.text = Description.text;
        ItemIcon = icon;
        ItemPrice.text = PriceText.text;
        RateText.text = Rate.text;
    }

    public void DropSetting(Image icon, string Name, string stat, string Description, float Price, Color color, string rating)
    {
        ItemName.text = Name;
        ItemName.color = color;
        RateText.text = rating;
        RateText.color = color;
        ItemStatus.text = stat;
        ItemDescription.text = Description;
        ItemIcon.sprite = icon.sprite;
        ItemIcon.SetNativeSize();
        RectTransform rectTransform = ItemIcon.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x * 6, rectTransform.sizeDelta.y * 6);
        Case.color = color;
        tipText.text = "ащ╠Б";
        KeyIcon.SetActive(true);
        MouseIcon.enabled = false;
        GoldIcon.enabled = false;
    }
}
