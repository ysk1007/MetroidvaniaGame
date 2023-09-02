using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossClearPos : MonoBehaviour
{
    public GameObject PB;
    public static BossClearPos instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void function(int i)
    {
        GameObject go = PB;
        go.GetComponent<BossClearUi>().count = i;
        Instantiate(go, gameObject.transform);
    }
}
