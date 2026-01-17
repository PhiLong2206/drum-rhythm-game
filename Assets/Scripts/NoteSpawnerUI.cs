using UnityEngine;
using System.Collections;

public class NoteSpawnerUI : MonoBehaviour
{
    public Conductor conductor;
    public GameObject notePrefab;

    public LaneTarget[] laneTargets;  // kéo 5 Lane_* + JudgementUI vào đây

    public ScoreManager scoreManager;
    public ResultUI resultUI;

    public GameObject BG_VillageHappy;
    public GameObject BG_VillageAngry;

    public AudioSource sfxSource;   // SfxManager
    public AudioClip happyClip;     // tiếng vỗ tay
    public AudioClip angryClip;     // tiếng giận dữ

    bool resultShown = false;
    int nextIndex = 0;

    [System.Serializable]
    public struct NoteData
    {
        public float beat;
        public Lane lane;
    }

    public NoteData[] testNotes;

    void Start()
    {
        // đảm bảo 2 BG tắt lúc bắt đầu
        if (BG_VillageHappy != null) BG_VillageHappy.SetActive(false);
        if (BG_VillageAngry != null) BG_VillageAngry.SetActive(false);

        testNotes = new NoteData[]
        {
            new NoteData{ beat =  1f, lane = Lane.Left    },
            new NoteData{ beat =  2f, lane = Lane.Down    },
            new NoteData{ beat =  3f, lane = Lane.Up      },
            new NoteData{ beat =  4f, lane = Lane.Right   },

            new NoteData{ beat =  5f, lane = Lane.Left    },
            new NoteData{ beat =  6f, lane = Lane.Special },
            new NoteData{ beat =  7f, lane = Lane.Right   },
            new NoteData{ beat =  8f, lane = Lane.Up      },

            new NoteData{ beat =  9f, lane = Lane.Left    },
            new NoteData{ beat = 10f, lane = Lane.Down    },
            new NoteData{ beat = 11f, lane = Lane.Up      },
            new NoteData{ beat = 12f, lane = Lane.Special },
            new NoteData{ beat = 13f, lane = Lane.Right   },
            new NoteData{ beat = 14f, lane = Lane.Left    },
            new NoteData{ beat = 15f, lane = Lane.Down    },
            new NoteData{ beat = 16f, lane = Lane.Up      },
        };
    }

    void Update()
    {
        float songBeat = conductor.SongPositionInBeats;
        float beatsShownInAdvance = 4f;

        // Spawn note nếu còn
        if (nextIndex < testNotes.Length)
        {
            var data = testNotes[nextIndex];

            if (data.beat - songBeat <= beatsShownInAdvance)
            {
                LaneTarget target = System.Array.Find(
                    laneTargets,
                    t => t.lane == data.lane
                );

                if (target != null)
                {
                    Canvas canvas = FindObjectOfType<Canvas>();
                    GameObject go = Instantiate(notePrefab, canvas.transform);

                    var note = go.GetComponent<NoteUI>();
                    note.Init(
                        data.lane,
                        data.beat,
                        target.rect,
                        conductor,
                        scoreManager,
                        target.judgementUI
                    );
                }

                nextIndex++;
            }
        }

        // Kết thúc bài: đã spawn xong VÀ không còn NoteUI nào (tất cả đã PERFECT/GOOD/MISS)
        if (!resultShown && nextIndex >= testNotes.Length)
        {
            if (FindObjectsOfType<NoteUI>().Length == 0)
            {
                resultShown = true;

                Debug.Log("END SONG, missCount = " + scoreManager.missCount);

                if (scoreManager.missCount == 0)
                {
                    Debug.Log("SHOW HAPPY ENDING");

                    if (BG_VillageHappy != null) BG_VillageHappy.SetActive(true);
                    if (BG_VillageAngry != null) BG_VillageAngry.SetActive(false);

                    if (sfxSource != null && happyClip != null)
                        sfxSource.PlayOneShot(happyClip);
                }
                else
                {
                    Debug.Log("SHOW ANGRY ENDING");

                    if (BG_VillageAngry != null) BG_VillageAngry.SetActive(true);
                    if (BG_VillageHappy != null) BG_VillageHappy.SetActive(false);

                    if (sfxSource != null && angryClip != null)
                        sfxSource.PlayOneShot(angryClip);
                }

                // Đợi 2 giây rồi mới hiện bảng kết quả
                StartCoroutine(ShowResultDelay());
            }
        }
    }

    IEnumerator ShowResultDelay()
    {
        yield return new WaitForSeconds(2f);  // chỉnh số giây tùy ý
        resultUI.ShowResult();
    }
}
