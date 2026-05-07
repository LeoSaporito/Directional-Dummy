using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score;
    public TextMeshProUGUI scoreText;
    
    void Start()
    {
        
    }

    void Update()
    {
        scoreText.text = "Score: " + score;
    }
}
