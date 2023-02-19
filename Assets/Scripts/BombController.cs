using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class BombController : MonoBehaviour
{
    public GameObject BombPrefab;
    public KeyCode LayBomb = KeyCode.Space;

    [Header("Bomb")]
    public float bombFuseTime = 3.0f;
    public int bombAmount = 1;
    private int bombsRemaining;

    [Header("Explosion")]
    public Explosion ExplosionPrefab;
    public float explosionDuration = 1.0f;
    public int explosionRadius = 1;
    public LayerMask ExplosionLayerMask;

    [Header("Desructible")]
    public Tilemap DestructibleTiles;
    public Destructible DestructiblePrefab;

    private void OnEnable()
    {
        bombsRemaining = bombAmount;
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(LayBomb))
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private Vector2 RoundXYPos(Vector2 position)
    {
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        return position;
    }

    private IEnumerator PlaceBomb()
    {
        Vector2 position = transform.position;
        RoundXYPos(position);

        GameObject Bomb = Instantiate(BombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombFuseTime);

        // new position of bomb; it may have been pushed by player
        position = Bomb.transform.position;
        RoundXYPos(position);

        Explosion Explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        Explosion.SetActiveRenderer(Explosion.Start);
        Explosion.DestroyAfter(explosionDuration);
        
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);


        Destroy(Bomb);
        bombsRemaining++;
    }


    // Change bomb to be solid object
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) return;
       
        position += direction;

        // Divided by 2 (not a precise calculation) to guarantee it is within one tile
        // Angle is not important;  Need Layer Mask because we want explosion to stop only
        // with stage elements (i.e. blocks)
        // Return (don't continue the explosion in that direction) because it is hindered
        if (Physics2D.OverlapBox(position, Vector2.one / 2.0f, 0.0f, ExplosionLayerMask))
        {

            ClearDesructible(position);

            return;
        }

        Debug.Log(length);

        Explosion Explosion = Instantiate(ExplosionPrefab, position, Quaternion.identity);
        Explosion.SetActiveRenderer(length > 1 ? Explosion.Middle : Explosion.End);
        Explosion.SetDirection(direction);
        Explosion.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }

    private void ClearDesructible(Vector2 position)
    {
        Vector3Int Cell = DestructibleTiles.WorldToCell(position);
        TileBase Tile = DestructibleTiles.GetTile(Cell);

        if (Tile != null)
        {
            Instantiate(DestructiblePrefab, position, Quaternion.identity);
            DestructibleTiles.SetTile(Cell, null);
        }
    }
}
 