using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector2 speed;

    [SerializeField]
    private float wallCollisionOffset;

    private Vector2 screenBounds;
    private float spriteHalfWidth;
    private Rigidbody2D rb;

    private float clampOffset = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed;
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,Camera.main.transform.position.z));
        spriteHalfWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = speed;
    }

    void FixedUpdate()
    {
        ClampBallToScreen();
    }

    private void BounceX()
    {
        speed.x = -speed.x;
    }

    private void BounceY()
    {
        speed.y = -speed.y;
    }

    void OnCollisionEnter2D(Collision2D collisionInfo)
    {   
        if(collisionInfo.gameObject.tag == "Player")
        {
            PaddleMovement pm = collisionInfo.gameObject.GetComponent<PaddleMovement>();
            if(transform.position.x < pm.GetSpriteLeft() || transform.position.x > pm.GetSpriteRight())
            {
                BounceX();
            }
            else BounceY();
        }
    }

    void ClampBallToScreen()
    {
        Vector2 currentPos = rb.position;
        
        if(currentPos.x < screenBounds.x + spriteHalfWidth + wallCollisionOffset ||
            currentPos.x > (screenBounds.x * -1) - spriteHalfWidth - wallCollisionOffset)
        {
            currentPos.x = Mathf.Clamp(currentPos.x,screenBounds.x + spriteHalfWidth + wallCollisionOffset + clampOffset,(screenBounds.x * -1)  - spriteHalfWidth - wallCollisionOffset - clampOffset);
            transform.position = currentPos;
            BounceX();
        }   
    }
}
