using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Controller : MonoBehaviour
{

    public Enemy monster;
    public Transform target;


    void Start()
    {
        target = Player.instance.gameObject.transform;
        monster.InitSetting();
        monster.GolemBossOneTime();
    }


    void Update()
    {
        monster.GolemBoss(target);
    }
}
