using UnityEngine;
using TMPro;   // nếu dùng TextMeshPro

public class ScoreUI : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TMP_Text scoreText;
    public TMP_Text comboText;

    void Update()
    {
        scoreText.text = $"Score: {scoreManager.score}";
        comboText.text = $"Combo: {scoreManager.combo}";
    }
}
