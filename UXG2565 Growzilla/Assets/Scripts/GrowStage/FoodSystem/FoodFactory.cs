using UnityEngine;

public class FoodFactory : MonoBehaviour
{
    public static FoodFactory Instance { get; private set; }
    public GameObject FoodPrefab = null;
    public GameObject ConveyorSpawnLocation = null;
    public GameObject ChuteDisplayLocation = null;
    [HideInInspector] public GameObject ChuteSpawnLocation = null;
    [HideInInspector] public FoodData foodInChute = null;
    [HideInInspector] public GameObject foodInChuteObject = null;
    [SerializeField] float ConveyorSpeed = 1.0f;
    [SerializeField] float FallingSpeed = 5.0f;

    [HideInInspector] public bool FactoryIsEnabled = true;

    void Awake()
    {
        ImplementSingleton();
        FactoryIsEnabled = true;
    }
    private void ImplementSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // For ConveyorBelt
    public FoodData SpawnFood(Vector3 arg_position)
    {
        if (!FactoryIsEnabled) { return null; }

        FoodData spawnFoodData = new FoodData();
        spawnFoodData.SpriteType = Random.Range(1, 10);
        spawnFoodData.FoodType = (spawnFoodData.SpriteType % 3 == 0) ? 3 : spawnFoodData.SpriteType % 3;
        int ColorType = Random.Range(1, 9);
        switch (ColorType)
        {
            case 1: spawnFoodData.FoodColor = new Vector3(1.0f, 0.0f, 0.0f); break;
            case 2: spawnFoodData.FoodColor = new Vector3(0.0f, 1.0f, 0.0f); break;
            case 3: spawnFoodData.FoodColor = new Vector3(0.0f, 0.0f, 1.0f); break;
            case 4: spawnFoodData.FoodColor = new Vector3(1.0f, 1.0f, 0.0f); break;
            case 5: spawnFoodData.FoodColor = new Vector3(0.0f, 1.0f, 1.0f); break;
            case 6: spawnFoodData.FoodColor = new Vector3(1.0f, 0.0f, 1.0f); break;
            case 7: spawnFoodData.FoodColor = new Vector3(1.0f, 1.0f, 1.0f); break;
            case 8: spawnFoodData.FoodColor = new Vector3(0.0f, 0.0f, 0.0f); break;
        }

        GameObject SpawnedObject = Instantiate(FoodPrefab, arg_position, Quaternion.identity);
        SpawnedObject.GetComponent<FoodScript>().foodData = spawnFoodData;
        SpawnedObject.GetComponent<FoodScript>().objectType = FoodObjectType.Conveyor;
        SpawnedObject.GetComponent<FoodScript>().moveSpeed = ConveyorSpeed;
        SpawnedObject.GetComponent<FoodScript>().OnCreate();
        SpawnedObject.transform.localScale =
            new Vector3(
                -SpawnedObject.transform.localScale.x,
                SpawnedObject.transform.localScale.y,
                SpawnedObject.transform.localScale.z);

        return spawnFoodData;
    }
    public FoodData SpawnFood(Vector3 arg_position, FoodData arg_foodData, bool isDisplay = false)
    {
        if (!FactoryIsEnabled) { return null; }

        GameObject SpawnedObject = Instantiate(FoodPrefab, arg_position, Quaternion.identity);
        SpawnedObject.GetComponent<FoodScript>().foodData = arg_foodData;
        if (isDisplay)
        {
            SpawnedObject.GetComponent<FoodScript>().objectType = FoodObjectType.Display;
            SpawnedObject.GetComponent<FoodScript>().moveSpeed = 0.0f;
            foodInChuteObject = SpawnedObject;
        }
        else
        {
            SpawnedObject.GetComponent<FoodScript>().objectType = FoodObjectType.Falling;
            SpawnedObject.GetComponent<FoodScript>().moveSpeed = FallingSpeed;
        }
        
        SpawnedObject.GetComponent<FoodScript>().OnCreate();

        return arg_foodData;
    }
    public void DisableFactory()
    {
        if (foodInChuteObject != null) { Destroy(foodInChuteObject); }
        FactoryIsEnabled = false;
    }
}

