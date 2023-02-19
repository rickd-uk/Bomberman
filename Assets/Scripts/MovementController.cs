using UnityEngine;

public class MovementController : MonoBehaviour
{
   public Rigidbody2D Rigidbody { get; private set; }

    private Vector2 direction = Vector2.down;
    public float speed = 5.0f;

    public KeyCode inputUp = KeyCode.W;
    public KeyCode inputDown = KeyCode.S;
    public KeyCode inputLeft = KeyCode.A;
    public KeyCode inputRight = KeyCode.D;

    public AnimatedSpriteRenderer SpriteRendererUp;
    public AnimatedSpriteRenderer SpriteRendererDown;
    public AnimatedSpriteRenderer SpriteRendererLeft;
    public AnimatedSpriteRenderer SpriteRendererRight;
    private AnimatedSpriteRenderer ActiveSpriteRenderer;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        ActiveSpriteRenderer = SpriteRendererDown;
    }

    private void HandleInput()
    {
        if (Input.GetKey(inputUp))
        {
            SetDirection(Vector2.up, SpriteRendererUp);
        }
        else if (Input.GetKey(inputDown))
        {
            SetDirection(Vector2.down, SpriteRendererDown);
        }
        else if (Input.GetKey(inputLeft))
        {
            SetDirection(Vector2.left, SpriteRendererLeft);
        }
        else if (Input.GetKey(inputRight))
        {
            SetDirection(Vector2.right, SpriteRendererRight);
        }
        else
        {
            SetDirection(Vector2.zero, ActiveSpriteRenderer);
        }
    }

    private void Update()
    {
        HandleInput();
     }

    private void FixedUpdate()
    {
        Vector2 position = Rigidbody.position;
        Vector2 translation = speed * Time.fixedDeltaTime * direction;

        Rigidbody.MovePosition(position + translation);
    }

    private void SetDirection(Vector2 newDir, AnimatedSpriteRenderer SpriteRenderer)
    {
        direction = newDir;

        SpriteRendererUp.enabled = SpriteRenderer == SpriteRendererUp;
        SpriteRendererDown.enabled = SpriteRenderer == SpriteRendererDown;
        SpriteRendererLeft.enabled = SpriteRenderer == SpriteRendererLeft;
        SpriteRendererRight.enabled = SpriteRenderer == SpriteRendererRight;

        ActiveSpriteRenderer = SpriteRenderer;
        ActiveSpriteRenderer.idle = direction == Vector2.zero;
    }
}
