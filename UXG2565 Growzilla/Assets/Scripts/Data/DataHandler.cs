using System.IO;
using UnityEngine;

public partial class DataHandler : MonoBehaviour
{
    public static DataHandler Instance { get; private set; }
    // ----------------------------------------------------------------
    private DynamicData scoreData = new DynamicData();
    private CacheData cacheData = new CacheData();
    // ----------------------------------------------------------------

    [HideInInspector] public string InitialDataPath = "";

#if UNITY_ANDROID

    void Awake()
    {
        InitialDataPath = Application.persistentDataPath;
        ImplementSingleton();
        GetHighscore();
        GetCacheData();
    }

#elif UNITY_WEBGL

    void Awake()
    {
        InitialDataPath = Application.persistentDataPath;
        ImplementSingleton();
        GetHighscore();
        GetCacheData();
    }

#else

    void Awake()
    {
        InitialDataPath = Application.dataPath;
        ImplementSingleton();
        GetHighscore();
        GetCacheData();
    }

#endif

}

public partial class DataHandler : MonoBehaviour
{
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
    // ----------------------------------------------------------------
    public void GetCacheData()
    {
        string cachePath = InitialDataPath + "/CacheData.json";

        if (File.Exists(cachePath))
        {
            string cacheJson = File.ReadAllText(cachePath);
            cacheData = JsonUtility.FromJson<CacheData>(cacheJson);
        }
        else
        {
            ResetCacheData();
        }
    }
    public void ResetCacheData()
    {
        Debug.Log("a");
        cacheData = new CacheData();
        string cacheJson = JsonUtility.ToJson(cacheData, true);
        string cachePath = InitialDataPath + "/CacheData.json";
        File.WriteAllText(cachePath, cacheJson);
    }
    public void UpdateCacheData()
    {
        string cacheJson = JsonUtility.ToJson(cacheData, true);
        string cachePath = InitialDataPath + "/CacheData.json";
        File.WriteAllText(cachePath, cacheJson);
    }
    public CacheData ReadCacheData()
    {
        if (this.cacheData == null) { GetCacheData(); }
        return this.cacheData;
    }
    public void InputCacheData(CacheData InputData)
    {
        this.cacheData = InputData;
    }
    // ----------------------------------------------------------------
    public void GetHighscore()
    {
        string dynPath = InitialDataPath + "/ScoreData.json";

        if (File.Exists(dynPath))
        {
            string dynJson = File.ReadAllText(dynPath);
            scoreData = JsonUtility.FromJson<DynamicData>(dynJson);
        }
        else
        {
            ResetHighScore();
        }
    }
    public void ResetHighScore()
    {
        scoreData = new DynamicData();

        /*
         * Generate pre-made highscores for new users here.
         */

        if (scoreData.Highscore.Count > 0) { scoreData.Highscore.Sort(); }

        string dynJson = JsonUtility.ToJson(scoreData, true);
        string dynPath = InitialDataPath + "/ScoreData.json";
        File.WriteAllText(dynPath, dynJson);
    }
    public void UpdateHighscore()
    {
        if (scoreData.Highscore.Count > 0) { scoreData.Highscore.Sort(); }

        string dynJson = JsonUtility.ToJson(scoreData, true);
        string dynPath = InitialDataPath + "/ScoreData.json";
        File.WriteAllText(dynPath, dynJson);
    }
}
