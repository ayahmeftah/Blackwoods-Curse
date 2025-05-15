using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineCutsceneControl : MonoBehaviour
{
    public PlayableDirector director;
    public string nextScene = ""; // Replace with your actual next scene

    void Start()
    {
        director.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        if (SceneLoader.Instance != null)
        {
            SceneLoader.Instance.LoadSceneWithFade(nextScene);
        }
        else
        {
            Debug.LogWarning("SceneLoader not found — loading scene directly.");
            SceneManager.LoadScene(nextScene);
        }
    }
}
