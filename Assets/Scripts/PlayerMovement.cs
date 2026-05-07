using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum MovementState
    {
        Vertical,
        Horizontal,
        Idle,
        Dash,
        Click,
    }

    public MovementState currentState;

    public Vector2 directionalInput;
    private Vector2 position;
    public Vector2 lastMoveDirection;

    public float moveSpeed;

    public Coroutine dashCoroutine;
    public float dashProgress;
    public float dashDuration;
    public float dashSpeed;
    public Vector2 dashDirection;

    public Coroutine clickCoroutine;
    public Vector2 mousePosition;

    public ScoreManager scoreManagerScript;

    void Start()
    {
        scoreManagerScript = GetComponent<ScoreManager>();
    }

    void Update()
    {
        position = transform.position;

        switch (currentState)
        {
            case MovementState.Vertical:
                position.y += moveSpeed * Time.deltaTime * Mathf.Sign(directionalInput.y);
                break;
            case MovementState.Horizontal:
                position.x += moveSpeed * Time.deltaTime * Mathf.Sign(directionalInput.x);
                break;
            case MovementState.Idle:
                break;
        }

        transform.position = position;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (currentState == MovementState.Dash)
        {
            return;
        }

        directionalInput = context.ReadValue<Vector2>();

        if (directionalInput.x != 0)
        {
            currentState = MovementState.Horizontal;
            lastMoveDirection = new Vector2(Mathf.Sign(directionalInput.x), 0);
        }
        else if (directionalInput.y != 0)
        {
            currentState = MovementState.Vertical;
            lastMoveDirection = new Vector2(0, Mathf.Sign(directionalInput.y));
        }
        else
        {
            currentState = MovementState.Idle;
        }
    }

    public void OnDash()
    {
        dashProgress = 0f;

        dashDirection = lastMoveDirection;

        currentState = MovementState.Dash;

        dashCoroutine = StartCoroutine(DashUpdate());
    }

    public IEnumerator DashUpdate()
    {
        while (dashProgress < dashDuration)
        {
            dashProgress += Time.deltaTime;

            position += Time.deltaTime * dashSpeed * dashDirection;

            transform.position = position;
            
            yield return null;
        }

        currentState = MovementState.Idle;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        //Debug.Log(context);

        

        if (context.canceled)
        {
            currentState = MovementState.Click;

            if (clickCoroutine != null)
            {
                StopCoroutine(clickCoroutine);
            }

            clickCoroutine = StartCoroutine(ClickUpdate());                       
        }
    }

    public IEnumerator ClickUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        float distance = Vector2.Distance(mousePosition, position);

        while (distance > 1)
        {
            position = transform.position;

            Vector2 direction = (mousePosition - position).normalized;
            
            position += Time.deltaTime * moveSpeed * direction;
            
            transform.position = position;

            distance = Vector2.Distance(mousePosition, position);

            yield return null;
        }

        clickCoroutine = null;
        currentState = MovementState.Idle;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            Destroy(collision.gameObject);
            scoreManagerScript.score ++;
        }
    }
}
