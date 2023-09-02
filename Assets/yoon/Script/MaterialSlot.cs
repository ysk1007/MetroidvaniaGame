using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MaterialSlot : MonoBehaviour
{
    public DataManager dm;
    public Player player;

    public Image FristMaterial;
    public Image SecondMaterial;
    public Image ThirdMaterial;

    public MouseOnItem FirstScript;
    public MouseOnItem SecondScript;
    public MouseOnItem ThirdScript;
    public Color color = new Color32(255, 255, 255, 255);
    // Start is called before the first frame update
    void Start()
    {
        player = Player.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.FirstMaterial)
        {
            FristMaterial.color = color;
            FirstScript.enabled = true;
        }
        if (player.SecondMaterial)
        {
            SecondMaterial.color = color;
            SecondScript.enabled = true;
        }
        if (player.ThirdMaterial)
        {
            ThirdMaterial.color = color;
            ThirdScript.enabled = true;
        }
    }
}
