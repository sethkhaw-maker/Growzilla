using UnityEngine;
using UnityEngine.SceneManagement;

public partial class Stage1Menu : MonoBehaviour
{
    [Header("Overlay Properties")]
    [SerializeField] GameObject HowToPlayOverlay = null;
    [SerializeField] GameObject HighscoreOverlay = null;
    [SerializeField] GameObject CreditsOverlay = null;

    void Start()
    {
        OnCloseHowToPlayButtonPressed(true);
        OnCloseHighscoreButtonPressed(true);
        OnCloseCreditsButtonPressed(true);
        UpdateHighscore();
    }

    private void UpdateHighscore()
    {
        
    }
}
public partial class Stage1Menu : MonoBehaviour
{
    public void OnGameStartButtonPressed(bool muteAudio = false)
    {
        SceneManager.LoadScene("01_GrowStage");
    }
    // ----------------------------------------------------------------
    public void OnHowToPlayButtonPressed(bool muteAudio = false)
    {
        if (HowToPlayOverlay == null)
        {
            throw new System.Exception("HowToPlay Overlay has not been referenced in the Inspector!");
        }
        else
        {
            HowToPlayOverlay.SetActive(true);
        }
    }
    public void OnCloseHowToPlayButtonPressed(bool muteAudio = false)
    {
        if (HowToPlayOverlay == null)
        {
            throw new System.Exception("HowToPlay Overlay has not been referenced in the Inspector!");
        }
        else
        {
            HowToPlayOverlay.SetActive(false);
        }
    }
    // ----------------------------------------------------------------
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
    // ----------------------------------------------------------------
    public void OnCreditsButtonPressed(bool muteAudio = false)
    {
        if (CreditsOverlay == null)
        {
            throw new System.Exception("Credits Overlay has not been referenced in the Inspector!");
        }
        else
        {
            CreditsOverlay.SetActive(true);
        }
    }
    public void OnCloseCreditsButtonPressed(bool muteAudio = false)
    {
        if (CreditsOverlay == null)
        {
            throw new System.Exception("Credits Overlay has not been referenced in the Inspector!");
        }
        else
        {
            CreditsOverlay.SetActive(false);
        }
    }
    // ----------------------------------------------------------------
    public void OnGameQuitButtonPressed(bool muteAudio = false)
    {
        Application.Quit();
    }
}