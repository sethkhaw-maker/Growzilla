using UnityEngine;

public class RampageStageSceneHandler : MonoBehaviour
{
    public string Ability1 { get; private set; }
    public string Ability2 { get; private set; }

    [SerializeField] float WorldMoveSpeed = 1.0f;

    [SerializeField] float AbilityCooldown = 1.0f;
    float Ability1CooldownTimer = 0.0f;
    float Ability2CooldownTimer = 0.0f;

    [SerializeField] float HornWorldSpeedModifier = 1.0f;
    [SerializeField] float JumpForce = 1.0f;

    [SerializeField] GameObject HornsCollisionObject = null;
    [SerializeField] GameObject TailCollisionObject = null;

    [SerializeField] Rigidbody2D[] KaijuuRigidBody = new Rigidbody2D[3];

    void Start()
    {
        Ability1CooldownTimer = 0.0f;
        Ability2CooldownTimer = 0.0f;
    }

    public void OnSceneLoad()
    {
        // 1. Read data from data handler,
        // 2. Set the abilities accordingly,
        // 3. Color the Kaijuu accordingly
    }

    // Should be looping
    public void HandleObstacleSpawning()
    {

    }

    // Button Input
    public void OnAbility1ButtonPressed()
    {
        if (Ability1CooldownTimer > 0.0f) { Ability1CooldownTimer -= Time.deltaTime; return; }

        switch(Ability1)
        {
            case "Horns": ExecuteAbilityHorns(); break;
            case "Tail":  ExecuteAbilityTail();  break;
            case "Wings": ExecuteAbilityWings(); break;
            default: throw new System.Exception("Ability 1 is either null or unrecognized!");
        }

        Ability1CooldownTimer = AbilityCooldown;
    }
    public void OnAbility2ButtonPressed()
    {
        if (Ability2CooldownTimer > 0.0f) { Ability2CooldownTimer -= Time.deltaTime; return; }

        switch (Ability2)
        {
            case "Horns": ExecuteAbilityHorns(); break;
            case "Tail":  ExecuteAbilityTail();  break;
            case "Wings": ExecuteAbilityWings(); break;
            default: throw new System.Exception("Ability 2 is either null or unrecognized!");
        }

        Ability2CooldownTimer = AbilityCooldown;
    }

    // Executable abilities, can be refactored into strategy pattern if we need to expand upon this game.
    private void ExecuteAbilityHorns()
    {

    }
    private void ExecuteAbilityTail()
    {

    }
    private void ExecuteAbilityWings()
    {
        foreach(Rigidbody2D rb2d in KaijuuRigidBody)
        {
            if (rb2d.gameObject.activeSelf == true)
            {
                rb2d.AddForce(Vector2.up * rb2d.mass * JumpForce);
            }
        }
    }

}
