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
                break;
            default:
                break;
        }
    }
}
