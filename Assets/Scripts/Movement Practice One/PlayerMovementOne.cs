using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerMovementOne : MonoBehaviour
{
    public enum MovementState
    { 
        Idle,
        Vertical,
        Horizontal,
        Dash,
        Click,
    }

    public MovementState currentState;

    public float moveSpeed;
    private Vector2 position;
    public Vector2 directionalInput;
    public Vector2 lastMoveDirection;

    public Coroutine dashCoroutine;
    public float dashSpeed;
    public float dashProgress;
    public float dashDuration;

    public Coroutine clickCoroutine;
    public Vector2 mousePosition;
    public Vector2 direction;
    private Vector2 newPosition;
    public float distance;

    public ScoreManagerOne scoreManagerScript;
    void Start()
    {
        
    }

    void Update()
    {       
        position = transform.position;

        switch (currentState)
        {            
            case MovementState.Horizontal:
                position.x += Time.deltaTime * moveSpeed * Mathf.Sign(directionalInput.x);
                lastMoveDirection = directionalInput;
                break;
            case MovementState.Vertical:
                position.y += Time.deltaTime * moveSpeed * Mathf.Sign(directionalInput.y);
                lastMoveDirection = directionalInput;
                break;
            case MovementState.Idle:
                break;
        }

        transform.position = position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (currentState == MovementState.Dash || currentState == MovementState.Click)
        {
            return;
        }
        else 
        {
            directionalInput = context.ReadValue<Vector2>();

            if (directionalInput.x != 0)
            {
                currentState = MovementState.Horizontal;
            }
            else if (directionalInput.y != 0)
            {
                currentState = MovementState.Vertical;
            }
            else 
            {
                currentState = MovementState.Idle;
            }
        }
    }

    public void OnDash()
    {
        if (dashCoroutine != null)
        {
            return;
        }

        dashProgress = 0f;

        currentState = MovementState.Dash;

        dashCoroutine = StartCoroutine(DashUpdate());
    }

    public IEnumerator DashUpdate()
    {
        while (dashProgress < dashDuration)
        {
            dashProgress += Time.deltaTime;

            position += Time.deltaTime * lastMoveDirection * dashSpeed;

            transform.position = position;

            yield return null;
        }

        currentState = MovementState.Idle;
        dashCoroutine = null;
    }

    public void OnClickToMove(InputAction.CallbackContext context)
    {
        if (context.canceled)
        { 
            if (clickCoroutine != null)
            {
                StopCoroutine(clickCoroutine);
            }

            currentState = MovementState.Click;

            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            clickCoroutine = StartCoroutine(ClickUpdate());           
        }
    }

    public IEnumerator ClickUpdate()
    {
        while (Vector2.Distance(mousePosition, transform.position) > 0.01f)
        {
            direction = (mousePosition - (Vector2)(transform.position)).normalized;
        
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);

            yield return null;
        }

        currentState = MovementState.Idle;
        clickCoroutine = null;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);

            scoreManagerScript.ScoreManager();
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
        }
    }
}
