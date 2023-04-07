using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshProUGUI 사용하려 참조

public class OptionScript : MonoBehaviour
{
    public TextMeshProUGUI ScreenText;
    public TextMeshProUGUI ResolutionText;
    public GameObject OptionCanvas;
    enum ScreenState
    {
        FullScreen,
        Windowscreen,
        NoBorder
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

    public void OptionClose()
    {
        OptionCanvas.SetActive(false);
    }

    public void OptionOpen()
    {
        OptionCanvas.SetActive(true);
    }
}
