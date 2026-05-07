using UnityEngine;
using UnityEngine.UIElements;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public Vector2 coinPosition;

    public float spawnProgress;
    public float spawnDuration;

    void Start()
    {
        
    }

    void Update()
    {
        coinPosition = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        Vector2 randomPosition = Camera.main.ScreenToWorldPoint(coinPosition);

        spawnProgress += Time.deltaTime;

        if (spawnProgress > spawnDuration)
        { 
            GameObject spawnedCoin = Instantiate(coinPrefab, randomPosition, Quaternion.identity);
            spawnProgress = 0f;
        }
    }
}
