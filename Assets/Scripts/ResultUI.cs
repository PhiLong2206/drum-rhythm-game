using UnityEngine;
using TMPro;

public class ResultUI : MonoBehaviour
{
    public ScoreManager scoreManager;

    public TMP_Text scoreText;
    public TMP_Text comboText;
    public TMP_Text accuracyText;
    public TMP_Text rankText;

    public void ShowResult()
    {
        gameObject.SetActive(true);

        scoreText.text = $"Score: {scoreManager.score}";
        comboText.text = $"Max Combo: {scoreManager.maxCombo}";
        accuracyText.text = $"Accuracy: {scoreManager.Accuracy:F1}%";

        float acc = scoreManager.Accuracy;
        string rank;
        if (acc >= 95f)      rank = "S";
        else if (acc >= 85f) rank = "A";
        else if (acc >= 70f) rank = "B";
        else if (acc >= 50f) rank = "C";
        else                 rank = "D";

        rankText.text = $"Rank: {rank}";
    }
}
