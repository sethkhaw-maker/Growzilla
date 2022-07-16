using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnKaijuuToLandAnimation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.name)
        {
            case "BigKaijuu":
            case "MidKaijuu":
            case "SmallKaijuu":
                other.gameObject.GetComponent<Animator>().SetTrigger("KaijuuLanded");
                ScreenShakeController.cam_instance.startShake(0.07f, 0.07f);
                break;
            default:
                break;
        }
    }
}
