using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    int count = 1;
    string mark = ".";
    string BaseText = "·ÎµùÁß";
    // Update is called once per frame
    void Start()
    {
        Mark();
    }

    void Mark()
    {
        loadingText.text = BaseText;
        for (int i = 0; i < count; i++)
        {
            loadingText.text += mark;
        }
        count++;
        if (count > 3)
        {
            count = 1;
        }
        Invoke("Mark",0.5f);
    }
}
