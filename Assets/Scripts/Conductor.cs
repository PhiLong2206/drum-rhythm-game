using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioSource musicSource;
   public float songBpm = 110f;      // tạm
public float firstBeatOffset = 0f;


    public float SongPositionInBeats { get; private set; }

    double songStartDspTime;
    float secPerBeat;

 void Start()
{
    secPerBeat = 60f / songBpm;

    musicSource.time = 0f;
    musicSource.Play();

    songStartDspTime = AudioSettings.dspTime;
}




    void Update()
    {
        double dspNow = AudioSettings.dspTime;
        float songPos = (float)(dspNow - songStartDspTime) - firstBeatOffset;
        if (songPos < 0) songPos = 0;

        SongPositionInBeats = songPos / secPerBeat;
    }
}
