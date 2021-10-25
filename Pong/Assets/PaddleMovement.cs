using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    [SerializeField]
    private float paddleCurrentMoveSpeed;

    [SerializeField]
    private float paddleStartMoveSpeed;

    [SerializeField]
    private float paddleSpeedAcceleration;

    [SerializeField]
    private float maxPaddleSpeed;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleCurrentMoveSpeed = paddleStartMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMovement();
    }

    void CheckForMovement()
    {
        float direction = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + new Vector2((paddleCurrentMoveSpeed * direction * Time.deltaTime),0.0f));

        if(paddleCurrentMoveSpeed <= maxPaddleSpeed)   paddleCurrentMoveSpeed += paddleSpeedAcceleration * Time.deltaTime;
        else    paddleCurrentMoveSpeed = maxPaddleSpeed;

        if(Mathf.Abs(direction) <= 0.1f)    paddleCurrentMoveSpeed = paddleStartMoveSpeed;

    }
}
