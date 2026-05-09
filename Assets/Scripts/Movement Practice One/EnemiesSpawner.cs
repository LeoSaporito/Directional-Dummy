using NUnit.Framework.Constraints;
using UnityEngine;

public class EnemiesSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public Vector2 enemyPosition;

    void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            enemyPosition = Camera.main.ScreenToWorldPoint(new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height)));

            Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
        }
    }

    void Update()
    {
        
    }
}
