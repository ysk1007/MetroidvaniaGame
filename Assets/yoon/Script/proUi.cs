using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Profiling;

public class proUi : MonoBehaviour
{
    public TextMeshProUGUI value;
    public Image bar;
    public Proficiency_ui pro;

    public Color red;
    public Color blue;
    public Color green;
    // Start is called before the first frame update
    void Start()
    {
        pro = GameManager.Instance.GetComponent<Proficiency_ui>();
    }

    // Update is called once per frame
    void Update()
    {
        value.text = (pro.Profill.fillAmount * 100).ToString("F0") + "%";
        bar.fillAmount = pro.Profill.fillAmount;
        if (bar.fillAmount < 0.34)
        {
            bar.color = green;
        }
        else if (bar.fillAmount > 0.34 && bar.fillAmount < 0.67)
        {
            bar.color = blue;
        }
        else
        {
            bar.color = red;
        }
    }
}
