using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovementOne : MonoBehaviour
{
    public float enemyMoveSpeed;
    public Vector2 position;
    public Vector2 direction;
    public float distance;

    public PlayerMovementOne playerMovementScript;
    void Start()
    {
        playerMovementScript = FindFirstObjectByType<PlayerMovementOne>();
    }

    void Update()
    {
        position = transform.position;

        distance = Vector2.Distance(playerMovementScript.transform.position, transform.position);

        if (distance < 5)
        { 
            direction = (playerMovementScript.transform.position - transform.position).normalized;

            position += enemyMoveSpeed * direction * Time.deltaTime;
        }

        transform.position = position;
    }
}
