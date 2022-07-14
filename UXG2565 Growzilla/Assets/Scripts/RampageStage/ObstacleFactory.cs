using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] Vector2 MinimumCooldown = new Vector2(1.0f, 2.0f);
    float obstacleMinimumCooldown = 0.0f;

    [SerializeField] Transform Obstacle1SpawnPosition = null;
    [SerializeField] Vector2 Obstacle1Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle1Prefab = null;
    float obstacle1CooldownTimer = 0.0f;

    [SerializeField] Transform Obstacle2SpawnPosition = null;
    [SerializeField] Vector2 Obstacle2Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle2Prefab = null;
    float obstacle2CooldownTimer = 0.0f;

    [SerializeField] Transform Obstacle3SpawnPosition = null;
    [SerializeField] Vector2 Obstacle3Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle3Prefab = null;
    float obstacle3CooldownTimer = 0.0f;

    [SerializeField] Transform Obstacle4SpawnPosition = null;
    [SerializeField] Vector2 Obstacle4Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle4Prefab = null;
    float obstacle4CooldownTimer = 0.0f;

    void Start()
    {
        obstacle1CooldownTimer = Random.Range(Obstacle1Cooldown.x, Obstacle1Cooldown.y);
        obstacle2CooldownTimer = Random.Range(Obstacle2Cooldown.x, Obstacle2Cooldown.y);
        obstacle3CooldownTimer = Random.Range(Obstacle3Cooldown.x, Obstacle3Cooldown.y);
        obstacle4CooldownTimer = Random.Range(Obstacle4Cooldown.x, Obstacle4Cooldown.y);
    }
    void Update()
    {
        obstacleMinimumCooldown -= Time.deltaTime;
        Obstacle1Tick();
        Obstacle2Tick();
        Obstacle3Tick();
        Obstacle4Tick();
    }
    void Obstacle1Tick()
    {
        if (obstacleMinimumCooldown > 0.0f) { return; }

        if (Obstacle1Prefab == null) { return; }
        if (obstacle1CooldownTimer > 0.0f) { obstacle1CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle1Prefab, Obstacle1SpawnPosition.position, Quaternion.identity);

        obstacle1CooldownTimer = Random.Range(Obstacle1Cooldown.x, Obstacle1Cooldown.y);

        obstacleMinimumCooldown = Random.Range(MinimumCooldown.x, MinimumCooldown.y);
    }
    void Obstacle2Tick()
    {
        if (obstacleMinimumCooldown > 0.0f) { return; }

        if (Obstacle2Prefab == null) { return; }
        if (obstacle2CooldownTimer > 0.0f) { obstacle2CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle2Prefab, Obstacle2SpawnPosition.position, Quaternion.identity);

        obstacle2CooldownTimer = Random.Range(Obstacle2Cooldown.x, Obstacle2Cooldown.y);

        obstacleMinimumCooldown = Random.Range(MinimumCooldown.x, MinimumCooldown.y);
    }

    void Obstacle3Tick()
    {
        if (obstacleMinimumCooldown > 0.0f) { return; }

        if (Obstacle3Prefab == null) { return; }
        if (obstacle3CooldownTimer > 0.0f) { obstacle3CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle3Prefab, Obstacle3SpawnPosition.position, Quaternion.identity);

        obstacle3CooldownTimer = Random.Range(Obstacle3Cooldown.x, Obstacle3Cooldown.y);

        obstacleMinimumCooldown = Random.Range(MinimumCooldown.x, MinimumCooldown.y);
    }

    void Obstacle4Tick()
    {
        if (obstacleMinimumCooldown > 0.0f) { return; }

        if (Obstacle4Prefab == null) { return; }
        if (obstacle4CooldownTimer > 0.0f) { obstacle4CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle4Prefab, Obstacle4SpawnPosition.position, Quaternion.identity);

        obstacle4CooldownTimer = Random.Range(Obstacle4Cooldown.x, Obstacle4Cooldown.y);

        obstacleMinimumCooldown = Random.Range(MinimumCooldown.x, MinimumCooldown.y);
    }
}
