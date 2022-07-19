using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampageBuildingSpawnGrowth : MonoBehaviour
{
    public static RampageBuildingSpawnGrowth Instance { get; private set; }

    public float SpawnMultiplier = 1.0f;
    [SerializeField] float InitialSpawnMultiplier = 0.1f;
    [SerializeField] float TimeGrowth = 10.0f;
    float TimeGrowthTimer = 0.0f;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
    }

    void Start()
    {
        SpawnMultiplier = InitialSpawnMultiplier;
        TimeGrowthTimer = TimeGrowth;
    }
    void Update()
    {
        Tick();
    }
    void Tick()
    {
        if (TimeGrowthTimer > 0.0f) { TimeGrowthTimer -= Time.deltaTime; return; }

        SpawnMultiplier += 0.1f;

        TimeGrowthTimer = TimeGrowth;
    }
}
