using UnityEngine;
using TMPro;

public class JudgementUI : MonoBehaviour
{
    public TMP_Text text;
    public float showTime = 0.3f;

    float timer;

    void Start()
    {
        text.text = "";
    }

    void Update()
    {
        if (timer > 0f)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                text.text = "";
            }
        }
    }

    public void ShowPerfect()
    {
        text.text = "PERFECT";
        text.color = Color.yellow;
        timer = showTime;
    }

    public void ShowGood()
    {
        text.text = "GOOD";
        text.color = Color.green;
        timer = showTime;
    }

    public void ShowMiss()
    {
        text.text = "MISS";
        text.color = Color.red;
        timer = showTime;
    }
}
