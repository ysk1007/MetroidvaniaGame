using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProTipUi : MonoBehaviour
{
    public Player p;
    // Start is called before the first frame update
    void Start()
    {
        p = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (p.proSelectWeapon != 4)
        {
            Destroy(gameObject);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
