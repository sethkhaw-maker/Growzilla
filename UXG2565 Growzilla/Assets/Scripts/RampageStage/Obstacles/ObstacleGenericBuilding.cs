using UnityEngine;

public class ObstacleGenericBuilding : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] GameObject ExplosionPrefab = null;
    [SerializeField] int DestructionScore = 25;
    Rigidbody2D rb2d = null;

    public AudioSource audioSource;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GameObject.Find("Explosion").GetComponent<AudioSource>();
    }

    void Start()
    {
        GetComponent<Animator>().Play("Building1Idle");
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveSpeed * -Vector2.right * RampageBuildingSpawnGrowth.Instance.SpawnMultiplier;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "BigKaijuu":
            case "MidKaijuu":
            case "SmallKaijuu":
                ScreenShakeController.cam_instance.startShake(0.05f, 0.05f);
                RampageDestructionHandler.Instance.LoseHealth();
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Receives Damage from Buildings!");
                GetComponent<Animator>().Play("Building1Destroyed");
                GetComponent<Collider2D>().enabled = false;
                var temp = Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                temp.transform.localScale = new Vector3(1, 1, 1);
                audioSource.Play();
                Invoke("OnDestroyed", 1.0f);
                break;
            case "ability_horns_hitbox":
            case "ability_tail_hitbox":
            case "ability_wings_hitbox":
                ScreenShakeController.cam_instance.startShake(0.05f, 0.05f);
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Used Skill to Destroy Buildings!");
                GetComponent<Animator>().Play("Building1Destroyed");
                GetComponent<Collider2D>().enabled = false;
                var temp2 = Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                temp2.transform.localScale = new Vector3(1, 1, 1);
                audioSource.Play();
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
