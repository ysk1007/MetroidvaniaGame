using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossClearUi : MonoBehaviour
{
    public Vector2 targetPosition;
    public Vector2 StartPosition;
    public float fallSpeed = 750f;
    public bool isFalling = false;
    public Animator anim;
    public Image icon;
    public TextMeshProUGUI TitleText;

    public Image First;
    public Image Second;
    public Image Third;
    public int count;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delete", 6f);
        anim.SetTrigger("Anim");

        switch (count)
        {
            case 1:
                icon.sprite = First.sprite;
                icon.SetNativeSize();
                icon.rectTransform.sizeDelta = new Vector2(icon.rectTransform.sizeDelta.x * 4, icon.rectTransform.sizeDelta.y * 4);
                TitleText.text = "Ã¹ ¹øÂ° Àç·á È¹µæ !";
                break;
            case 2:
                icon.sprite = Second.sprite;
                icon.SetNativeSize();
                icon.rectTransform.sizeDelta = new Vector2(icon.rectTransform.sizeDelta.x * 3, icon.rectTransform.sizeDelta.y * 3);
                TitleText.text = "µÎ ¹øÂ° Àç·á È¹µæ !";
                break;
            case 3:
                icon.sprite = Third.sprite;
                icon.SetNativeSize();
                icon.rectTransform.sizeDelta = new Vector2(icon.rectTransform.sizeDelta.x * 3, icon.rectTransform.sizeDelta.y * 3);
                TitleText.text = "¼¼ ¹øÂ° Àç·á È¹µæ !";
                break;
        }

    }

    public void Delete()
    {
        Destroy(gameObject);
    }
}
