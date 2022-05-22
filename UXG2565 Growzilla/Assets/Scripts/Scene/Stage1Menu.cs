using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Stage1Menu : MonoBehaviour
{
    [Header("Overlay Properties")]
    [SerializeField] GameObject HighscoreOverlay = null;

    void Start()
    {
        OnCloseHighscoreButtonPressed(true);
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        Debug.LogWarning("Highscore display is not implemented yet, do make sure to implement it after the main scenes are completed.");
    }
}
public partial class Stage1Menu : MonoBehaviour
{
    public void OnGameStartButtonPressed(bool muteAudio = false)
    {
        SceneManager.LoadScene("01_GrowStage");
    }
    public void OnHighscoreButtonPressed(bool muteAudio = false)
    {
        if (HighscoreOverlay == null)
        {
            throw new System.Exception("Highscore Overlay has not been referenced in the Inspector!");
        }
        else
        {
            HighscoreOverlay.SetActive(true);
        }
    }
    public void OnCloseHighscoreButtonPressed(bool muteAudio = false)
    {
        if (HighscoreOverlay == null)
        {
            throw new System.Exception("Highscore Overlay has not been referenced in the Inspector!");
        }
        else
        {
            HighscoreOverlay.SetActive(false);
        }
    }
    public void OnGameQuitButtonPressed(bool muteAudio = false)
    {
        Application.Quit();
    }
}