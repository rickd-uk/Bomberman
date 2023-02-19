
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public AnimatedSpriteRenderer Start;
    public AnimatedSpriteRenderer Middle;
    public AnimatedSpriteRenderer End;

    public void SetActiveRenderer(AnimatedSpriteRenderer Renderer)
    {
        Start.enabled = Renderer == Start;
        Middle.enabled = Renderer == Middle;
        End.enabled = Renderer == End;
    }

    public void SetDirection(Vector2 direction)
    {
        // Rotate explosion to face the correct direction
        float angle = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
    }

    public void DestroyAfter(float seconds)
    {
        Destroy(gameObject, seconds);
    }
}
