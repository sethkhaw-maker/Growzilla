using UnityEngine;

public class LoopingGroundHandler : MonoBehaviour
{
    BoxCollider2D GroundCollider = null;
    private float groundLength = 0.0f;

    void Awake()
    {
        GroundCollider = GetComponent<BoxCollider2D>();
        groundLength = GroundCollider.size.x;
    }

    void Update()
    {
        if (transform.position.x < -groundLength)
        {
            Loop();
        }
    }
    void Loop()
    {
        Vector2 groundOffset = new Vector2(groundLength * 2f, 0.0f);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}
