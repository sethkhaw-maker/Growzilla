using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeveralSeconds : MonoBehaviour
{
    public float seconds = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Terminate", seconds);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Terminate()
    {
        Destroy(this.gameObject);
    }
}
