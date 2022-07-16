using UnityEngine;

public class GrowPauseMenuHandler : MonoBehaviour
{
    [SerializeField] Animator PauseMenuAnimator = null;
    public void OnPauseButtonPressed()
    {
        if (Time.timeScale == 0.0f)
        {
            Time.timeScale = 1.0f;
            PauseMenuAnimator.Play("PauseMenuExit");
        }
        else
        {
            Time.timeScale = 0.0f;
            PauseMenuAnimator.Play("PauseMenuEnter");
        }
    }
    public void OnResumeButtonPressed()
    {
        Time.timeScale = 1.0f;
        PauseMenuAnimator.Play("PauseMenuExit");
    }
    public void OnHowToPlayPressed()
    {
        Time.timeScale = 0.0f;
    }
    public void OnAbortPressed()
    {
        Time.timeScale = 1.0f;
        PauseMenuAnimator.Play("PauseMenuExit");
        this.gameObject.GetComponent<GrowStageSceneHandler>().OnNameEnterButtonPressed();
    }
}
