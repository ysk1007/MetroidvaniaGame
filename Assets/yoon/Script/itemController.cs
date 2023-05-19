using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class itemController : MonoBehaviour
{
    public itemStatus itemStatus;
    public itemStatus[] itemStatus_list;

    // Start is called before the first frame update
    void Start()
    {
        itemStatus.InitSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
