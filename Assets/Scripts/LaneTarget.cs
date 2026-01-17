using UnityEngine;

public class LaneTarget : MonoBehaviour
{
    public Lane lane;
    [HideInInspector] public RectTransform rect;

    public JudgementUI judgementUI;   // kéo JudgementUI của lane này vào

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
}
