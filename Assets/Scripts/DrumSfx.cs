using UnityEngine;

public class DrumSfx : MonoBehaviour
{
    public static DrumSfx Instance;

    public AudioSource audioSource;
    public AudioClip hitPerfect;
    public AudioClip hitGood;
    public AudioClip hitMiss;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

   public void PlayPerfect()
{
    Debug.Log("PlayPerfect called " + (hitPerfect == null ? "NULL" : "OK"));
    if (hitPerfect != null)
        audioSource.PlayOneShot(hitPerfect, 1f);
}


    public void PlayGood()
    {
        if (hitGood != null)
            audioSource.PlayOneShot(hitGood, 0.9f);
    }

    public void PlayMiss()
    {
        if (hitMiss != null)
            audioSource.PlayOneShot(hitMiss, 0.7f);
    }
    
}
