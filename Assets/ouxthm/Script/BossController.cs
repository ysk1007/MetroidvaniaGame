using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BossController : MonoBehaviour
{

    public Enemy monster;
    public Transform target;


    void Start()
    {
        target = Player.instance.gameObject.transform;
        monster.InitSetting(MapManager.instance.Difficulty);
        monster.bossOnetime();
    }

    void Update()
    {
        monster.Boss(target);
    }

}
