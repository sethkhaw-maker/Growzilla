using UnityEngine;

public class ObstacleHelicopter : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] GameObject ExplosionPrefab = null;
    [SerializeField] GameObject AnimatorObject = null;
    [SerializeField] int DestructionScore = 50;
    Rigidbody2D rb2d = null;

    public AudioSource audioSource1;
    public AudioSource audioSource2;
    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        audioSource1 = GameObject.Find("Explosion").GetComponent<AudioSource>();
        audioSource2 = GameObject.Find("HelicopterMoving").GetComponent<AudioSource>();
    }

    void Start()
    {
        audioSource2.Play();
        AnimatorObject.GetComponent<Animator>().Play("HelicopterMoving");
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
                // Deal Damage to Kaijuu Here.
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Destroys helicopter!");
                AnimatorObject.GetComponent<Animator>().Play("HelicopterDestroyed");
                GetComponent<Collider2D>().enabled = false;
                var temp = Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                //Invoke("OnDestroyed", 1.0f);
                StopAudio();
                Destroy(this.gameObject);
                break;
            case "SceneBorders":
                StopAudio();
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }

    void StopAudio()
    {
        audioSource1.Play();
        audioSource2.Stop();
    }

    void OnDestroyed()
    {
        Destroy(this.gameObject);
    }
}
