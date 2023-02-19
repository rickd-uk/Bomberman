
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public float destructionTime = 1.0f;

    [Range(0, 1)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] SpawnableItems;

    private void Start()
    {
        Destroy(gameObject, destructionTime);
    }

    private void OnDestroy()
    {
        if (SpawnableItems.Length > 0 && Random.value < itemSpawnChance)
        {
            int randomIndex = Random.Range(0, SpawnableItems.Length);
            Instantiate(SpawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}
