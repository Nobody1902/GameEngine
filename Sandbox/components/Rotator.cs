using GameEngine;
using GameEngine.ECS;
using GameEngine.ECS.Components;
using System.Numerics;

public class Rotator : Component
{
    public Rotator(GameObject gameObject) : base(gameObject)
    {}
    public float Speed = 10_000_000f;
    public override void Update()
    {
        // Rotate the cube

        transform.rotation += new Vector3(0f, Time.DeltaTime * Speed, 0f);
        if (transform.rotation.Y > 360f)
        {
            transform.rotation -= new Vector3(transform.rotation.X, transform.rotation.Y, 0);
        }
    }
}