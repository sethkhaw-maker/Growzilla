using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaijuAnimEvents : MonoBehaviour
{
    public GameObject SmokeParticles;
    public GameObject[] SmokeSpawnPoints;

    public void SpawnFrontLeg1()
    {
        if (SmokeParticles != null)
        {
            Instantiate(SmokeParticles, SmokeSpawnPoints[0].transform.position, Quaternion.identity);
        }
    }
    public void SpawnFrontLeg2()
    {
        if (SmokeParticles != null)
        {
            Instantiate(SmokeParticles, SmokeSpawnPoints[1].transform.position, Quaternion.identity);
        }
    }
    public void SpawnBackLeg1()
    {
        if (SmokeParticles != null)
        {
            Instantiate(SmokeParticles, SmokeSpawnPoints[2].transform.position, Quaternion.identity);
        }
    }
    public void SpawnBackLeg2()
    {
        if (SmokeParticles != null)
        {
            Instantiate(SmokeParticles, SmokeSpawnPoints[3].transform.position, Quaternion.identity);
        }
    }
}
