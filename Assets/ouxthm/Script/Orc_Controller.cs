using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Controller : MonoBehaviour
{

    public Enemy monster;
    public Transform target;


    void Start()
    {
        target = Player.instance.gameObject.transform;
        monster.InitSetting(MapManager.instance.Difficulty);
        monster.orcbossOnetime();
    }


    void Update()
    {
        monster.OrcBoss(target);
    }
}
