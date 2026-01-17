using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public int combo;
    public int maxCombo;

    public int perfectCount;
    public int goodCount;
    public int missCount;

    public void AddPerfect()
    {
        score += 100;
        combo++;
        if (combo > maxCombo) maxCombo = combo;
        perfectCount++;
    }

    public void AddGood()
    {
        score += 50;
        combo++;
        if (combo > maxCombo) maxCombo = combo;
        goodCount++;
    }

    public void AddMiss()
    {
        combo = 0;
        missCount++;
    }

    public int TotalNotes => perfectCount + goodCount + missCount;

    public float Accuracy
    {
        get
        {
            int total = TotalNotes;
            if (total == 0) return 0f;
            float acc = (perfectCount + goodCount * 0.5f) / total;
            return acc * 100f;
        }
    }
}

