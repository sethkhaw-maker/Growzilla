using UnityEngine;

public class ScreenShakeController : MonoBehaviour
{
    public static ScreenShakeController cam_instance;
    private float shaketimeRemaining;
    private float shakePower;
    void Awake()
    {
        if (cam_instance == null)
        {
            cam_instance = this;
        }
        else if (cam_instance != this)
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            startShake(0.1f, 0.1f);
        }
    }
    public void LateUpdate()
    {
        if(shaketimeRemaining > 0)
        {
            shaketimeRemaining -= Time.deltaTime;
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(xAmount, yAmount, 0);
        }
        if (shaketimeRemaining <= 0)
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
    public void startShake(float length, float power)
    {
        shaketimeRemaining = length;
        shakePower = power;
    }
}
