using UnityEngine;

public class ObstacleHelicopter : MonoBehaviour
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
        GetComponent<Animator>().Play("HelicopterMoving");
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
                Debug.Log("Kaijuu Destroys helicopter!");
                GetComponent<Animator>().Play("HelicopterDestroyed");
                GetComponent<Collider2D>().enabled = false;
                var temp = Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                //Invoke("OnDestroyed", 1.0f);
                Destroy(this.gameObject);
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
