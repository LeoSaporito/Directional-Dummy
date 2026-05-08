using UnityEngine;

public class CoinSpawnerOne : MonoBehaviour
{
    public GameObject coinPrefab;

    public float progress;
    public float duration;

    public Vector2 coinPosition;
    public Vector2 randomPosition;
    void Start()
    {
        
    }

    void Update()
    {
        progress += Time.deltaTime;

        if (progress > duration)
        {
            randomPosition = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));

            coinPosition = Camera.main.ScreenToWorldPoint(randomPosition);
            
            Instantiate(coinPrefab, coinPosition, Quaternion.identity);

            progress = 0f;
        }
    }    
}
