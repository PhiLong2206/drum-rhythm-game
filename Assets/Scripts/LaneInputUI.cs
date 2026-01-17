using UnityEngine;
using UnityEngine.UI;

public class LaneInputUI : MonoBehaviour
{
    public ScoreManager scoreManager;

    public Lane lane;
    public KeyCode key;
    public Image laneImage;
    public JudgementUI judgementUI;

    public DrumShakeUI drumShake;

    [Header("Timing Window (beats)")]
    public float perfectWindow = 0.2f;
    public float goodWindow = 0.4f;

    [Header("Hit Scale")]
    public float pressedScale = 1.2f;
    public float returnSpeed = 10f;

    Color normalColor;
    public Color pressedColor = Color.yellow;

    Conductor conductor;

    void Start()
    {
        conductor = FindObjectOfType<Conductor>();
        if (laneImage == null)
            laneImage = GetComponent<Image>();

        normalColor = laneImage.color;
    }

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            laneImage.color = pressedColor;
            laneImage.rectTransform.localScale = Vector3.one * pressedScale;
            TryHitNote();
        }

        if (Input.GetKeyUp(key))
        {
            laneImage.color = normalColor;
        }

        // scale về 1 dần dần
        laneImage.rectTransform.localScale = Vector3.Lerp(
            laneImage.rectTransform.localScale,
            Vector3.one,
            Time.deltaTime * returnSpeed
        );
    }

    void TryHitNote()
    {
        NoteUI[] allNotes = FindObjectsOfType<NoteUI>();
        NoteUI best = null;
        float bestDiff = Mathf.Infinity;
        float songBeat = conductor.SongPositionInBeats;

        foreach (var n in allNotes)
        {
            if (n.lane != lane) continue;

            float diff = Mathf.Abs(n.beat - songBeat);
            if (diff < bestDiff)
            {
                bestDiff = diff;
                best = n;
            }
        }

        if (best == null) return;

        // Gọi chấm điểm + score + sfx
        JudgeHit(bestDiff, best);
    }

    void JudgeHit(float diff, NoteUI note)
    {
        if (diff <= perfectWindow)
        {
            Debug.Log(lane + " PERFECT " + diff);
            note.Hit();
            DrumSfx.Instance?.PlayPerfect();
            scoreManager.AddPerfect();
            judgementUI?.ShowPerfect();
            drumShake?.Shake();
        }
        else if (diff <= goodWindow)
        {
            Debug.Log(lane + " GOOD " + diff);
            note.Hit();
            DrumSfx.Instance?.PlayGood();
            scoreManager.AddGood();
            judgementUI?.ShowGood();
            drumShake?.Shake();
        }
        else
        {
            Debug.Log(lane + " MISS " + diff);
            DrumSfx.Instance?.PlayMiss();
            scoreManager.AddMiss();
            judgementUI?.ShowMiss();
            drumShake?.Shake();
        }
    }
}
