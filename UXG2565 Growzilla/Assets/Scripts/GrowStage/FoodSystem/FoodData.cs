public class FoodData
{
    public int SpriteType = 1;
    public int FoodType = 1;
    public UnityEngine.Vector3 FoodColor = new UnityEngine.Vector3(0, 0, 0);
}

public enum FoodObjectType
{
    Conveyor,
    Display,
    Falling,
}
