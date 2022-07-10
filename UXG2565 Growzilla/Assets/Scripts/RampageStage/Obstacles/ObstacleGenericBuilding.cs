using UnityEngine;

public class ObstacleGenericBuilding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    Rigidbody2D rb2d = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveSpeed * -Vector2.right;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "BigKaijuu":
            case "MidKaijuu":
            case "SmallKaijuu":
                // Deal Damage to Kaijuu Here.
                // Add Destruction Score
                Debug.Log("Kaijuu Receives Damage!");
                Destroy(this.gameObject);
                break;
            case "ability_horns_hitbox":
            case "ability_tail_hitbox":
            case "ability_wings_hitbox":
                // Add Destruction score.
                Debug.Log("Kaijuu Used Skill to Destroy Buildings!");
                Destroy(this.gameObject);
                break;
            case "SceneBorders":
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}
