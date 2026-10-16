using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VInspector;

public class ReplayRecoder : MonoBehaviour
{
    [Header("Replay")]
    [SerializeField] private GameObject recoderPrefab;

    [Header("Replay UI")]
    [SerializeField] private Slider timelineSlider;

    [Tooltip("How many samples to record per second.")]
    [SerializeField] private int samplesPerSecond = 30;

    [Tooltip("Maximum recording length in seconds.")]
    [SerializeField] private float maxRecordingTime = 60f;

    [Header("Recording")]
    [SerializeField] private bool recordOnStart = true;

    [Header("Playback")]
    [SerializeField] private float defaultPlaybackSpeed = 1f;
    [SerializeField] private bool loopReplay = false;
    private bool isScrubbing;
    private struct ReplayFrame
    {
        public Vector3 position;
        public Quaternion rotation;
        public float time;

        public ReplayFrame(Vector3 position, Quaternion rotation, float time)
        {
            this.position = position;
            this.rotation = rotation;
            this.time = time;
        }
    }

    private readonly List<ReplayFrame> frames = new List<ReplayFrame>();

    private float recordingStartTime;
    private float nextSampleTime;
    private float sampleInterval;

    private bool isRecording;
    private bool isReplaying;
    private bool isPaused;

    private GameObject replayObject;

    private float replayTime;
    private int replayFrameIndex;

    private float playbackSpeed = 1f;

    // ---------------------------------------------------------
    // Unity
    // ---------------------------------------------------------

    private void Start()
    {
        sampleInterval = 1f / Mathf.Max(1, samplesPerSecond);

        playbackSpeed = defaultPlaybackSpeed;


    }


    public void StartRecordingFromOutside()
    {
        sampleInterval = 1f / Mathf.Max(1, samplesPerSecond);

        playbackSpeed = defaultPlaybackSpeed;

        if (recordOnStart)
        {
            StartRecording();
        }
    }
    private void Update()
    {
        if (isRecording)
        {
            Record();
        }

        if (isReplaying && !isPaused)
        {
            Replay();
        }
    }

    // ---------------------------------------------------------
    // Recording
    // ---------------------------------------------------------

    public void StartRecording()
    {
        StopReplay();

        frames.Clear();

        recordingStartTime = Time.unscaledTime;
        nextSampleTime = recordingStartTime;

        isRecording = true;
    }

    public void StopRecording()
    {
        if (!isRecording)
            return;

        AddFrame(Time.unscaledTime - recordingStartTime);

        isRecording = false;
    }

    private void Record()
    {
        float currentTime = Time.unscaledTime - recordingStartTime;

        if (currentTime >= maxRecordingTime)
        {
            StopRecording();
            return;
        }

        if (Time.unscaledTime < nextSampleTime)
            return;

        AddFrame(currentTime);

        nextSampleTime += sampleInterval;
    }

    private void AddFrame(float time)
    {
        frames.Add(new ReplayFrame(
            transform.position,
            transform.rotation,
            time
        ));
    }

    // ---------------------------------------------------------
    // Replay
    // ---------------------------------------------------------

    [Button]
    public void StartReplay()
    {
        if (frames.Count < 2)
        {
            Debug.LogWarning("Not enough frames to replay.");
            return;
        }

        StopRecording();
        StopReplay();

        if (recoderPrefab == null)
        {
            Debug.LogError("Replay Recorder: No replay prefab assigned.");
            return;
        }
        Cursor.visible = true;
        replayObject = recoderPrefab.gameObject;
        //replayObject = Instantiate(
        //    recoderPrefab,
        //    frames[0].position,
        //    frames[0].rotation
        //);

        replayTime = 0f;
        replayFrameIndex = 0;
        playbackSpeed = defaultPlaybackSpeed;
        isPaused = false;
        isReplaying = true;

        ApplyReplayTime();
        
    }

    private void Replay()
    {
        if (replayObject == null)
        {
            StopReplay();
            return;
        }

        replayTime += Time.unscaledDeltaTime * playbackSpeed;

        float duration = GetReplayDuration();

        // Reached the end.
        if (replayTime >= duration)
        {
            replayTime = duration;
            ApplyReplayTime();

            if (loopReplay)
            {
                replayTime = 0f;
                replayFrameIndex = 0;
                ApplyReplayTime();
            }
            else
            {
                isPaused = true;
            }

            return;
        }

        // Reached the beginning while playing backwards.
        if (replayTime <= 0f)
        {
            replayTime = 0f;
            replayFrameIndex = 0;
            ApplyReplayTime();

            if (loopReplay)
            {
                replayTime = duration;
                replayFrameIndex = frames.Count - 2;
                ApplyReplayTime();
            }
            else
            {
                isPaused = true;
            }

            return;
        }
        ApplyReplayTime();
        UpdateTimelineSlider();
    }

    private void ApplyReplayTime()
    {
        if (replayObject == null || frames.Count < 2)
            return;

        float duration = GetReplayDuration();

        replayTime = Mathf.Clamp(
            replayTime,
            0f,
            duration
        );

        // Move frame index forward.
        while (
            replayFrameIndex < frames.Count - 2 &&
            frames[replayFrameIndex + 1].time <= replayTime
        )
        {
            replayFrameIndex++;
        }

        // Move frame index backward when scrubbing/reversing.
        while (
            replayFrameIndex > 0 &&
            frames[replayFrameIndex].time > replayTime
        )
        {
            replayFrameIndex--;
        }

        ReplayFrame from = frames[replayFrameIndex];
        ReplayFrame to = frames[replayFrameIndex + 1];

        float durationBetweenFrames = to.time - from.time;

        float t = durationBetweenFrames > 0f
            ? (replayTime - from.time) / durationBetweenFrames
            : 0f;

        Vector3 position = Vector3.Lerp(
            from.position,
            to.position,
            t
        );

        Quaternion rotation = Quaternion.Slerp(
            from.rotation,
            to.rotation,
            t
        );

        replayObject.transform.SetPositionAndRotation(
            position,
            rotation
        );
    }

