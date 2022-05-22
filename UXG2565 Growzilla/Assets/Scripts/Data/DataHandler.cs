using System.IO;
using UnityEngine;

public partial class DataHandler : MonoBehaviour
{
    public static DataHandler Instance { get; private set; }
    // ----------------------------------------------------------------
    private DynamicData scoreData = new DynamicData();
    private CacheData cacheData = new CacheData();
    // ----------------------------------------------------------------
    void Awake()
    {
        ImplementSingleton();
        GetHighscore();
        GetCacheData();
    }
}

public partial class DataHandler : MonoBehaviour
{
    private void ImplementSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // ----------------------------------------------------------------
    private void GetCacheData()
    {
        string cachePath = Application.dataPath + "/Resources/CacheData.json";

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
    private void ResetCacheData()
    {
        cacheData = new CacheData();
        string cacheJson = JsonUtility.ToJson(cacheData, true);
        string cachePath = Application.dataPath + "/Resources/CacheData.json";
        File.WriteAllText(cachePath, cacheJson);
    }
    private void UpdateCacheData()
    {
        string cacheJson = JsonUtility.ToJson(cacheData, true);
        string cachePath = Application.dataPath + "/Resources/CacheData.json";
        File.WriteAllText(cachePath, cacheJson);
    }
    // ----------------------------------------------------------------
    private void GetHighscore()
    {
        string dynPath = Application.dataPath + "/Resources/ScoreData.json";

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
    private void ResetHighScore()
    {
        scoreData = new DynamicData();

        /*
         * Generate pre-made highscores for new users here.
         */

        if (scoreData.Highscore.Count > 0) { scoreData.Highscore.Sort(); }

        string dynJson = JsonUtility.ToJson(scoreData, true);
        string dynPath = Application.dataPath + "/Resources/ScoreData.json";
        File.WriteAllText(dynPath, dynJson);
    }
    private void UpdateHighscore()
    {
        if (scoreData.Highscore.Count > 0) { scoreData.Highscore.Sort(); }

        string dynJson = JsonUtility.ToJson(scoreData, true);
        string dynPath = Application.dataPath + "/Resources/ScoreData.json";
        File.WriteAllText(dynPath, dynJson);
    }
}


public partial class DataHandler : MonoBehaviour
{
    public void OnMainMenuEnter()
    {
        ResetCacheData();
    }
    public void OnMainMenuExit()
    {
        ResetCacheData();
    }
    // ----------------------------------------------------------------
    public void OnGrowStageEnter()
    {
        ResetCacheData();
    }
    public void OnGrowStageExit()
    {
        UpdateCacheData();
    }
    // ----------------------------------------------------------------
    public void OnRampageStageEnter()
    {

    }
    public void OnRampageStageExit()
    {
        UpdateHighscore();
        ResetCacheData();
    }
    // ----------------------------------------------------------------
}