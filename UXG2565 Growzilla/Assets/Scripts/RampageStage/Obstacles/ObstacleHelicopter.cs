using UnityEngine;

public class ObstacleHelicopter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] GameObject ExplosionPrefab = null;
    [SerializeField] GameObject AnimatorObject = null;
    [SerializeField] int DestructionScore = 50;
    Rigidbody2D rb2d = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        AnimatorObject.GetComponent<Animator>().Play("HelicopterMoving");
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
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Destroys helicopter!");
                AnimatorObject.GetComponent<Animator>().Play("HelicopterDestroyed");
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
