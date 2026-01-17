using UnityEngine;

public class NoteUI : MonoBehaviour
{
    public Lane lane;
    public float beat;                 // beat mà note phải trùng với nút
    public RectTransform rect;

    RectTransform target;
    Conductor conductor;

    float beatsShownInAdvance = 10f;   // xuất hiện trước nút 10 beat

    public bool handled = false;       // đã PERFECT/GOOD/MISS chưa

    public ScoreManager scoreManager;
    public JudgementUI judgementUI;

    public void Init(
        Lane lane,
        float beat,
        RectTransform target,
        Conductor conductor,
        ScoreManager scoreManager,
        JudgementUI judgementUI
    )
    {
        this.lane = lane;
        this.beat = beat;
        this.target = target;
        this.conductor = conductor;
        this.scoreManager = scoreManager;
        this.judgementUI = judgementUI;
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (conductor == null || target == null) return;

        float songBeat = conductor.SongPositionInBeats;

        // nếu còn xa hơn beatsShownInAdvance thì chưa cần hiện
        if (beat - songBeat > beatsShownInAdvance)
            return;

        // t = 0 (mới spawn, ở xa), t = 1 (đến nút)
        float t = 1f - ((beat - songBeat) / beatsShownInAdvance);
        t = Mathf.Clamp01(t);

        Vector3 startPos = target.position + Vector3.down * 400f;   // bắt đầu thấp hơn
        Vector3 endPos   = target.position;

        rect.position = Vector3.Lerp(startPos, endPos, t);

        // nếu trôi qua nút lâu rồi mà chưa bị hit -> auto MISS
        if (!handled && songBeat - beat > 0.5f)
        {
            AutoMiss();
        }
    }

    public void Hit()
    {
        if (handled) return;   // tránh double hit
        handled = true;

        // PERFECT/GOOD được tính ở LaneInputUI (AddPerfect/AddGood)
        Destroy(gameObject);
    }

    void AutoMiss()
    {
        handled = true;

        if (scoreManager != null)
            scoreManager.AddMiss();

        if (judgementUI != null)
            judgementUI.ShowMiss();

        Destroy(gameObject);
    }
}
