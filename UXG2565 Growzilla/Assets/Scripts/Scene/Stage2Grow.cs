using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Stage2Grow : MonoBehaviour
{
    [Header("Overlay Properties")]
    [SerializeField] GameObject SetttingsOverlay = null;
    [SerializeField] GameObject ReturnToMenuPrompt = null;
    [Header("Food Chute Properties")]
    [SerializeField] GameObject FoodChute = null;
    [SerializeField] float ChuteSpeed = 1.0f;
    [SerializeField] float FoodSpawnDelay = 0.5f;
    [SerializeField] Vector2 FoodSpawnOffset = new Vector2();
    [SerializeField] GameObject[] SpawnableFood;
    float FoodSpawnTimer = 0.0f;

    void Start()
    {
        //OnCloseSettingsButtonPressed(true);
        FoodSpawnTimer = FoodSpawnDelay;
    }
    void Update()
    {
        // Input
        if (Input.GetKey(KeyCode.Z)) { OnLeftButtonPressed(); }
        if (Input.GetKey(KeyCode.X)) { OnRightButtonPressed(); }

        // Logic
        OnGameTick();
    }

    void OnGameTick()
    {
        // Handle Food Spawn Loop
        if (FoodSpawnTimer >= 0.0f) { FoodSpawnTimer -= Time.deltaTime; }
        else { SpawnFood(); FoodSpawnTimer = FoodSpawnDelay; }

        // To Handle Kaijuu Movement
    }
    void SpawnFood()
    {
        if (SpawnableFood.Length < 1)
        {
            throw new System.Exception("Spawnable Food not initialized in Inspector!");
        }
        else
        {
            int spawnIndex = Random.Range(0, SpawnableFood.Length);
            Instantiate(SpawnableFood[spawnIndex], FoodChute.transform.position + (Vector3)FoodSpawnOffset, Quaternion.identity);
        }
    }
}
public partial class Stage2Grow : MonoBehaviour
{
    public void OnLeftButtonPressed(bool muteAudio = false)
    {
        if (Time.timeScale == 0.0f) { return; }

        if (FoodChute == null)
        {
            throw new System.Exception("Food Chute has not been referenced in the Inspector!");
        }
        else
        {
            FoodChute.transform.localPosition = 
                new Vector3(

                    ((FoodChute.transform.localPosition.x - 1.0f * ChuteSpeed) < -8.5f)?
                    -8.5f : FoodChute.transform.localPosition.x - 1.0f * ChuteSpeed,

                    FoodChute.transform.localPosition.y,
                    FoodChute.transform.localPosition.z
                    );
        }
    }
    public void OnRightButtonPressed(bool muteAudio = false)
    {
        if (Time.timeScale == 0.0f) { return; }

        if (FoodChute == null)
        {
            throw new System.Exception("Food Chute has not been referenced in the Inspector!");
        }
        else
        {
            FoodChute.transform.localPosition =
                new Vector3(

                    ((FoodChute.transform.localPosition.x + 1.0f * ChuteSpeed) > 8.5f) ?
                    8.5f : FoodChute.transform.localPosition.x + 1.0f * ChuteSpeed,

                    FoodChute.transform.localPosition.y,
                    FoodChute.transform.localPosition.z
                    );
        }
    }
    public void OnSettingsButtonPressed(bool muteAudio = false)
    {
        if (SetttingsOverlay == null)
        {
            throw new System.Exception("Setttings Overlay has not been referenced in the Inspector!");
        }
        else
        {
            Time.timeScale = 0.0f;
            SetttingsOverlay.SetActive(true);
        }
    }
    public void OnCloseSettingsButtonPressed(bool muteAudio = false)
    {
        if (SetttingsOverlay == null)
        {
            throw new System.Exception("Setttings Overlay has not been referenced in the Inspector!");
        }
        else
        {
            Time.timeScale = 1.0f;
            SetttingsOverlay.SetActive(false);
        }
    }
    public void OnReturnToMainMenuPressed(bool muteAudio = false)
    {
        if (ReturnToMenuPrompt == null)
        {
            throw new System.Exception("Return To Menu Prompt has not been referenced in the Inspector!");
        }
        else
        {
            ReturnToMenuPrompt.SetActive(true);
        }
    }
    public void OnConfirmReturnToMainMenuPressed(bool muteAudio = false)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("00_MainMenu");
    }
    public void OnCancelReturnToMainMenuPressed(bool muteAudio = false)
    {
        if (ReturnToMenuPrompt == null)
        {
            throw new System.Exception("Return To Menu Prompt has not been referenced in the Inspector!");
        }
        else
        {
            ReturnToMenuPrompt.SetActive(false);
        }
    }
}