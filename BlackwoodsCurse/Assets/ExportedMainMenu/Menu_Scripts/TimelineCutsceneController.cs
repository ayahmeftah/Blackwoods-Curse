using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class TimelineCutsceneController : MonoBehaviour
{
    public PlayableDirector director;
    public string nextScene = "Main_Scene"; // change to your actual scene name

    void Start()
    {
        director.stopped += OnTimelineFinished;
    }

    void OnTimelineFinished(PlayableDirector pd)
    {
        SceneManager.LoadScene(nextScene);
    }
}
