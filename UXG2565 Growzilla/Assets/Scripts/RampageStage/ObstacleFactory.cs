using UnityEngine;

public class ObstacleFactory : MonoBehaviour
{
    [SerializeField] Transform Obstacle1SpawnPosition = null;
    [SerializeField] Vector2 Obstacle1Cooldown = new Vector2(1.0f, 2.0f);
    [SerializeField] GameObject Obstacle1Prefab = null;
    float obstacle1CooldownTimer = 0.0f;

    void Start()
    {
        obstacle1CooldownTimer = Random.Range(Obstacle1Cooldown.x, Obstacle1Cooldown.y);
    }
    void Update()
    {
        Obstacle1Tick();    
    }
    void Obstacle1Tick()
    {
        if (Obstacle1Prefab == null) { return; }
        if (obstacle1CooldownTimer > 0.0f) { obstacle1CooldownTimer -= Time.deltaTime; return; }

        Instantiate(Obstacle1Prefab, Obstacle1SpawnPosition.position, Quaternion.identity);

        obstacle1CooldownTimer = Random.Range(Obstacle1Cooldown.x, Obstacle1Cooldown.y);
    }
}
