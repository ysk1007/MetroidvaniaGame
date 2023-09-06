using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoScene : MonoBehaviour
{
    public Scene_Move sc;
    public bool story = false;
    // Start is called before the first frame update
    void Start()
    {
        if (story)
        {
            Invoke("ingameScene", 51f);
        }
        else
        {
            Invoke("SceneMove", 13f);
            DataManager.instance.CreateOptionJson();
        }
    }

    void SceneMove()
    {
        sc.SceneLoader("Title_Scene");
    }

    void ingameScene()
    {
        sc.SceneLoader("ingame_scene");
    }
}
