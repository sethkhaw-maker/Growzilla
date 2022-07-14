using UnityEngine;

public class ObstacleGenericBuilding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] GameObject ExplosionPrefab = null;
    Rigidbody2D rb2d = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();    
    }

    void Start()
    {
        GetComponent<Animator>().Play("Building1Idle");
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
                Debug.Log("Kaijuu Receives Damage from Buildings!");
                GetComponent<Animator>().Play("Building1Destroyed");
                GetComponent<Collider2D>().enabled = false;
                var temp = Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                temp.transform.localScale = new Vector3(1, 1, 1);
                Invoke("OnDestroyed", 1.0f);
                break;
            case "ability_horns_hitbox":
            case "ability_tail_hitbox":
            case "ability_wings_hitbox":
                // Add Destruction score.
                Debug.Log("Kaijuu Used Skill to Destroy Buildings!");
                GetComponent<Animator>().Play("Building1Destroyed");
                GetComponent<Collider2D>().enabled = false;
                var temp2 = Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                temp2.transform.localScale = new Vector3(1, 1, 1);
                Invoke("OnDestroyed", 1.0f);
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
