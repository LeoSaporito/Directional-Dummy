using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public enum EnemyMovementState
    { 
        EnemyIdle,
        EnemyMoving,
    }

    public PlayerMovement playerMovementScript;

    public Vector2 enemyCurrentPosition;
    public float enemySpeed;

    public float followPlayer;

    public Vector2 direction;

    void Start()
    {
        playerMovementScript = FindFirstObjectByType<PlayerMovement>();
    }

    void Update()
    {
        enemyCurrentPosition = transform.position;

        Vector2 playerPosition = playerMovementScript.transform.position;

        float distance = Vector2.Distance(enemyCurrentPosition, playerPosition);

        if (distance < followPlayer)
        {
            direction = (playerPosition - enemyCurrentPosition).normalized;

            transform.position += (Vector3)(direction * enemySpeed * Time.deltaTime);
        }

    }
}
