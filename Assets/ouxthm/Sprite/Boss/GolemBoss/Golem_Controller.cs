using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem_Controller : MonoBehaviour
{

    public Enemy monster;
    public Player player;


    void Start()
    {
        player = Player.instance;
        monster.InitSetting();
        monster.GolemBossOneTime();
    }


    void Update()
    {
        monster.GolemBoss(player.transform);
    }
}
