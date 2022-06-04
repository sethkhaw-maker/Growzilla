using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] GameObject[] Sprites = new GameObject[9];
    [HideInInspector] public int FoodType = 1;
    [HideInInspector] public int SpriteType = 1;
    [HideInInspector] public Vector3 FoodColor = new Vector3(0, 0, 0);

    void Awake()
    {
        SpriteType = Random.Range(1, 10);
        FoodType = (SpriteType % 3 == 0)? 3: SpriteType % 3;
        int ColorType = Random.Range(1, 8);
        switch (ColorType)
        {
            case 1: FoodColor = new Vector3(1.0f, 0.0f, 0.0f); break;
            case 2: FoodColor = new Vector3(0.0f, 1.0f, 0.0f); break;
            case 3: FoodColor = new Vector3(0.0f, 0.0f, 1.0f); break;
            case 4: FoodColor = new Vector3(1.0f, 1.0f, 0.0f); break;
            case 5: FoodColor = new Vector3(0.0f, 1.0f, 1.0f); break;
            case 6: FoodColor = new Vector3(1.0f, 0.0f, 1.0f); break;
            case 7: FoodColor = new Vector3(1.0f, 1.0f, 1.0f); break;
            // case 8: FoodColor = new Vector3(0.0f, 0.0f, 0.0f); break;
        }
        Sprites[SpriteType - 1].SetActive(true);
        Sprites[SpriteType - 1].GetComponent<SpriteRenderer>().color = new Color(FoodColor.x, FoodColor.y, FoodColor.z, 1.0f);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Kaijuu"))
        {
            GrowStageKaijuuHandler.Instance.OnKaijuuEatFood(this);
        }
    }
}
