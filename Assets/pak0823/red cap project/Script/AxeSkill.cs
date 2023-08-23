using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSkill : MonoBehaviour
{
    public Player player; // Player 스크립트를 가지고 있는 GameObject
    public SpriteRenderer spriteRenderer;
    public Transform trans;



    private void Start()
    {
        player = Player.instance.GetComponent<Player>();
    }

    void Update()
    {
        
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
