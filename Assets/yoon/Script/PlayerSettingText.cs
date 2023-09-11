using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSettingText : MonoBehaviour
{
    public TextMeshProUGUI company;
    public TextMeshProUGUI version;
    // Start is called before the first frame update
    void Start()
    {
        company.text = Application.companyName;
        version.text = Application.version;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
