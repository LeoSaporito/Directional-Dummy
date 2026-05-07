using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        { 
            Vector2 randomEnemyPosition = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));

            Vector2 randomEnemyPositionWorld = Camera.main.ScreenToWorldPoint(randomEnemyPosition);

            Debug.Log(randomEnemyPositionWorld);

            Instantiate(enemyPrefab, randomEnemyPositionWorld, Quaternion.identity);
        }
    }
}