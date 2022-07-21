using UnityEngine;

public class DamageVignetteHandler : MonoBehaviour
{
    public static DamageVignetteHandler Instance = null;
    [SerializeField] SpriteRenderer VignetteSR = null;

    void Awake() { if (Instance == null) Instance = this; else Destroy(this.gameObject); }
    void Start() { VignetteSR.color = new Color(1.0f, 1.0f, 1.0f, 0.0f); }
    void Update() { Tick(); }

    void Tick()
    {
        if (VignetteSR.color.a == 0.0f) { return; }
        float currentAlpha = VignetteSR.color.a - Time.deltaTime * 0.5f;
        if (currentAlpha < 0.0f) { currentAlpha = 0.0f; }
        VignetteSR.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);
    }

    public void OnDamageShowVignette() { VignetteSR.color = new Color(1.0f, 1.0f, 1.0f, 1.0f); }
}
