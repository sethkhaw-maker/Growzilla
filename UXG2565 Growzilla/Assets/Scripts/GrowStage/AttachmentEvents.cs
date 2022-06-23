using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentEvents : MonoBehaviour
{
    public void Deactivate()
    {
        this.gameObject.SetActive(false);
    }
}
