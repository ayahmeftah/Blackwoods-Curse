using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectibleManager : MonoBehaviour
{
    public static CollectibleManager Instance;

    public TMP_Text scoreText;

    // Assign in Inspector
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); 
        }
        else
            Destroy(gameObject);
    }


    void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = score + "/10";
    }

    public int GetCollectedScore()
    {
        return score;
    }

}
