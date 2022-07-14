using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] float MinimumCooldown = 1.0f;
    float obstacleMinimumCooldown = 0.0f;

    [SerializeField] Transform Obstacle1SpawnPosition = null;
    [SerializeField] Vector2 Obstacle1Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle1Prefab = null;
    float obstacle1CooldownTimer = 0.0f;

    [SerializeField] Transform Obstacle2SpawnPosition = null;
    [SerializeField] Vector2 Obstacle2Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle2Prefab = null;
    float obstacle2CooldownTimer = 0.0f;

    void Start()
    {
        obstacle1CooldownTimer = Random.Range(Obstacle1Cooldown.x, Obstacle1Cooldown.y);
        obstacle2CooldownTimer = Random.Range(Obstacle2Cooldown.x, Obstacle2Cooldown.y);
    }
    void Update()
    {
        obstacleMinimumCooldown -= Time.deltaTime;
        Obstacle1Tick();
        Obstacle2Tick();
    }
    void Obstacle1Tick()
    {
        if (obstacleMinimumCooldown > 0.0f) { return; }

        if (Obstacle1Prefab == null) { return; }
        if (obstacle1CooldownTimer > 0.0f) { obstacle1CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle1Prefab, Obstacle1SpawnPosition.position, Quaternion.identity);

        obstacle1CooldownTimer = Random.Range(Obstacle1Cooldown.x, Obstacle1Cooldown.y);

        obstacleMinimumCooldown = MinimumCooldown;
    }
    void Obstacle2Tick()
    {
        if (obstacleMinimumCooldown > 0.0f) { return; }

        if (Obstacle2Prefab == null) { return; }
        if (obstacle2CooldownTimer > 0.0f) { obstacle2CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle2Prefab, Obstacle2SpawnPosition.position, Quaternion.identity);

        obstacle2CooldownTimer = Random.Range(Obstacle2Cooldown.x, Obstacle2Cooldown.y);

        obstacleMinimumCooldown = MinimumCooldown;
    }
}
