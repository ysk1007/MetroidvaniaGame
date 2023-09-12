using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterConstroller : MonoBehaviour
{

    public Enemy monster;
    public Transform target;

    
    void Start()
    {
        target = Player.instance.gameObject.transform;
        monster.InitSetting(MapManager.instance.Difficulty);
        monster.onetime();
    }

    void Update()
    {
        monster.Short_Monster(target);
    }
        
}