    // ---------------------------------------------------------
    // Playback controls
    // ---------------------------------------------------------

    public void Play()
    {
        if (!isReplaying)
        {
            StartReplay();
            return;
        }

        isPaused = false;
    }

    public void Pause()
    {
        if (!isReplaying)
            return;

        isPaused = true;
    }

    public void TogglePause()
    {
        if (!isReplaying)
        {
            StartReplay();
            return;
        }

        isPaused = !isPaused;
    }

    public void Stop()
    {
        StopReplay();
    }

    public void StopReplay()
    {
        isReplaying = false;
        isPaused = false;

        if (replayObject != null)
        {
            Destroy(replayObject);
            replayObject = null;
        }
    }

    // ---------------------------------------------------------
    // Speed
    // ---------------------------------------------------------

    public void SetPlaybackSpeed(float speed)
    {
        playbackSpeed = speed;
    }

    public void IncreasePlaybackSpeed(float amount = 0.5f)
    {
        playbackSpeed += amount;
    }

    public void DecreasePlaybackSpeed(float amount = 0.5f)
    {
        playbackSpeed -= amount;
    }

    public void NormalSpeed()
    {
        playbackSpeed = 1f;
    }

    public void FastForward()
    {
        playbackSpeed = 4f;
    }

    public void SlowMotion()
    {
        playbackSpeed = 0.25f;
    }

    public void Reverse()
    {
        playbackSpeed = -Mathf.Abs(playbackSpeed);

        if (Mathf.Approximately(playbackSpeed, 0f))
        {
            playbackSpeed = -1f;
        }

        isPaused = false;
    }

    public void Forward()
    {
        playbackSpeed = Mathf.Abs(playbackSpeed);

        if (Mathf.Approximately(playbackSpeed, 0f))
        {
            playbackSpeed = 1f;
        }

        isPaused = false;
    }

    // ---------------------------------------------------------
    // Timeline / Scrubbing
    // ---------------------------------------------------------

    /// <summary>
    /// Set replay position from 0 to 1.
    /// Useful directly with a Unity UI Slider.
    /// </summary>
    public void SetTimelineNormalized(float value)
    {
        if (frames.Count < 2)
            return;

        if (!isReplaying)
        {
            StartReplay();
        }

        replayTime = Mathf.Clamp01(value) * GetReplayDuration();

        ApplyReplayTime();
    }

    /// <summary>
    /// Get replay position from 0 to 1.
    /// </summary>
    public float GetTimelineNormalized()
    {
        float duration = GetReplayDuration();

        if (duration <= 0f)
            return 0f;

        return Mathf.Clamp01(replayTime / duration);
    }

    /// <summary>
    /// Jump to a specific time in seconds.
    /// </summary>
    public void SetReplayTime(float time)
    {
        if (frames.Count < 2)
            return;

        if (!isReplaying)
        {
            StartReplay();
        }

        replayTime = Mathf.Clamp(
            time,
            0f,
            GetReplayDuration()
        );

        ApplyReplayTime();
    }

    public void SkipForward(float seconds)
    {
        SetReplayTime(replayTime + seconds);
    }

    public void SkipBackward(float seconds)
    {
        SetReplayTime(replayTime - seconds);
    }

    public void Rewind()
    {
        SetReplayTime(0f);
    }

    public void GoToEnd()
    {
        SetReplayTime(GetReplayDuration());
    }

    // ---------------------------------------------------------
    // Frame stepping
    // ---------------------------------------------------------

    public void NextFrame()
    {
        if (frames.Count < 2)
            return;

        replayFrameIndex = Mathf.Min(
            replayFrameIndex + 1,
            frames.Count - 2
        );

        replayTime = frames[replayFrameIndex].time;

        ApplyReplayTime();
    }

    public void PreviousFrame()
    {
        if (frames.Count < 2)
            return;

        replayFrameIndex = Mathf.Max(
            replayFrameIndex - 1,
            0
        );

        replayTime = frames[replayFrameIndex].time;

        ApplyReplayTime();
    }

    // ---------------------------------------------------------
    // Information
    // ---------------------------------------------------------

    public float GetReplayDuration()
    {
        if (frames.Count == 0)
            return 0f;

        return frames[frames.Count - 1].time;
    }

    public float GetReplayTime()
    {
        return replayTime;
    }

    public float GetPlaybackSpeed()
    {
        return playbackSpeed;
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public bool IsPlaying()
    {
        return isReplaying && !isPaused;
    }

    public bool HasRecording()
    {
        return frames.Count >= 2;
    }

    public int GetFrameCount()
    {
        return frames.Count;
    }

    public void SetLoop(bool enabled)
    {
        loopReplay = enabled;
    }

    public bool IsLooping()
    {
        return loopReplay;
    }

    // ---------------------------------------------------------
    // Recording state
    // ---------------------------------------------------------

    public bool IsRecording()
    {
        return isRecording;
    }

    public void ClearRecording()
    {
        StopRecording();
        StopReplay();

        frames.Clear();
    }

    private void UpdateTimelineSlider()
    {
        if (timelineSlider == null || isScrubbing)
            return;

        timelineSlider.SetValueWithoutNotify(GetTimelineNormalized());
    }

    public void BeginScrubbing()
    {
        isScrubbing = true;
    }

    public void EndScrubbing()
    {
        isScrubbing = false;
    }
}