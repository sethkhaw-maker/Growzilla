using UnityEngine;
using UnityEngine.UI;

public class RampageDestructionHandler : MonoBehaviour
{
    public static RampageDestructionHandler Instance { get; private set; }
    public int CurrentScore { get; private set; }
    [SerializeField] Text DestructionText = null;
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(this.gameObject); }
    }
    void Start()
    {
        CurrentScore = 0;
        DestructionText.text = CurrentScore.ToString();
    }

    public void AddScore(int arg_amount)
    {
        CurrentScore += arg_amount;
        DestructionText.text = CurrentScore.ToString();
    }

    public void LoseHealth() { GetComponent<RampageStageSceneHandler>().DecreaseHealth(); }
}
