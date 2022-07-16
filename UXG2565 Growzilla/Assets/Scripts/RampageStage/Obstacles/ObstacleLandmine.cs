using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLandmine : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2.0f;
    [SerializeField] GameObject ExplosionPrefab = null;
    [SerializeField] int DestructionScore = 50;
    Rigidbody2D rb2d = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        GetComponent<Animator>().Play("LandmineIdle");
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
                Debug.Log("Kaijuu Receives Damage from Landmines!");
                GetComponent<Animator>().Play("LandmineTrigger");
                GetComponent<Collider2D>().enabled = false;
                Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                //Invoke("OnDestroyed", 1.0f);
                break;
            case "ability_tail_hitbox":
                ScreenShakeController.cam_instance.startShake(0.05f, 0.05f);
                RampageDestructionHandler.Instance.AddScore(DestructionScore);
                Debug.Log("Kaijuu Used Skill to Destroy Landmines!");
                GetComponent<Animator>().Play("LandmineTrigger");
                GetComponent<Collider2D>().enabled = false;
                Instantiate(ExplosionPrefab, this.gameObject.transform.position, Quaternion.identity);
                Destroy(this.gameObject);
                //Invoke("OnDestroyed", 1.0f);
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
