using UnityEngine;

public class ObstacleBarricade : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] GameObject ExplosionPrefab = null;
    [SerializeField] int DestructionScore = 50;
    Rigidbody2D rb2d = null;

    public AudioSource audioSource;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GameObject.Find("Explosion").GetComponent<AudioSource>();
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
                ScreenShakeController.cam_instance.startShake(0.05f, 0.05f);
                RampageDestructionHandler.Instance.LoseHealth();
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Receives Damage from Barricade!");
                GetComponent<Animator>().Play("BarricadeTrigger");
                GetComponent<Collider2D>().enabled = false;
                Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                audioSource.Play();
                Destroy(this.gameObject);
                //Invoke("OnDestroyed", 1.1f);
                break;
            case "ability_horns_hitbox":
                ScreenShakeController.cam_instance.startShake(0.05f, 0.05f);
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Used Skill to Destroy Barricade!");
                GetComponent<Animator>().Play("BarricadeTrigger");
                GetComponent<Collider2D>().enabled = false;
                Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                audioSource.Play();
                Destroy(this.gameObject);
                //Invoke("OnDestroyed", 1.1f);
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
