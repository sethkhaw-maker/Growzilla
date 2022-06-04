using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrowStageSceneHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameObject KaijuuReference = null;
    [SerializeField] GameObject ChuteReference = null;
    [SerializeField] GameObject NamePanelReference = null;
    [SerializeField] GameObject MainMenuCanvas = null;
    [SerializeField] GameObject ScreenFader = null;

    [Header("Adjustable Values")]
    [SerializeField] float GrowStageFoodSpawnBuffer = 3.0f;
    [SerializeField] float GrowStageTimeLimit = 60.0f;
    [SerializeField] float ChuteSpeed = 1.5f;

    [Header("Transition Animation Objects")]
    [SerializeField] GameObject[] StartTransitionObjects = new GameObject[1];

    bool FoodChuteControlEnabled = false;
    bool KaijuuMovementEnabled = false;

    // ----------------------------------------------------------

    void Start()
    {
        FoodChuteControlEnabled = false;
        KaijuuMovementEnabled = false;
    }

    void Update()
    {
        ControlFoodChute();
        MoveKaijuu();
    }

    // ----------------------------------------------------------

    void ControlFoodChute()
    {
        if (!FoodChuteControlEnabled) { return; }
        if (ChuteReference == null) { return; }

        if (Input.GetKey(KeyCode.Z)) { MoveChuteLeft();  }
        if (Input.GetKey(KeyCode.X)) { MoveChuteRight(); }
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

    void MoveKaijuu()
    {
        if (!KaijuuMovementEnabled) { return; }
        if (KaijuuReference == null) { return; }


    }

    // ----------------------------------------------------------

    // Call using Unity Button Events
    public void OnStartButtonPressed()
    {
        // Play Transition Sequence.
        foreach (GameObject GO in StartTransitionObjects)
        {
            if (GO.GetComponent<Animator>() != null)
            {
                GO.GetComponent<Animator>().enabled = true;
            }
        }
    }
    // Call using Animation Events
    public void OnTransitionReachedKaijuuSpawn()
    {
        // Spawn Kaijuu in scene. (Or just activate the Game Object)
        Debug.Log("Kaijuu Spawned");

        // Create new cache data.
    }
    // Call using Animation Events
    public void OnTransitionSequenceEnd()
    {
        // Enable chute control.
        FoodChuteControlEnabled = true;

        // Enable Kaijuu Movement.
        KaijuuMovementEnabled = true;

        if (MainMenuCanvas != null) { MainMenuCanvas.SetActive(false); }

        // Start buffer time before food spawn.(Done)
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

        // Start Food Spawning.
        Debug.Log("Game Started");

        // Start Grow Stage Timer.
        StartCoroutine(GrowTimerCoroutine());
    }
    IEnumerator GrowTimerCoroutine()
    {
        for (int i = 0; i < GrowStageTimeLimit; i++)
        {
            Debug.Log("Grow Stage Time Left: " + (GrowStageTimeLimit - i));
            //Debug.Log("i: " + i);
            //Debug.Log("GrowStageTimeLimit");
            yield return new WaitForSeconds(1);
        }

        OnGrowStageTimeEnd();
    }
    public void OnGrowStageTimeEnd()
    {
        StopCoroutine(GrowTimerCoroutine());

        // Spawn Name Input Panel
        if (NamePanelReference != null) NamePanelReference.SetActive(true);
    }
    // Call using Unity Button Events
    public void OnNameEnterButtonPressed()
    {
        // Play scene fade animation.
        if (ScreenFader != null) ScreenFader.GetComponent<Animator>().enabled = true;

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
        // Change Scene.
        SceneManager.LoadScene("01_GrowScene");
    }

    // ----------------------------------------------------------
}
