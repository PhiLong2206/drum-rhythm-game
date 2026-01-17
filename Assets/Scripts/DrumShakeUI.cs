using UnityEngine;

public class DrumShakeUI : MonoBehaviour
{
    public RectTransform target;     // ảnh trống
    public float shakeDuration = 0.1f;
    public float shakeAmount = 10f;  // pixel

    Vector3 originalPos;
    float shakeTimer;

    void Start()
    {
        if (target == null)
            target = GetComponent<RectTransform>();
        originalPos = target.anchoredPosition;
    }

    void Update()
    {
        if (shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            float offsetX = Random.Range(-1f, 1f) * shakeAmount;
            float offsetY = Random.Range(-1f, 1f) * shakeAmount;
            target.anchoredPosition = originalPos + new Vector3(offsetX, offsetY, 0f);

            if (shakeTimer <= 0f)
                target.anchoredPosition = originalPos;
        }
    }

    public void Shake()
    {
        shakeTimer = shakeDuration;
    }
}
