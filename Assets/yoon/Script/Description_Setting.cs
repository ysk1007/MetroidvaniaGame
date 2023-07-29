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
    public TextMeshProUGUI ItemPrice;
    public Image ItemIcon;
    public Image Case;

    void Start()
    {
        Case.color = ItemName.color;
    }

    public void Setting(Image icon, TextMeshProUGUI Name, TextMeshProUGUI stat, TextMeshProUGUI Description, TextMeshProUGUI PriceText)
    {
        ItemName.text = Name.text;
        ItemStatus.text = stat.text;
        ItemDescription.text = Description.text;
        ItemIcon = icon;
        ItemPrice.text = PriceText.text;
    }
}
