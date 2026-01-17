using UnityEngine;

public class NoteSpawnerUI : MonoBehaviour
{
    public Conductor conductor;
    public GameObject notePrefab;

    public LaneTarget[] laneTargets;  // kéo 5 Lane_* + JudgementUI vào đây

    public ScoreManager scoreManager;
    public ResultUI resultUI;
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

        // Chỉ hiện kết quả khi: đã spawn xong VÀ đã đánh/miss hết 16 note
        if (!resultShown && nextIndex >= testNotes.Length)
        {
            int total = scoreManager.TotalNotes;

            if (total >= testNotes.Length)
            {
                resultShown = true;
                resultUI.ShowResult();
            }
        }
    }
}
