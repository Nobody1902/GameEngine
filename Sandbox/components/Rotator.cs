using GameEngine;
using GameEngine.ECS;
using GameEngine.ECS.Components;

public class Rotator : Component
{
    public Rotator(GameObject gameObject) : base(gameObject)
    {}

    public float Speed = 10_000_000f;
    public override void Update()
    {
        // Rotate the cube

        transform.rotation += new Vector3(Time.DeltaTime * Speed*0, Time.DeltaTime * Speed, 0f);
    }
}