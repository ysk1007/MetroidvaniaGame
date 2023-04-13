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
        monster.InitSetting();
    }

    // Update is called once per frame
    void Update()
    {
        monster.Short_Monster(target);
    }
        
}
