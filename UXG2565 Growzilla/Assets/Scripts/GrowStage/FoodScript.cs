using UnityEngine;

public class FoodScript : MonoBehaviour
{
    [SerializeField] GameObject[] Sprites = new GameObject[9];
    [HideInInspector ]public float moveSpeed = 0.0f;

    public FoodData foodData = null;
    public FoodObjectType objectType = FoodObjectType.Falling;

    void Update()
    {
        if ((int)objectType == 2)
        {
            this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Time.timeSinceLevelLoad * 360.0f);
        }
    }

    public void OnCreate()
    {
        Sprites[foodData.SpriteType - 1].SetActive(true);
        Sprites[foodData.SpriteType - 1].GetComponent<SpriteRenderer>().color = new Color(foodData.FoodColor.x, foodData.FoodColor.y, foodData.FoodColor.z, 1.0f);

        Vector2 m_velocity = ((int)objectType == 0) ? new Vector2(-1.0f, 0.0f) : ((int)objectType == 1) ? new Vector2(0.0f, 0.0f) : new Vector2(0.0f, -1.0f);
        m_velocity *= moveSpeed;
        this.GetComponent<Rigidbody2D>().velocity = m_velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Kaijuu"))
        {
            if (FoodFactory.Instance.FactoryIsEnabled == false) { Destroy(this.gameObject); return; }

            GrowStageKaijuuHandler.Instance.OnKaijuuEatFood(this);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Destructor"))
        {
            if (other.gameObject.name == "FoodProcessor" && ((int)objectType == 0))
            {
                if (FoodFactory.Instance.FactoryIsEnabled == false) { Destroy(this.gameObject); return; }

                if (FoodFactory.Instance.foodInChute != null)
                {
                    Vector3 FoodSpawnPos = 
                        new Vector3(
                            FoodFactory.Instance.ChuteSpawnLocation.transform.position.x,
                            FoodFactory.Instance.ChuteSpawnLocation.transform.position.y - 1.5f,
                            0.0f);

                    FoodFactory.Instance.SpawnFood(FoodSpawnPos, FoodFactory.Instance.foodInChute);
                    FoodFactory.Instance.foodInChute = null;
                    Destroy(FoodFactory.Instance.foodInChuteObject);
                }
                FoodFactory.Instance.foodInChute = FoodFactory.Instance.SpawnFood(FoodFactory.Instance.ChuteDisplayLocation.transform.position, this.foodData, true);
            }
            if ((int)objectType != 1) { Destroy(this.gameObject); }
        }
    }
}
