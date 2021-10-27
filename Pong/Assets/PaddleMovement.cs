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

    [SerializeField]
    private float wallCollisionOffset;

    private Rigidbody2D rb;

    private Vector2 screenBounds;
    private float spriteHalfWidth;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        paddleCurrentMoveSpeed = paddleStartMoveSpeed;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
        spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {     
        CheckForMovement();
        ClampPaddleToScreen();
    }

    void CheckForMovement()
    {
        float direction = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + new Vector2((paddleCurrentMoveSpeed * direction * Time.deltaTime),0.0f));

        if(paddleCurrentMoveSpeed <= maxPaddleSpeed)   paddleCurrentMoveSpeed += paddleSpeedAcceleration * Time.deltaTime;
        else    paddleCurrentMoveSpeed = maxPaddleSpeed;

        if(Mathf.Abs(direction) <= 0.1f)    paddleCurrentMoveSpeed = paddleStartMoveSpeed;

    }

    void ClampPaddleToScreen()
    {
        Vector2 currentPos = rb.position;
        currentPos.x = Mathf.Clamp(currentPos.x,screenBounds.x + spriteHalfWidth + wallCollisionOffset,(screenBounds.x * -1)  - spriteHalfWidth - wallCollisionOffset);
        transform.position = currentPos;
    }
}
