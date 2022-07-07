using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    public Sprite[] ToExperiment;
    public Sprite[] ToRampage;
    public Image[] Bars;

    // Start is called before the first frame update
    void Start()
    {
        Bars[0].sprite = ToExperiment[0];
        Bars[1].sprite = ToExperiment[1];
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            this.gameObject.GetComponent<Animator>().Play("OpenInRampage");
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            this.gameObject.GetComponent<Animator>().Play("OpenInMainMenu");
        }
    }
    public void LoadingExperiment()
    {
        Bars[0].sprite = ToExperiment[0];
        Bars[1].sprite = ToExperiment[1];
    }

    public void LoadingRampage()
    {
        Bars[0].sprite = ToRampage[0];
        Bars[1].sprite = ToRampage[1];
    }

    public void GameActivate()
    {
        //activates controls OR Game and set self as inactive
    }

    public void GameDeactivate()
    {
        //deactivates controls OR Game (require another script to activate Loading Screen again (example: Button press)
    }

    

}
