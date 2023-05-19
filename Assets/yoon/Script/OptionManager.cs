using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionManager : MonoBehaviour
{
    public static OptionManager instance;
    public GameObject option_panel;
    private bool open_option = false;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!open_option)
            {
                option_panel.SetActive(true);
                open_option = true;
            }
            else
            {
                option_panel.SetActive(false);
                open_option = false;
            }
        }
    }

}
