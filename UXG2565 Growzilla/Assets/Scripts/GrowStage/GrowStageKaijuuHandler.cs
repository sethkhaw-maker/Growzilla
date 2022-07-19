using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GrowStageKaijuuHandler : MonoBehaviour
{
    public static GrowStageKaijuuHandler Instance { get; private set; }

    [Header("References")]
    [SerializeField] GameObject KaijuuObject = null;
    [SerializeField] GameObject[] KaijuuSizes = new GameObject[3];
    [SerializeField] SpriteRenderer[] AllColorableParts = new SpriteRenderer[1];

    [SerializeField] GameObject[] KaijuuSizesDisplay = new GameObject[3];
    [SerializeField] SpriteRenderer[] AllColorablePartsDisplay = new SpriteRenderer[1];

    [SerializeField] UnityEngine.UI.Text KaijuuNameField = null;

    [SerializeField] GameObject DisplayObject = null;

    [Header("Evolution Threshold")]
    [SerializeField] int midEvolveAmount = 10;
    [SerializeField] int bigEvolveAmount = 20;

    [SerializeField] float kaijuuSpeed = 3.0f;

    bool KaijuuMovementEnabled = false;
    int dir = 1;
    float kaijuuFlipTimer = 0.0f;
    float flipDelay = 0.0f;

    int currentSize = 0;
    int foodEaten = 0;
    int[] AbilityGrowth = new int[3];
    Vector3 KaijuuColor = new Vector3(1.0f, 1.0f, 1.0f);
    
    public List<Animator> KaijuAnimators;

    [SerializeField] Text HornsAmount = null;
    [SerializeField] Text TailAmount = null;
    [SerializeField] Text WingsAmount = null;


    // ----------------------------------------------------------

    void Awake()
    {
        ImplementSingleton();

    }

    void Start()
    {
        KaijuuMovementEnabled = false;
        dir = 1;
        kaijuuFlipTimer = 5.0f;
        flipDelay = 0.0f;
    }

    void Update()
    {
        MoveKaijuu();
        TryFlipKaijuu();
        if (Input.GetKeyDown(KeyCode.F)) { FlipKaijuu(); }
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

    // ----------------------------------------------------------

    void MoveKaijuu()
    {
        foreach (Animator anims in KaijuAnimators)
        {
            if(anims.GetCurrentAnimatorStateInfo(0).IsName("Walk")!=true)
            anims.Play("Walk");
        }
        if (KaijuuObject == null) { return; }
        if (!KaijuuMovementEnabled) { return; }
        if (flipDelay > 0.0f) { flipDelay -= Time.deltaTime; return; }
        Vector3 newPos = KaijuuObject.transform.position + new Vector3(1 * this.dir * kaijuuSpeed * Time.deltaTime * 30, 0, 0);
        
        if (currentSize == 0)
        {
            if (newPos.x > 7.7f) { FlipKaijuu(); newPos.x = 7.7f; }
            if (newPos.x < -7.7f) { FlipKaijuu(); newPos.x = -7.7f; }
        }

        if (currentSize == 1)
        {
            if (newPos.x > 7.35f) { FlipKaijuu(); newPos.x = 7.35f; }
            if (newPos.x < -7.35f) { FlipKaijuu(); newPos.x = -7.35f; }
        }

        if (currentSize == 2)
        {
            if (newPos.x > 6.85f) { FlipKaijuu(); newPos.x = 6.85f; }
            if (newPos.x < -6.85f) { FlipKaijuu(); newPos.x = -6.85f; }
        }

        KaijuuObject.transform.position = newPos;
        KaijuuObject.transform.localScale = new Vector3(Mathf.Abs(KaijuuObject.transform.localScale.x) * dir, KaijuuObject.transform.localScale.y, KaijuuObject.transform.localScale.z);
    }
    void TryFlipKaijuu()
    {
        foreach (Animator anims in KaijuAnimators)
        {
            if (anims.GetCurrentAnimatorStateInfo(0).IsName("Walk") != true)
                anims.Play("Idle");
        }
        if (KaijuuObject == null) { return; }
        if (!KaijuuMovementEnabled) { return; }
        if (kaijuuFlipTimer > 0.0f) { kaijuuFlipTimer -= Time.deltaTime; return; }
        kaijuuFlipTimer = 5.0f + Random.Range(0.0f, 3.0f);
        FlipKaijuu();
    }
    public void FlipKaijuu()
    {
        if (KaijuuObject == null) { return; }
        flipDelay = 0.5f;
        dir = -dir;
        kaijuuFlipTimer += 1.0f;
    }
    public void GrowKaijuu()
    {
        currentSize += 1;
        if (currentSize > 2) { currentSize = 2; }

        KaijuuSizes[currentSize].SetActive(true);

        KaijuuSizes[currentSize].transform.position =
            new Vector3(
                KaijuuSizes[currentSize].transform.position.x,
                KaijuuSizes[currentSize - 1].transform.position.y,
                KaijuuSizes[currentSize].transform.position.z
                );

        KaijuuSizesDisplay[currentSize].SetActive(true);

        UpdateColor();

        KaijuuSizes[currentSize-1].SetActive(false);
        KaijuuSizesDisplay[currentSize - 1].SetActive(false);
    }

    // ----------------------------------------------------------

    public void OnKaijuuMovementEnabled()
    {
        // 1.Enable Kaijuu movement.(Kaijuu now moves around in "Update()"
        KaijuuMovementEnabled = true;
    }

    public void OnKaijuuEatFood(FoodScript InputFood)
    {
        // 1. FoodEaten + 1.
        foodEaten++;
        // 2. AbilityGrowth[(int)Food.AbilityType] + 1.
        AbilityGrowth[InputFood.foodData.FoodType - 1] += 1;
        // 3. Color = Color / 5 * 4 + Food.Color / 5.
        KaijuuColor = KaijuuColor / 10.0f * 8.0f + InputFood.foodData.FoodColor / 10.0f * 2.0f;

        // 4. Destroy Food
        Destroy(InputFood.gameObject);

        // 5. Try Evolve.
        OnKaijuuTryEvolve();

        HornsAmount.text = AbilityGrowth[0].ToString();
        TailAmount.text = AbilityGrowth[1].ToString();
        WingsAmount.text = AbilityGrowth[2].ToString();
    }
    

    [SerializeField] GameObject[] MidUpgrades = new GameObject[1];
    [SerializeField] GameObject[] BigUpgrades = new GameObject[1];

    [SerializeField] GameObject[] MidUpgradesDisplay = new GameObject[1];
    [SerializeField] GameObject[] BigUpgradesDisplay = new GameObject[1];

    public void OnKaijuuTryEvolve()
    {
        // 1. ApplyColor onto the sprite.
        UpdateColor();

        // 2. Check FoodEaten against MidEvolveAmount and BigEvolve Amount accordingly.
        if ((currentSize == 0 && foodEaten > midEvolveAmount) || (currentSize == 1 && foodEaten > bigEvolveAmount))
        {
            GrowKaijuu();
        }

        int smallest = AbilityGrowth[0];
        int smallestIndex = 0;
        for (int i = 1; i < AbilityGrowth.Length; i++)
        {
            if (smallest > AbilityGrowth[i])
            {
                smallest = AbilityGrowth[i];
                smallestIndex = i;
            }
        }

        // 3. Update Kaijuu Sprites to match AbilityGrowth if size is bigger than small(0).
        if (currentSize == 1)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == smallestIndex) { MidUpgrades[i].GetComponent<Animator>().Play("Despawn"); }
                else { MidUpgrades[i].SetActive(true); }

                if (i == smallestIndex) { MidUpgradesDisplay[i].SetActive(false); }
                else { MidUpgradesDisplay[i].SetActive(true); }
            }
        }
        if (currentSize == 2)
        {
            for (int i = 0; i < 3; i++)
            {
                if (i == smallestIndex) { BigUpgrades[i].GetComponent<Animator>().Play("Despawn"); ; }
                else { BigUpgrades[i].SetActive(true); }

                if (i == smallestIndex) { BigUpgradesDisplay[i].SetActive(false); }
                else { BigUpgradesDisplay[i].SetActive(true); }
            }
        }
    }

    public void UpdateColor()
    {
        foreach(SpriteRenderer SR in AllColorableParts)
        {
            SR.color = new Color(KaijuuColor.x, KaijuuColor.y, KaijuuColor.z, 1.0f);
        }
        foreach (SpriteRenderer SR in AllColorablePartsDisplay)
        {
            SR.color = new Color(KaijuuColor.x, KaijuuColor.y, KaijuuColor.z, 1.0f);
        }
    }

    public void OnGrowStageTimeEnd()
    {
        if (KaijuuObject == null) { return; }
        KaijuuMovementEnabled = false;

        KaijuuObject.transform.position =
            new Vector3(
                KaijuuObject.transform.position.x,
                KaijuuObject.transform.position.y,
                50f
                    );
        DisplayObject.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
    }
    public void OnRampageButtonPressed()
    {
        DisplayObject.transform.position = new Vector3(0.0f, 1000.0f, 0.0f);
        DataHandler.Instance.InputCacheData(ReturnKaijuuData());
        DataHandler.Instance.UpdateCacheData();
    }
    public CacheData ReturnKaijuuData()
    {
        CacheData CurrentData = DataHandler.Instance.ReadCacheData();

        CurrentData.Current.KaijuuName = KaijuuNameField.text.ToString();

        if (CurrentData.Current.KaijuuName == "") { CurrentData.Current.KaijuuName = "Test Subject"; }

        CurrentData.Current.FoodEaten = this.foodEaten;
        CurrentData.Current.Color = new float[] { KaijuuColor.x, KaijuuColor.y, KaijuuColor.z };

        int smallest = AbilityGrowth[0];
        int smallestIndex = 0;
        for (int i = 1; i < AbilityGrowth.Length; i++)
        {
            if (smallest > AbilityGrowth[i])
            {
                smallest = AbilityGrowth[i];
                smallestIndex = i;
            }
        }

        CurrentData.Current.Ability1 = (smallestIndex != 0)?  "Horns": "Tail";

        if (CurrentData.Current.Ability1 == "Horns")
            CurrentData.Current.Ability2 = (smallestIndex != 1) ? "Tail" : "Wings";
        else
            CurrentData.Current.Ability2 = "Wings";

        CurrentData.Current.Destruction = 0;
        CurrentData.Current.Distance = 0;
        CurrentData.Current.Score = 0;

        return CurrentData;
    }

    // ----------------------------------------------------------
}
