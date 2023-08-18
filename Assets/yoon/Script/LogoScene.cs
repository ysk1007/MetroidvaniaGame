using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScene : MonoBehaviour
{
    public Scene_Move sc;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SceneMove", 8f);
    }

    void SceneMove()
    {
        sc.SceneLoader("Title_Scene");
    }
}
