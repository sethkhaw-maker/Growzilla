using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentEvents : MonoBehaviour
{

    public GameObject itemtoActivate;
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }

    public void ActivateForiegnItem()
    {
        if (itemtoActivate != null)
        {
            itemtoActivate.gameObject.SetActive(true);
        }
    }
}
