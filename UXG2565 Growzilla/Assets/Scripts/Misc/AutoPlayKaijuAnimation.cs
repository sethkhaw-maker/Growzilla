using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPlayKaijuAnimation : MonoBehaviour
{
    public Animator SmallKaiju;
    public Animator MidKaiju;
    public Animator BigKaiju;

    // Update is called once per frame
    void Update()
    {
        if (SmallKaiju.gameObject.activeSelf == false)
        {
            if (MidKaiju.gameObject.activeSelf == false)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    BigKaiju.Play("Walk");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                   BigKaiju.Play("Attack");
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    BigKaiju.Play("Jump");
                }
                else if(Input.GetKeyDown(KeyCode.Space))
                {
                    BigKaiju.Play("Idle");
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    MidKaiju.Play("Walk");
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    MidKaiju.Play("Attack");
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    MidKaiju.Play("Jump");
                }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    MidKaiju.Play("Idle");
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SmallKaiju.Play("Walk");
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SmallKaiju.Play("Attack");
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SmallKaiju.Play("Jump");
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                SmallKaiju.Play("Idle");
            }
        }
    }
}
