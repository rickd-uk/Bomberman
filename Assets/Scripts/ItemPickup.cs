
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType
    {
        ExtraBomb, 
        BlastRadius,
        SpeedIncrease
    }

    public ItemType Type;

    private void OnItemPickup(GameObject Player)
    {
        switch (Type)
        {
            case ItemType.ExtraBomb:
                Player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.BlastRadius:
                Player.GetComponent<BombController>().explosionRadius++;
                break;
            case ItemType.SpeedIncrease:
                Player.GetComponent<MovementController>().speed++;
                break;
            default:
                // No known type
                break;
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D Other)
    {
        
        if (Other.CompareTag("Player"))
        {
            OnItemPickup(Other.gameObject);
        }
    }
}
