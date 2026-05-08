using UnityEngine;
using TMPro;

public class ScoreManagerOne : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ScoreManager()
    {
        score++;
        scoreText.text = "Score: " + score;
    }
}
