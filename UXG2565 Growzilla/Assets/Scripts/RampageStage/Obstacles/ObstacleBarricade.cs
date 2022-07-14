using UnityEngine;

public class ObstacleBarricade : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    Rigidbody2D rb2d = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GetComponent<Animator>().Play("BarricadeIdle");
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
                Debug.Log("Kaijuu Receives Damage from Barricade!");
                GetComponent<Animator>().Play("BarricadeTrigger");
                GetComponent<Collider2D>().enabled = false;
                Invoke("OnDestroyed", 1.1f);
                break;
            case "ability_horns_hitbox":
                // Add Destruction score.
                Debug.Log("Kaijuu Used Skill to Destroy Barricade!");
                GetComponent<Animator>().Play("BarricadeTrigger");
                GetComponent<Collider2D>().enabled = false;
                Invoke("OnDestroyed", 1.1f);
                break;
            case "SceneBorders":
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    void OnDestroyed()
    {
        Destroy(this.gameObject);
    }
}
