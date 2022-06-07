using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GrowStageSceneHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject KaijuuReference = null;
    [SerializeField] GameObject ChuteReference = null;
    [SerializeField] GameObject NamePanelReference = null;
    [SerializeField] GameObject MainMenuCanvas = null;
    [SerializeField] GameObject ScreenFader = null;
    [SerializeField] GameObject BottomFloor = null;
    [SerializeField] GameObject BottomFloorSpriteLeft = null;
    [SerializeField] GameObject BottomFloorSpriteRight = null;
    [SerializeField] GameObject AndroidButtonA = null;
    [SerializeField] GameObject AndroidButtonB = null;
    [SerializeField] Text TimeLeftBeforeAttack = null;

    [Header("Adjustable Values")]
    [SerializeField] float GrowStageFoodSpawnBuffer = 3.0f;
    [SerializeField] float GrowStageTimeLimit = 60.0f;
    [SerializeField] float ChuteSpeed = 1.5f;
    [SerializeField] float FoodSpawnDelay = 1.0f;

    [Header("Transition Animation Objects")]
    [SerializeField] GameObject[] StartTransitionObjects = new GameObject[1];

    [Header("Prefabs")]
    [SerializeField] GameObject FoodObjectPrefab = null;

    bool FoodChuteControlEnabled = false;
    bool FoodSpawnEnabled = false;
    float FoodSpawnTimer = 0.0f;

    // ----------------------------------------------------------

    void Start()
    {
        FoodChuteControlEnabled = false;
        FoodSpawnEnabled = false;
        FoodSpawnTimer = FoodSpawnDelay;
    }

    void Update()
    {
        ControlFoodChute();
        SpawnFoodFromChute();
    }

    // ----------------------------------------------------------

    void ControlFoodChute()
    {
        if (!FoodChuteControlEnabled) { return; }
        if (ChuteReference == null) { return; }

        if (Input.GetKey(KeyCode.Z)) { MoveChuteLeft();  }
        if (Input.GetKey(KeyCode.X)) { MoveChuteRight(); }
    }
    void SpawnFoodFromChute()
    {
        if (!FoodSpawnEnabled) { return; }
        if (FoodSpawnTimer > 0.0f) { FoodSpawnTimer -= Time.deltaTime; return; }
        Vector3 FoodSpawnPos = new Vector3(ChuteReference.transform.position.x, ChuteReference.transform.position.y - 1.5f, 0.0f);
        Instantiate(FoodObjectPrefab, FoodSpawnPos, Quaternion.identity);
        FoodSpawnTimer = FoodSpawnDelay;
    }
    public void MoveChuteLeft()
    {
        Vector2 newPos = ChuteReference.GetComponent<RectTransform>().anchoredPosition + new Vector2(-1 * ChuteSpeed * Time.deltaTime * 30, 0);
        if (ChuteReference.GetComponent<RectTransform>().anchoredPosition.x <= -865.0f)
        {
            newPos = new Vector2(-865, ChuteReference.GetComponent<RectTransform>().anchoredPosition.y);
        }
        ChuteReference.GetComponent<RectTransform>().anchoredPosition = newPos;
    }
    public void MoveChuteRight()
    {
        Vector2 newPos = ChuteReference.GetComponent<RectTransform>().anchoredPosition + new Vector2(1 * ChuteSpeed * Time.deltaTime * 30, 0);
        if (ChuteReference.GetComponent<RectTransform>().anchoredPosition.x >= 865.0f)
        {
            newPos = new Vector2(865, ChuteReference.GetComponent<RectTransform>().anchoredPosition.y);
        }
        ChuteReference.GetComponent<RectTransform>().anchoredPosition = newPos;
    }

    // ----------------------------------------------------------

    // Call using Unity Button Events
    public void OnStartButtonPressed()
    {
        // 1. Play Transition Sequence.
        foreach (GameObject GO in StartTransitionObjects)
        {
            if (GO.GetComponent<Animator>() != null)
            {
                GO.GetComponent<Animator>().enabled = true;
            }
        }

        TimeLeftBeforeAttack.text = (GrowStageTimeLimit).ToString();
    }
    // Call using Animation Events
    public void OnTransitionReachedKaijuuSpawn()
    {
        // 1. Spawn Kaijuu in scene. (Or just activate the Game Object)
        KaijuuReference.SetActive(true);
        // 2. Create new cache data.
        DataHandler.Instance.ResetCacheData();
    }
    // Call using Animation Events
    public void OnTransitionSequenceEnd()
    {
        // 1. Enable chute control.
        FoodChuteControlEnabled = true;
        AndroidButtonA.SetActive(true);
        AndroidButtonB.SetActive(true);

        // 2. Enable Kaijuu Movement.
        this.gameObject.GetComponent<GrowStageKaijuuHandler>().OnKaijuuMovementEnabled();

        if (MainMenuCanvas != null) { MainMenuCanvas.SetActive(false); }

        // 3. Start buffer time before food spawn.
        StartCoroutine(FoodBufferCoroutine());
    }
    IEnumerator FoodBufferCoroutine()
    {
        for (int i = 0; i < GrowStageFoodSpawnBuffer; i++)
        {
            Debug.Log("Delay Time Left: " + (GrowStageFoodSpawnBuffer - i));
            yield return new WaitForSeconds(1);
        }
        
        OnFoodSpawnBufferEnd();
    }
    public void OnFoodSpawnBufferEnd()
    {
        StopCoroutine(FoodBufferCoroutine());

        // 1. Start Food Spawning.
        FoodSpawnEnabled = true;

        // 2. Start Grow Stage Timer.
        StartCoroutine(GrowTimerCoroutine());
    }
    IEnumerator GrowTimerCoroutine()
    {
        for (int i = 0; i < GrowStageTimeLimit; i++)
        {
            // Debug.Log("Grow Stage Time Left: " + (GrowStageTimeLimit - i));
            TimeLeftBeforeAttack.text = (GrowStageTimeLimit - i).ToString();
            // Debug.Log("i: " + i);
            // Debug.Log("GrowStageTimeLimit");
            yield return new WaitForSeconds(1);
        }

        OnGrowStageTimeEnd();
    }
    public void OnGrowStageTimeEnd()
    {
        StopCoroutine(GrowTimerCoroutine());

        // Display time's up.
        TimeLeftBeforeAttack.text = "0";

        // 1. Disable Food Spawn.
        FoodSpawnEnabled = false;

        // 2. Disable Kaijuu Movement.
        this.gameObject.GetComponent<GrowStageKaijuuHandler>().OnGrowStageTimeEnd();

        // 3. Disable Chute Control
        FoodChuteControlEnabled = false;

        // 4. Spawn Name Input Panel.
        if (NamePanelReference != null) NamePanelReference.SetActive(true);
    }
    // Call using Unity Button Events
    public void OnNameEnterButtonPressed()
    {
        // 1. Play scene fade animation.
        if (ScreenFader != null) ScreenFader.GetComponent<Animator>().enabled = true;

        // 2. Let the Kaijuu fall through the floor.
        if (BottomFloor != null) BottomFloor.SetActive(false);
        if (BottomFloorSpriteLeft != null) BottomFloorSpriteLeft.GetComponent<Animator>().enabled = true;
        if (BottomFloorSpriteRight != null) BottomFloorSpriteRight.GetComponent<Animator>().enabled = true;

        StartCoroutine(SceneFadeCoroutine());
    }

    IEnumerator SceneFadeCoroutine()
    {
        for (int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(1);
        }
        OnSceneFaded();
    }

    public void OnSceneFaded()
    {
        // 1. Change Scene.
        SceneManager.LoadScene("01_GrowScene");
    }

    // ----------------------------------------------------------
}
