using UnityEngine;

public class ScrollingObjectsHandler : MonoBehaviour
{
    [SerializeField] private float ScrollSpeed = 1.0f;

    Rigidbody2D rb2d;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb2d.velocity = ScrollSpeed * -Vector2.right * RampageBuildingSpawnGrowth.Instance.SpawnMultiplier;
    }
}
